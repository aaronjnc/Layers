using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(CapsuleCollider2D))]
public class PlayerMovement : Singleton<PlayerMovement>
{

    public delegate void DescendingDelgate(bool bDescending);
    public DescendingDelgate Descending;

    private Rigidbody2D rb;
    private PlayerControls controls;
    private CapsuleCollider2D col;
    [SerializeField]
    private float speed = 3.0f;
    [SerializeField]
    private float jumpForce = 3.0f;
    private float moveDir = 0;
    [SerializeField]
    private LayerMask floor;
    bool bGrounded = false;
    bool bAscending = false;
    float prevYVel = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        controls = new PlayerControls();
        controls.PlayerActions.Movement.performed += Move;
        controls.PlayerActions.Movement.canceled += StopMove;
        controls.PlayerActions.Movement.Enable();
        controls.PlayerActions.Jump.performed += Jump;
        controls.PlayerActions.Jump.Enable();
    }

    void Move(CallbackContext ctx)
    {
        moveDir = ctx.ReadValue<float>();
        if (!bGrounded)
        {
            return;
        }
        if (rb.linearVelocityX != moveDir * speed)
            rb.linearVelocityX = moveDir * speed;
    }

    void StopMove(CallbackContext ctx)
    {
        moveDir = 0;
        if (!bGrounded)
        {
            return;
        }
        rb.linearVelocityX = 0;
    }

    void Jump(CallbackContext ctx)
    {
        if (bGrounded)
        {
            rb.AddForceY(jumpForce * rb.gravityScale * rb.mass);
            bAscending = true;
            prevYVel = rb.linearVelocityY;
        }
    }

    private void FixedUpdate()
    {
        bool bFrameGrounded = isGrounded() && Mathf.Abs(rb.linearVelocityY) <= .5f;
        if (bAscending)
        {
            if (prevYVel > 0 && rb.linearVelocityY <= 0)
            {
                bAscending = false;
                Descending(true);
            }
            prevYVel = rb.linearVelocityY;
        }
        if (bFrameGrounded && !bGrounded)
        {
            Descending(false);
            rb.linearVelocityX = moveDir * speed;
        }
        if (moveDir != 0)
        {
            rb.linearVelocityX = moveDir * speed;
        }
        bGrounded = bFrameGrounded;
    }

    private void OnDestroy()
    {
        controls.Disable();
    }

    private bool isGrounded()
    {
        return Physics2D.Raycast(transform.position, -transform.up, col.bounds.extents.y + .1f, floor);
    }

    public float GetPlayerLow()
    {
        return col.bounds.min.y;
    }
}
