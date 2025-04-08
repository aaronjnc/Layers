using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(BoxCollider2D))]
public class DropLayers : MonoBehaviour
{
    [SerializeField]
    protected Vector3 lowerPosition;

    [SerializeField]
    protected float acceptanceRadius;

    [SerializeField]
    protected float startX;

    [SerializeField]
    protected List<Collider2D> disableColliders = new List<Collider2D>();

    PlayerControls controls;

    protected bool bCanDescend = true;

    protected virtual void Awake()
    {
        controls = new PlayerControls();
        controls.PlayerActions.Descend.performed += Descend;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        controls?.PlayerActions.Descend.Enable();
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        controls?.PlayerActions.Descend.Disable();
    }

    protected void Descend(CallbackContext ctx)
    {
        if (!bCanDescend)
        {
            return;
        }
        Vector3 goalLoc = new Vector3(startX, PlayerMovement.Instance.transform.position.y, PlayerMovement.Instance.transform.position.z);
        PlayerMovement.Instance.MoveToLocation(goalLoc, false, false, acceptanceRadius, StopMoveToDescent);
        bCanDescend = false;
    }

    protected virtual void StopMoveToDescent()
    {
        foreach (Collider2D col in disableColliders)
        {
            col.enabled = false;
        }
        PlayerMovement.Instance.MoveToLocation(lowerPosition, false, true, acceptanceRadius, StopDescent);
    }

    protected void OnDestroy()
    {
        controls?.PlayerActions.Descend.Disable();
    }

    public virtual void StopDescent()
    {
        foreach (Collider2D col in disableColliders)
        {
            col.enabled = true;
        }
        bCanDescend = true;
    }

    protected void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, acceptanceRadius);
    }
}
