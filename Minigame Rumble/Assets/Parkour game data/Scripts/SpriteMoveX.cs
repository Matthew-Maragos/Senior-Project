using UnityEngine;

public class SpriteMoveX : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 5f; // Movement speed
    public bool moveRight = true; // Direction control

    void Update()
    {
        // Determine movement direction
        float moveDirection = moveRight ? 1f : -1f;

        // Move the sprite in the X direction
        transform.position += new Vector3(moveDirection * speed * Time.deltaTime, 0f, 0f);
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.SetActive(false);
        }
    }
}
