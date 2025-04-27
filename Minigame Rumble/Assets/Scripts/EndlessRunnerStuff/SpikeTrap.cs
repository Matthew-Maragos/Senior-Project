using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Instantly kill the player
            PlayerDeath playerDeath = collision.GetComponent<PlayerDeath>();
            if (playerDeath != null)
            {
                playerDeath.Kill();
            }
            else
            {
                Debug.LogWarning("PlayerDeath script missing on Player!");
            }
        }
    }
}