using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public void Kill()
    {
        Debug.Log("Player touched spikes and died!");
        // You can trigger death animation, sound, or reload the scene here
        Destroy(gameObject);
    }
}