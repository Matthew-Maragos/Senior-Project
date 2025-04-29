using System.Collections.Generic;
using UnityEngine;
using TMPro;

// This script will handle the multiplayer logic
public partial class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Keeping track of players, the current player and the scores
    public int numPlayers; 
    private int currentPlayer = 0;
    private int[] playerScores;
    private int[] players;
    private int winnerIndex = -1;
    public List<int> currentWinningPlayers = new List<int>();

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

        numPlayers = MainGameManager.Instance.GetPlayerCount();
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

    public void AddPointToPlayer(int index)
    {
        playerScores[index]++;
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
    
    public List<int> GetWinningPlayerIndices()
    {
        List<int> winners = new List<int>();
        int maxScore = -1;

        for (int i = 0; i < numPlayers; i++)
        {
            if (playerScores[i] > maxScore)
            {
                maxScore = playerScores[i];
                winners.Clear();       // Clear the previous lower scores
                winners.Add(i);        // Add the new top player
            }
            else if (playerScores[i] == maxScore)
            {
                winners.Add(i);        // Add another player tied with top score
            }
        }

        return winners;
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