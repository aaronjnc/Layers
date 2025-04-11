using UnityEngine;

[RequireComponent(typeof(MoveToInteractable))]
[RequireComponent(typeof(ObjectFinish))]
public class FishingRod : MonoBehaviour, InteractableInterface
{
    [SerializeField]
    private MoveTo bridgePieceMove;
    [SerializeField]
    private Vector3 bridgeStopLocation = Vector3.zero;
    [SerializeField]
    private GameObject fullBridge;
    [SerializeField]
    private GameObject partBridge;
    [SerializeField]
    private float bridgeAcceptanceRadius = 0.5f;

    private MoveToInteractable moveToInteractable;

    private void Awake()
    {
        moveToInteractable = GetComponent<MoveToInteractable>();
    }

    public void FinishMove()
    {
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
        bridgePieceMove.MoveToLocation(bridgeStopLocation, true, true, bridgeAcceptanceRadius, BridgePieceDone);
    }

    public void BridgePieceDone()
    {
        Destroy(bridgePieceMove.gameObject);
        Destroy(partBridge);
        fullBridge.SetActive(true);
        GetComponent<ObjectFinish>().Finish();
    }

    public void TrashcanBurst()
    {
        moveToInteractable.SetPrereq(true);
    }
}
