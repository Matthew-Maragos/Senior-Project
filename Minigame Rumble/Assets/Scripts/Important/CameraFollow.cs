using UnityEngine;

public class CameraFollow : MonoBehaviour
{ 
    public Transform player;

    private Vector3 lastPlayerPosition;

    void Start()
    {
        if (player != null)
        {
            lastPlayerPosition = player.position;
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            float deltaX = player.position.x - lastPlayerPosition.x;

            // Move the camera only on the X axis by the same amount the player moved
            transform.position += new Vector3(deltaX, 0f, 0f);

            lastPlayerPosition = player.position;
        }
    }
}