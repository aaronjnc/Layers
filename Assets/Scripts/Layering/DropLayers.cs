using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(BoxCollider2D))]
public class DropLayers : MonoBehaviour
{
    [SerializeField]
    private Vector3 lowerPosition;

    [SerializeField]
    private float acceptanceRadius;

    [SerializeField]
    private float startX;

    [SerializeField]
    private List<Collider2D> disableColliders = new List<Collider2D>();

    PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.PlayerActions.Descend.performed += Descend;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        controls.PlayerActions.Descend.Enable();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        controls.PlayerActions.Descend.Disable();
    }

    private void Descend(CallbackContext ctx)
    {
        Vector3 goalLoc = new Vector3(startX, transform.position.y, transform.position.z);
        PlayerMovement.Instance.MoveToLocation(goalLoc, false, false, acceptanceRadius, StopMoveToDescent);
    }

    private void StopMoveToDescent()
    {
        foreach (Collider2D col in disableColliders)
        {
            col.enabled = false;
        }
        PlayerMovement.Instance.MoveToLocation(lowerPosition, false, true, acceptanceRadius, StopDescent);
    }

    private void OnDestroy()
    {
        controls.PlayerActions.Descend.Disable();
    }

    public void StopDescent()
    {
        foreach (Collider2D col in disableColliders)
        {
            col.enabled = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, acceptanceRadius);
    }
}
