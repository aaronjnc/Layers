using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(LayerTraveler))]
public class PlayerMovement : Singleton<PlayerMovement>
{

    public delegate void DescendingDelgate(bool bDescending);
    public DescendingDelgate Descending;

    private Rigidbody2D rb;
    private PlayerControls controls;
    private CapsuleCollider2D col;
    private LayerTraveler layerTraveler;
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
    private bool bMovingToLocation = false;
    private Vector3 goalLocation = Vector3.zero;
    private float moveAcceptanceRadius = 0.0f;
    private Action callbackAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        layerTraveler = GetComponent<LayerTraveler>();
        controls = new PlayerControls();
        controls.PlayerActions.Movement.performed += Move;
        controls.PlayerActions.Movement.canceled += StopMove;
        controls.PlayerActions.Movement.Enable();
        controls.PlayerActions.Jump.performed += Jump;
        controls.PlayerActions.Jump.Enable();
        controls.PlayerActions.ClickAction.performed += ClickAction;
        controls.PlayerActions.ClickAction.Enable();
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

    void ClickAction(CallbackContext ctx)
    {
        RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!hit.collider) return;

        if (hit.collider.gameObject.TryGetComponent<MovableObject>(out var movable))
        {
            movable.Clicked();
        }
    }

    private void FixedUpdate()
    {
        if (bMovingToLocation)
        {
            if (Vector3.Distance(transform.position, goalLocation) <= moveAcceptanceRadius)
            {
                transform.position = goalLocation;
                bMovingToLocation = false;
                rb.gravityScale = 1.0f;
                col.enabled = true;
                callbackAction?.Invoke();
                callbackAction = null;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, goalLocation, speed * Time.deltaTime);
            }
        }
        else
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
    }

    private void OnDestroy()
    {
        controls.Disable();
    }

    private bool isGrounded()
    {
        return Physics2D.Raycast(transform.position, -transform.up, col.bounds.extents.y + .1f, floor);
    }

    private bool isStoppedGrounded()
    {
        return bGrounded;
    }

    public float GetPlayerLow()
    {
        return col.bounds.min.y;
    }

    public int GetPlayerLayer()
    {
        return layerTraveler.GetCurrentLayer();
    }

    public void MoveToLocation(Vector3 location, bool bIngoreCollision, float acceptanceRadius)
    {

    }

    public void MoveToLocation(Vector3 location, bool bIgnoreCollision, bool bIgnoreGravity, float acceptanceRadius, Action callback)
    {
        col.enabled = !bIgnoreCollision;
        if (bIgnoreGravity)
            rb.gravityScale = 0.0f;
        goalLocation = location;
        moveAcceptanceRadius = acceptanceRadius;
        callbackAction = callback;
        bMovingToLocation = true;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float GetVelocityX()
    {
        return rb.linearVelocityX;
    }
}
