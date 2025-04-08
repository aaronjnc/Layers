using UnityEngine;

[RequireComponent(typeof(MoveToInteractable))]
public class FishingRod : MonoBehaviour, InteractableInterface
{
    [SerializeField]
    private MoveTo bridgePieceMove;
    [SerializeField]
    private Vector3 bridgeStopLocation = Vector3.zero;
    [SerializeField]
    private Vector3 bridgeStopSize = Vector3.zero;
    [SerializeField]
    private float bridgeAcceptanceRadius = 0.5f;

    private MoveToInteractable moveToInteractable;

    private void Awake()
    {
        moveToInteractable = GetComponent<MoveToInteractable>();
    }

    public void FinishMove()
    {
        Debug.Log("Reached fishing rod");
        Interact(null);
    }

    public void AssignMoveToCallback()
    {
        moveToInteractable.AssignCallback(FinishMove);
    }

    public bool CanMoveTo()
    {
        return moveToInteractable.CanMoveTo();
    }

    public MoveToInteractable GetMoveToInteractable()
    {
        return moveToInteractable;
    }

    public void Interact(Item heldItem)
    {
        Debug.Log("Move bridge piece");
        bridgePieceMove.MoveToLocation(bridgeStopLocation, true, true, bridgeAcceptanceRadius, BridgePieceDone);
    }

    public void BridgePieceDone()
    {

    }

    public void TrashcanBurst()
    {
        moveToInteractable.SetPrereq(true);
    }
}
