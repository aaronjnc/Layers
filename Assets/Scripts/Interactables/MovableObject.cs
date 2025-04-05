using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(BoxCollider2D))]
public class MovableObject : LayerObject
{
    bool bMovable = false;

    private BoxCollider2D triggerCollider;
    
    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.PlayerActions.ClickAction.performed += ClickAction;
        controls.PlayerActions.ClickAction.Enable();
    }

    protected override void Start()
    {
        triggerCollider = GetComponent<BoxCollider2D>();
        triggerCollider.isTrigger = true;
        triggerCollider.enabled = false;
        base.Start();
    }

    private void ClickAction(CallbackContext ctx)
    {
        RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!hit.collider) return;

        if (hit.collider.gameObject == gameObject) 
        {
            
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
    
    private void OnDestroy()
    {
        controls.Disable();
    }
}
