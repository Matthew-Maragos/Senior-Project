using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager Instance;
    
    public static int playerCount = 2; // Default to 2 players

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
        
        
    }
    void Start()
    {
    }

   public void SetPlayerCount(int count)
    {
        playerCount = count;
        Debug.Log("Player count set to: " + playerCount);
        playerWins = new int[playerCount];
    }

    public int GetPlayerCount()
    {
        return playerCount;
    }

    public void AddWinToPlayers()
    {
        List<int> winners = GameManager.Instance.GetWinningPlayerIndices();
        foreach (int winner in winners) 
        {
            playerWins[winner]++;
            Debug.Log($"Player {winner + 1} wins increased to {playerWins[winner]}");
        }
        
    }
    public void AddWinToPlayer(int winner)
    {
        playerWins[winner]++;
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
    
    public enum MinigameType
    {
        MemoryGame,
        EndlessRunner,
        Other
    }

    public MinigameType minigameType = MinigameType.Other;
}
