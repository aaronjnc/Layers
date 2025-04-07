using System;
using UnityEngine;

[RequireComponent(typeof(MoveToInteractable))]
[RequireComponent(typeof(MoveTo))]
public class Boulder : MonoBehaviour, InteractableInterface
{
    private MoveTo moveToLocation;
    private MoveToInteractable moveToInteract;

    private void Awake()
    {
        moveToLocation = GetComponent<MoveTo>();
        moveToInteract = GetComponent<MoveToInteractable>();
        moveToInteract.AssignCallback(Interact);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    public MoveToInteractable GetMoveToInteractable()
    {
        return moveToInteract;
    }
}
