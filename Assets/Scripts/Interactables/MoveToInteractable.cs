using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MoveToInteractable : MonoBehaviour
{
    [SerializeField]
    private bool bAutomateApproach;

    [SerializeField]
    private float acceptanceRadius;

    [SerializeField]
    private Vector3 relMoveLoc = Vector3.zero;

    [SerializeField]
    private bool bFlipApproach = false;

    public Action callback { get; protected set; }

    public Predicate<PlayerMovement> canMoveTo { get; protected set;}
    [SerializeField]
    private int interactLayer;

    [SerializeField]
    public bool bPrereqMet = false;

    public bool GetPrereq()
    {
        return bPrereqMet;
    }

    public bool isApproachAutomated()
    {
        return bAutomateApproach;
    }

    public float GetAcceptanceRadius()
    {
        return acceptanceRadius;
    }

    public Vector3 GetMoveLoc()
    {
        Vector3 moveLoc = gameObject.transform.position + relMoveLoc;
        if (bFlipApproach && PlayerMovement.Instance.gameObject.transform.position.x < transform.position.x)
        {
            moveLoc.x = gameObject.transform.position.x - relMoveLoc.x;
        }
        if (relMoveLoc.x == 0)
        {
            moveLoc.x = PlayerMovement.Instance.gameObject.transform.position.x;
        }
        if (relMoveLoc.y == 0)
        {
            moveLoc.y = PlayerMovement.Instance.gameObject.transform.position.y;
        }
        moveLoc.z = PlayerMovement.Instance.transform.position.z;
        return moveLoc;
    }

    public void AssignCallback(Action newCallback)
    {
        callback = newCallback;
    }

    public void SetPrereq(bool newPrereq)
    {
        bPrereqMet = newPrereq;
    }

    public bool CanMoveTo()
    {
        return PlayerMovement.Instance.GetPlayerLayer() == interactLayer && bPrereqMet;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + relMoveLoc, acceptanceRadius);
    }
}
