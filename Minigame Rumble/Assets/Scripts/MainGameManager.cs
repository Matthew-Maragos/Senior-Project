using UnityEngine;
using UnityEngine.UI;

public class MainGameManager : MonoBehaviour
{
    public static int playerCount = 2; // Default to 2 players
    public Button twoPlayerButton, threePlayerButton, fourPlayerButton;

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
}
