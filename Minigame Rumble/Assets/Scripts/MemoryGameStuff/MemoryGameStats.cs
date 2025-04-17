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
        
        StartCoroutine(DelayedWinUpdate());
        
    }
    IEnumerator DelayedWinUpdate()
    {
        // Wait one frame to ensure everything is initialized
        yield return null;

        if (MainGameManager.Instance != null)
        {
            //int winner = GameManager.Instance.GetWinner();
            //MainGameManager.Instance.AddWinToPlayer(winner);
            
            List<int> winners = GameManager.Instance.GetWinningPlayerIndices();
            MainGameManager.Instance.AddWinToPlayers();

// Update win text (optional: show all tied winners)
            if (winners.Count == 1)
            {
                winText.text = $"Player {winners[0] + 1} Wins!";
            }
            else
            {
                winText.text = $"Players {string.Join(", ", winners.Select(w => (w + 1).ToString()))} Tie!";
            }
            
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
