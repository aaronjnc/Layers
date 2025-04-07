using System;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.DefaultInputActions;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(MoveToInteractable))]
[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
[RequireComponent(typeof(ObjectInteractionManager))]
public class MovableObject : LayerObject, InteractableInterface
{

    [SerializeField]
    private bool bPrereqMet = true;
    bool bMovable = false;

    private BoxCollider2D triggerCollider;
    private Rigidbody2D rb;
    private MoveToInteractable moveToInteractable;
    
    private PlayerControls controls;

    [SerializeField]
    private int interactLayer = 0;

    [SerializeField]
    private float acceptanceRadius = 5.0f;
    [SerializeField]
    private float relMoveLoc = 0f;

    private float moveDir = 0f;
    private float speed = 0f;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.PlayerActions.Movement.performed += Move;
        controls.PlayerActions.Movement.canceled += StopMove;
        moveToInteractable = GetComponent<MoveToInteractable>();
        moveToInteractable.AssignCallback(FinishMove);
    }

    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0.0f;
        triggerCollider = GetComponent<BoxCollider2D>();
        triggerCollider.isTrigger = true;
        triggerCollider.enabled = false;
        base.Start();
        speed = PlayerMovement.Instance.GetSpeed();
    }

    private void Move(CallbackContext ctx)
    {
        moveDir = ctx.ReadValue<float>();
        if (rb.linearVelocityX != moveDir * speed)
            rb.linearVelocityX = moveDir * speed;
    }

    private void StopMove(CallbackContext ctx)
    {
        moveDir = 0;
        rb.linearVelocityX = 0;
    }

    private void FixedUpdate()
    {
        if (!bLocked && bMovable)
        {
            if (Vector3.Distance(goalLocation, transform.position) <= goalAcceptanceRadius)
            {
                SwitchMovable();
                Lock();
            }
            else if (moveDir != 0)
            {
                rb.linearVelocityX = PlayerMovement.Instance.GetVelocityX();
            }
        }

    }

    public override void SwitchLayer(int dir)
    {
        base.SwitchLayer(dir);
    }

    protected override void CheckCollider()
    {
        base.CheckCollider();
        if (scale == EScale.Large && parentLayer.GetLayerDepth() == -1)
        {
            triggerCollider.enabled = true;
        }
        else if (triggerCollider.enabled)
        {
            triggerCollider.enabled = false;
        }
    }

    public void FulfillPrereq()
    {
        moveToInteractable.SetPrereq(true);
    }
    
    private void OnDestroy()
    {
        controls.Disable();
    }

    public void SwitchMovable()
    {
        bMovable = !bMovable;
        if (bMovable)
        {
            controls.PlayerActions.Movement.Enable();
        }
        else {
            controls.PlayerActions.Movement.Disable();
            moveDir = 0;
        }
    }

    protected void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, goalAcceptanceRadius);
    }

    public void FinishMove()
    {
        Interact(null);
    }

    public void Interact(Item heldItem)
    {
        SwitchMovable();
    }

    public MoveToInteractable GetMoveToInteractable()
    {
        return moveToInteractable;
    }

    public bool CanMoveTo()
    {
        return moveToInteractable.CanMoveTo();
    }
}
