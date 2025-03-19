using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movementInput;
    private bool isGrounded;
    private bool isAttacking;
    private bool attackToggle;

    public float moveSpeed = 3f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private Vector3 originalScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        originalScale = transform.localScale;

        rb.freezeRotation = true; // Prevent rotation
        rb.linearDamping = 0; // No drag (instant movement)
        rb.gravityScale = 2f; // Proper gravity
    }

    void Update()
    {
        HandleInput();
        AnimatePlayer();
    }

    void FixedUpdate()
    {
        MovePlayer();
        CheckGroundStatus();
    }

    private void HandleInput()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal"); // Get movement input

        // Jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            Debug.Log("Is Ground....!  "+isGrounded);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Attack input (One button for both Attack1 & Attack2)
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            isAttacking = true;
            attackToggle = !attackToggle;

            anim.SetTrigger(attackToggle ? "Attack1" : "Attack2");

            Invoke(nameof(ResetAttack), 0.4f); // Reset attack after animation
        }
    }

    private void MovePlayer()
    {
        if (isAttacking) return; // Don't move when attacking

        rb.linearVelocity = new Vector2(movementInput.x * moveSpeed, rb.linearVelocity.y); // Instant movement

        // Flip character direction
        if (movementInput.x > 0)
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        else if (movementInput.x < 0)
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
    }

    private void AnimatePlayer()
    {
        if (isAttacking) return; // Don't change animation when attacking

        bool isMoving = Mathf.Abs(movementInput.x) > 0.1f;

        anim.ResetTrigger("Idle");
        anim.ResetTrigger("Run");
        anim.ResetTrigger("Jump");

        // Play animations instantly
        if (!isGrounded)
        {
            anim.SetTrigger("Jump");
        }
        else if (isMoving)
        {
            anim.SetTrigger("Run");
        }
        else
        {
            anim.SetTrigger("Idle");
        }
    }

    private void CheckGroundStatus()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.08f, groundLayer);
    }

    private void ResetAttack()
    {
        isAttacking = false;
    }
}
