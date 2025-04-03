using UnityEngine;
using TMPro;

// This script will handle the multiplayer logic
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Keeping track of players, the current player and the scores
    public int numPlayers; 
    private int currentPlayer = 0;
    private int[] playerScores;
    private int[] players;

    public TMP_Text turnText;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        numPlayers = MainGameManager.playerCount;
        playerScores = new int[numPlayers];
        players = new int[numPlayers];
    }

    public int GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public void EndTurn()
    {
        // Move to next player
        currentPlayer = (currentPlayer + 1) % numPlayers;
        MemoryGameSetup.Instance.UpdateUI();
    }

    public void AddPointToCurrentPlayer()
    {
        playerScores[currentPlayer]++;
        
        MemoryGameSetup.Instance.UpdateUI();
    }

    public int getPlayerScore(int playerIndex)
    {
        return playerScores[playerIndex];
    }

    public int getPlayer(int playerIndex)
    {
        if (playerIndex >= 0 && playerIndex < players.Length)
        {
            return players[playerIndex];
        }
        else
        {
            Debug.LogWarning("Invalid player index requested: " + playerIndex);
            return -1; // Return -1 as an invalid value
        }
    }
}