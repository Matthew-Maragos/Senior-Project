using UnityEngine;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour
{
    public List<Transform> players = new List<Transform>(); // List of all players
    public float cameraSpeed = 5f; // Speed at which camera moves when no one is pushing it
    private Vector3 lastCameraPosition;
    private bool playersSpawned = false; 

    void Start()
    {
        lastCameraPosition = transform.position;
    }

    void LateUpdate()
    {
        if (!playersSpawned) return; // Not moving camera til players are spawned in
        // Remove any players that have been destroyed (null entries)
        players.RemoveAll(player => player == null);

        if (players.Count > 0)
        {
            // Find the furthest X position of all alive players
            float furthestPlayerX = float.MinValue;
            foreach (Transform player in players)
            {
                if (player.position.x > furthestPlayerX)
                {
                    furthestPlayerX = player.position.x;
                }
            }

            // Move the camera toward the furthest player, or at a steady speed
            float deltaX = furthestPlayerX - lastCameraPosition.x;

            // If players are still pushing the camera forward
            if (deltaX > 0f)
            {
                transform.position += new Vector3(deltaX, 0f, 0f);
            }
            else
            {
                // Move at steady speed if no players are pushing forward
                transform.position += new Vector3(cameraSpeed * Time.deltaTime, 0f, 0f);
            }

            lastCameraPosition = transform.position;
        }
        else
        {
            // No players left alive -> Stop moving (or you can trigger a game over here)
            Debug.Log("All players are dead. Camera stopped.");
        }
    }
    
    public void SetPlayers(List<Transform> spawnedPlayers)
    {
        players = spawnedPlayers;
        playersSpawned = true;
    }
}