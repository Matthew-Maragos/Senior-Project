using UnityEngine;
using TMPro;

// This script will handle the multiplayer logic
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Keeping track of players, the current player and the scores
    public int numPlayers = 2; 
    private int currentPlayer = 0;
    private int[] playerScores;

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
        playerScores = new int[numPlayers];
    }

    public int GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public void EndTurn()
    {
        // Move to next player
        Debug.Log("Turn Ended");
        currentPlayer = (currentPlayer + 1) % numPlayers;
        MemoryGameSetup.Instance.UpdateUI();
    }

    public void AddPointToCurrentPlayer()
    {
        Debug.Log("Add Point");
        playerScores[currentPlayer]++;
    }

    public int getPlayerScore(int playerIndex)
    {
        return playerScores[playerIndex];
    }
}