using System;
using UnityEngine;

[RequireComponent(typeof(MoveToInteractable))]
[RequireComponent(typeof(MoveTo))]
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
    private float rotationSpeed;

    private bool bRotating = false;

    private void Awake()
    {
        moveToLocation = GetComponent<MoveTo>();
        moveToInteract = GetComponent<MoveToInteractable>();
        moveToInteract.AssignCallback(Interact);
    }

    public void Interact()
    {
        bRotating = true;
        moveToLocation.MoveToLocation(goalLoc, true, acceptanceRadius, BoulderStop);
    }

    public void BoulderStop()
    {
        bRotating = false;
        stairs.SetActive(true);
        layerTraveler.SetActive(true);
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
}
