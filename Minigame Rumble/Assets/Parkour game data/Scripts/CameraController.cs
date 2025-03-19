using UnityEngine;

public class CameraController : MonoBehaviour
{
    public string targetTag = "Player"; // Tag to find player at runtime
    public float smoothSpeed = 5f; // Speed of smoothing
    public Vector3 offset; // Offset from the player

    private Transform target;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag(targetTag);
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogWarning("CameraFollow: No player found with tag " + targetTag);
        }
    }

    private void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
