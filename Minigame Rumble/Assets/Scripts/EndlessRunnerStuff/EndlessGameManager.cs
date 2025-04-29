using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class EndlessGameManager : MonoBehaviour
{
    public List<GameObject> playerPrefabs = new List<GameObject>(); // Assign Player 1, Player 2, Player 3, Player 4 prefabs
    public List<Transform> spawnPoints = new List<Transform>(); // Your spawn points
    public CameraFollow cameraFollow;
    public int numberOfPlayers = 2;
    public ScreenController sceneNavigator;

    private List<Transform> spawnedPlayers = new List<Transform>();
    private List<GameObject> playersGameObjects = new List<GameObject>(); // To track the GameObjects for player death
    private List<PlayerInfo> playersInfo = new List<PlayerInfo>();

    private int lastPlayerOriginalIndex = -1; // To track the index of the last alive player

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
        // Clear previous players in case of respawn
        spawnedPlayers.Clear();
        playersGameObjects.Clear();
        playersInfo.Clear();

        for (int i = 0; i < numberOfPlayers; i++)
        {
            if (i < spawnPoints.Count && i < playerPrefabs.Count)
            {
                GameObject newPlayer = Instantiate(playerPrefabs[i], spawnPoints[i].position, playerPrefabs[i].transform.rotation);
                spawnedPlayers.Add(newPlayer.transform);
                playersGameObjects.Add(newPlayer); // Keep track of the actual GameObject for death checking
                playersInfo.Add(new PlayerInfo(newPlayer, i)); // Store player info with original index
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

        // If only one player is alive and no point has been awarded, award them a point
        if (alivePlayers == 1 && lastPlayerOriginalIndex == -1)
        {
            // Save the index of the last alive player
            lastPlayerOriginalIndex = GetLastAlivePlayerOriginalIndex();
            Debug.Log("Last player index: " + lastPlayerOriginalIndex);
        }

        // If no players are alive, award points to the last player who was alive
        if (alivePlayers == 0 && lastPlayerOriginalIndex != -1)
        {
            AwardPointToPlayer(lastPlayerOriginalIndex);
            DOTween.KillAll();
            sceneNavigator.LoadScene(7); // Load next scene or game over screen
        }
    }

    private void AwardPointToPlayer(int playerIndex)
    {
        // Implement awarding points logic here
        GameManager.Instance.SetWinner(playerIndex);
        Debug.Log($"Awarding point to player {playerIndex + 1}");
    }

    void CheckAlivePlayers()
    {
        // Remove dead players from playersGameObjects
        playersGameObjects.RemoveAll(player => player == null);

        // Remove dead players from playersInfo
        playersInfo.RemoveAll(info => info.playerObject == null);
    }

    int CountAlivePlayers()
    {
        // Return the number of alive players
        return playersGameObjects.Count;
    }

    int GetLastAlivePlayerOriginalIndex()
    {
        if (playersInfo.Count == 1)
        {
            return playersInfo[0].originalIndex;
        }

        return -1; // No alive player or still multiple players
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
