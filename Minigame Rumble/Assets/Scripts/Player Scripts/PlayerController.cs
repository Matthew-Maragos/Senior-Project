using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerControls controls;
    private Rigidbody2D rb;
    private SPUM_Prefabs spum;
    private PlayerInput playerInput;

    [Header("Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;

    private bool isGrounded;

    void Awake()
    {
        controls = new PlayerControls();
        controls.Jumping.Jump.performed += ctx => Jump();
        playerInput = GetComponent<PlayerInput>();
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spum = GetComponent<SPUM_Prefabs>();
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