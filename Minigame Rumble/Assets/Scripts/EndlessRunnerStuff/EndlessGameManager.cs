using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;

public class EndlessGameManager : MonoBehaviour
{
    public List<GameObject> playerPrefabs = new List<GameObject>(); // Player prefabs (with PlayerInput attached)
    public List<Transform> spawnPoints = new List<Transform>();     // Spawn points
    public CameraFollow cameraFollow;
    public int numberOfPlayers = 2;
    public ScreenController sceneNavigator;

    private List<Transform> spawnedPlayers = new List<Transform>();
    private List<GameObject> playersGameObjects = new List<GameObject>();
    private List<PlayerInfo> playersInfo = new List<PlayerInfo>();

    private int lastPlayerOriginalIndex = -1;

    private void Start()
    {
        GameManager.Instance.currentWinningPlayers.Clear();
        sceneNavigator = FindAnyObjectByType<ScreenController>();
        numberOfPlayers = MainGameManager.playerCount;
        SpawnPlayers(numberOfPlayers);
        MainGameManager.Instance.minigameType = MainGameManager.MinigameType.EndlessRunner;
    }

    void SpawnPlayers(int numberOfPlayers)
    {
        spawnedPlayers.Clear();
        playersGameObjects.Clear();
        playersInfo.Clear();

        var gamepads = Gamepad.all;

        if (gamepads.Count < numberOfPlayers)
        {
            Debug.LogError("Not enough controllers connected.");
            return;
        }

        for (int i = 0; i < numberOfPlayers; i++)
        {
            if (i < spawnPoints.Count && i < playerPrefabs.Count)
            {
                GameObject prefab = playerPrefabs[i];
                Gamepad device = gamepads[i];

                PlayerInput playerInput = PlayerInput.Instantiate(
                    prefab,
                    playerIndex: i,
                    controlScheme: null, // uses default
                    pairWithDevice: device
                );

                playerInput.transform.position = spawnPoints[i].position;

                spawnedPlayers.Add(playerInput.transform);
                playersGameObjects.Add(playerInput.gameObject);
                playersInfo.Add(new PlayerInfo(playerInput.gameObject, i));
            }
        }

        if (cameraFollow != null)
        {
            cameraFollow.SetPlayers(spawnedPlayers);
            Debug.Log("CameraFollow: Players have Spawned!");
        }
    }

    void Update()
    {
        CheckAlivePlayers();

        int alivePlayers = CountAlivePlayers();

        if (alivePlayers == 1 && lastPlayerOriginalIndex == -1)
        {
            lastPlayerOriginalIndex = GetLastAlivePlayerOriginalIndex();
            Debug.Log("Last player index: " + lastPlayerOriginalIndex);
        }

        if (alivePlayers == 0 && lastPlayerOriginalIndex != -1)
        {
            AwardPointToPlayer(lastPlayerOriginalIndex);
            DOTween.KillAll();
            sceneNavigator.LoadScene(5); // Load results screen or next scene
        }
    }

    private void AwardPointToPlayer(int playerIndex)
    {
        GameManager.Instance.SetWinner(playerIndex);
        Debug.Log($"Awarding point to player {playerIndex + 1}");
    }

    void CheckAlivePlayers()
    {
        playersGameObjects.RemoveAll(player => player == null);
        playersInfo.RemoveAll(info => info.playerObject == null);
    }

    int CountAlivePlayers()
    {
        return playersGameObjects.Count;
    }

    int GetLastAlivePlayerOriginalIndex()
    {
        if (playersInfo.Count == 1)
        {
            return playersInfo[0].originalIndex;
        }

        return -1;
    }

    [System.Serializable]
    public class PlayerInfo
    {
        public GameObject playerObject;
        public int originalIndex;

        public PlayerInfo(GameObject obj, int index)
        {
            playerObject = obj;
            originalIndex = index;
        }
    }
}
