using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerBase : MonoBehaviour
{
    public float moveSpeed = 5f;
    protected Vector2 moveInput;
    protected Rigidbody2D rb;
    protected PlayerInput playerInput;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    protected virtual void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }
}