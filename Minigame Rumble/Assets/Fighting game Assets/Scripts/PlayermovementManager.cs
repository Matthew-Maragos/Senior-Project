using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovementManager : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movementInput;
    private bool isGrounded;
    private bool isAttacking;
    private bool attackToggle;
    public LayerMask enemyLayer;
    public float moveSpeed = 3f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private Vector3 originalScale;
    public GameObject attackHitbox;

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
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void HandleInput()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal"); // Get movement input

        // Jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            anim.SetTrigger("Jump");
            isGrounded = false;
        }

        // Attack input (One button for both Attack1 & Attack2)
        if (Input.GetButtonDown("Fire1") && !isAttacking && !IsPointerOverUI())
        {
            isAttacking = true;
            attackToggle = !attackToggle;

            anim.SetTrigger(attackToggle ? "Attack1" : "Attack2");
            PerformAttackClientRpc();
            Invoke(nameof(ResetAttack), 0.4f); // Reset attack after animation
        }
       
    }
    bool IsPointerOverUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }
    void PerformAttackClientRpc()
    {
        attackHitbox.SetActive(true);
        Invoke(nameof(DisableHitbox), 0.3f); // Auto-disable after attack
    }

    void DisableHitbox()
    {
        attackHitbox.SetActive(false);
    }

    private void MovePlayer()
    {
        if (isAttacking) return; // Don't move when attacking

        // Move player
        rb.linearVelocity = new Vector2(movementInput.x * moveSpeed, rb.linearVelocity.y);

        // Play animations based on movement
        if (movementInput.x != 0)
        {
            anim.SetTrigger("Run");
        }
        else
        {
            anim.SetTrigger("Idle");
        }

        // Flip character direction
        if (movementInput.x > 0)
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        else if (movementInput.x < 0)
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
        if (((1 << col.gameObject.layer) & enemyLayer) != 0)
        {
            Debug.Log("Hit Enemy: " + col.gameObject.name);
            // Apply damage function here
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }

    private void ResetAttack()
    {
        isAttacking = false;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("enemydead....!");
            col.gameObject.SetActive(false);
        }
    }
}