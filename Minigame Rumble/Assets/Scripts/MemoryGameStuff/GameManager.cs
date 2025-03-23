using UnityEngine;
using TMPro;

// This script will handle the multiplayer logic
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Keeping track of players, the current player and the scores
    public int numPlayers = 2; 
    private int currentPlayer = 0;

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
}