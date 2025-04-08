using UnityEngine;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager Instance;
    
    public static int playerCount = 2; // Default to 2 players
    public Button twoPlayerButton, threePlayerButton, fourPlayerButton;

    private int[] playerWins;
    void Awake()
    {
        // Singleton pattern to persist and access globally
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep across scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        playerWins = new int[playerCount];
    }
    void Start()
    {
        
    }

   public void SetPlayerCount(int count)
    {
        playerCount = count;
        Debug.Log("Player count set to: " + playerCount);
        // You can use playerCount to update your game logic accordingly
    }

    public int GetPlayerCount()
    {
        return playerCount;
    }

    public void AddWinToPlayer(int playerIndex)
    {
        if (playerIndex >= 0 && playerIndex < playerWins.Length)
        {
            playerWins[playerIndex]++;
            Debug.Log($"Player {playerIndex + 1} wins increased to {playerWins[playerIndex]}");
        }
    }
    
    public int GetPlayerWins(int playerIndex)
    {
        if (playerIndex >= 0 && playerIndex < playerWins.Length)
        {
            return playerWins[playerIndex];
        }
        return 0;
    }

    public void ResetWins()
    {
        for (int i = 0; i < playerWins.Length; i++)
        {
            playerWins[i] = 0;
        }
    }
}
