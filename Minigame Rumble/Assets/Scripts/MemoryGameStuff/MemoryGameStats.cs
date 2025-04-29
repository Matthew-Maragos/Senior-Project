using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text;
using TMPro;

public class MemoryGameStats : MonoBehaviour
{
    public TMP_Text winText;
    public TMP_Text scoreText;
    public bool callGetWinningPlayerIndices = true; 

    //private int winner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    void Start()
    {
        if (winText == null)
            winText = GameObject.Find("Winner Text")?.GetComponent<TMP_Text>();
    
        if (scoreText == null)
            scoreText = GameObject.Find("Player's Stats")?.GetComponent<TMP_Text>();

        if (scoreText == null)
        {
            Debug.LogError("WinText or ScoreText not found in scene! Make sure they are named correctly.");
        }
        if (GameManager.Instance != null)
        {
            if (MainGameManager.Instance.minigameType == MainGameManager.MinigameType.MemoryGame)
            {
                callGetWinningPlayerIndices = true;
            }
            else
            {
                callGetWinningPlayerIndices = false;
            }
        }
        
        StartCoroutine(DelayedWinUpdate());
        
    }
    IEnumerator DelayedWinUpdate()
    {
        yield return null;

        if (MainGameManager.Instance != null)
        {
            List<int> winners = new List<int>();

            if (MainGameManager.Instance.minigameType == MainGameManager.MinigameType.MemoryGame)
            {
                // MEMORY GAME LOGIC (might be tie, so check winners list)
                winners = GameManager.Instance.GetWinningPlayerIndices();
                MainGameManager.Instance.AddWinToPlayers();
            }
            else
            {
                // ENDLESS RUNNER LOGIC (only 1 winner, no ties)
                int winner = GameManager.Instance.GetWinner();
                if (winner != -1)
                {
                    winners.Add(winner);
                    MainGameManager.Instance.AddWinToPlayer(winner);
                }
            }

            // Update win text
            if (winners.Count == 1)
            {
                winText.text = $"Player {winners[0] + 1} Wins!";
            }
            else
            {
                winText.text = $"Players {string.Join(", ", winners.Select(w => (w + 1).ToString()))} Tie!";
            }

            // Update all players' scores
            StringBuilder sb = new StringBuilder();
            int numPlayers = MainGameManager.Instance.GetPlayerCount();

            for (int i = 0; i < numPlayers; i++)
            {
                int score = MainGameManager.Instance.GetPlayerWins(i);
                sb.AppendLine($"Player {i + 1} Score: {score}");
            }

            scoreText.text = sb.ToString();
        }
        else
        {
            Debug.LogError("MainGameManager.Instance is null!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
