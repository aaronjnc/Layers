using System;
using UnityEngine;

[RequireComponent(typeof(MoveToInteractable))]
[RequireComponent(typeof(MoveTo))]
[RequireComponent(typeof(ObjectInteractionManager))]
[RequireComponent(typeof(ObjectFinish))]
public class Boulder : MonoBehaviour, InteractableInterface
{
    private MoveTo moveToLocation;
    private MoveToInteractable moveToInteract;

    [SerializeField]
    private Vector3 goalLoc;
    
    [SerializeField]
    private float acceptanceRadius;

    [SerializeField]
    private GameObject stairs;

    [SerializeField]
    private GameObject layerTraveler;

    [SerializeField]
    private Trashcan trashCan;

    [SerializeField]
    private FishingRod fishingRod;

    [SerializeField]
    private float rotationSpeed;

    private bool bRotating = false;

    private void Awake()
    {
        moveToLocation = GetComponent<MoveTo>();
        moveToInteract = GetComponent<MoveToInteractable>();
    }

    public void FinishMove()
    {
        Interact(null);
    }

    public void Interact(Item heldItem)
    {
        bRotating = true;
        moveToLocation.MoveToLocation(goalLoc, true, acceptanceRadius, BoulderStop);
    }

    public void BoulderStop()
    {
        bRotating = false;
        stairs.SetActive(true);
        layerTraveler.SetActive(true);
        trashCan.Smash();
        fishingRod.TrashcanBurst();
        GetComponent<ObjectFinish>().Finish();
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (bRotating)
        {
            gameObject.transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
        }
    }

    public MoveToInteractable GetMoveToInteractable()
    {
        return moveToInteract;
    }

    public bool CanMoveTo()
    {
        return moveToInteract.CanMoveTo();
    }

    public void AssignMoveToCallback()
    {
        moveToInteract.AssignCallback(FinishMove);
    }
}
