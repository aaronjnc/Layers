using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(LayerTraveler))]
[RequireComponent(typeof(MoveTo))]
[RequireComponent(typeof(PlayerInventory))]
public class PlayerMovement : Singleton<PlayerMovement>
{

    public delegate void DescendingDelgate(bool bDescending);
    public DescendingDelgate Descending;


    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private PlayerControls controls;
    private CapsuleCollider2D col;
    private LayerTraveler layerTraveler;
    private PlayerInventory inventory;
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
    float inputHorizontal;
    private bool bMovingToLocation = false;
    private MoveTo moveToLocation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<CapsuleCollider2D>();
        layerTraveler = GetComponent<LayerTraveler>();
        moveToLocation = GetComponent<MoveTo>();
        inventory = GetComponent<PlayerInventory>();
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

    void Update()
    {
        if ((rb.linearVelocityX > 0) || (rb.linearVelocityX < 0))
        {
            animator.SetBool("isMoving", true);
        }
        else {
            animator.SetBool("isMoving", false);
        }

    }


    void ClickAction(CallbackContext ctx)
    {
        RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!hit.collider) return;

        Debug.Log("Clicked " + hit.collider.gameObject.name);

        if (hit.collider.gameObject.TryGetComponent<ObjectInteractionManager>(out var interactionManager))
        {
            InteractableInterface interactable;
            if ((interactable = interactionManager.GetInteractable()) != null)
            {
                MoveToInteractable moveToInteractable = interactable.GetMoveToInteractable();
                if (moveToInteractable)
                {
                    Debug.Log("Move to");
                    MoveToLocation(moveToInteractable);
                }
                else
                {
                    Debug.Log("Interact");
                    interactable.Interact(inventory.GetHeldItem());
                }
            }
        }


    }

    private void FixedUpdate()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        
        if (!bMovingToLocation)
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
        if (inputHorizontal < 0) 
        {
            spriteRenderer.flipX = false;
        }
        if (inputHorizontal > 0) 
        {
            spriteRenderer.flipX = true;
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

    public void MoveToLocation(MoveToInteractable moveToInteractable)
    {
        if (moveToInteractable.isApproachAutomated() && moveToInteractable.CanMoveTo())
        {
            MoveToLocation(moveToInteractable.GetMoveLoc(), false, moveToInteractable.GetAcceptanceRadius(), moveToInteractable.callback);
        }
    }

    public void MoveToLocation(Vector3 location, bool bIgnoreCollision, float acceptanceRadius, Action callback)
    {
        MoveToLocation(location, bIgnoreCollision, false, acceptanceRadius, callback);
    }

    public void MoveToLocation(Vector3 location, bool bIgnoreCollision, bool bIgnoreGravity, float acceptanceRadius, Action callback)
    {
        callback += ReachDestination;
        moveToLocation.MoveToLocation(location, bIgnoreCollision, bIgnoreGravity, acceptanceRadius, callback);
        bMovingToLocation = true;
    }

    public void ReachDestination()
    {
        bMovingToLocation = false;
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
