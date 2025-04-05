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
    private int winnerIndex = -1;

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

    public int GetWinningPlayerIndex()
    {
        int highest = -1;
        int maxScore = -1;
        bool tie = false;

        for (int i = 0; i < numPlayers; i++)
        {
            if (playerScores[i] > maxScore)
            {
                highest = i;
                maxScore = playerScores[i];
                tie = false;
            }
            else if (playerScores[i] == maxScore)
            {
                tie = true;
            }
        }

        return tie ? -1 : highest;
    }
    public void SetWinner(int index)
    {
        winnerIndex = index;
    }

    public int GetWinner()
    {
        return winnerIndex;
    }  
    
}