using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SPUM_Prefabs spum;
    private PlayerInput playerInput;
    private InputAction jumpAction;

    [Header("Settings")]
    public float moveSpeed = 3f;
    public float jumpForce = 5f;
    
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;
    
    private float speedIncreaseTimer = 0f;
    public float speedIncreaseInterval = 15f; // time in seconds between speed increases
    
    private bool isGrounded;
    private bool gameStarted = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spum = GetComponent<SPUM_Prefabs>();
        playerInput = GetComponent<PlayerInput>();
        
        Debug.Log($"[{gameObject.name}] Default action map: {playerInput.currentActionMap?.name}");
        jumpAction = playerInput.actions["Jump"];
        if (jumpAction == null)
        {
            Debug.LogError("Jump action not found!");
        }
        else
        {
            Debug.Log("Jump action hooked up!");
            jumpAction.performed += ctx => jumpAction.performed += ctx =>
            {
                Debug.Log($"{gameObject.name} received Jump input from device: {ctx.control.device.name}");
                Jump();
            };
        }
        
    }

    void OnEnable()
    {
        jumpAction?.Enable();
    }

    void OnDisable()
    {
        jumpAction?.Disable();
    }

    void Update()
    {
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);

        // Handle speed increase over time
        speedIncreaseTimer += Time.deltaTime;
        if (speedIncreaseTimer >= speedIncreaseInterval)
        {
            moveSpeed += 0.5f;
            speedIncreaseTimer = 0f;
            Debug.Log($"{gameObject.name} speed increased to {moveSpeed}");
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    public void setMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void setJumpForce(float force)
    {
        jumpForce = force;
    }
    
    
}