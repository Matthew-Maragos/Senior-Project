using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SPUM_Prefabs spum;
    private PlayerInput playerInput;
    private InputAction jumpAction;

    [Header("Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;

    private bool isGrounded;

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
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        //spum = GetComponent<SPUM_Prefabs>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Always moving to the right
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
    
    
}