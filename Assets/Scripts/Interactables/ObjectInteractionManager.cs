using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ObjectInteractionManager : MonoBehaviour
{
    [SerializeField]
    private List<Component> interactables = new();

    public void InteractList(Item heldItem)
    {
        foreach (InteractableInterface interactable in interactables)
        {
            interactable.Interact(heldItem);
        }
    }

    public InteractableInterface GetInteractable()
    {
        InteractableInterface interactable = interactables[0] as InteractableInterface;
        interactable.AssignMoveToCallback();
        return interactable;
    }

    public bool CanMoveTo()
    {
        return (interactables[0] as InteractableInterface).GetMoveToInteractable();
    }

    public void RemoveTop()
    {
        interactables.RemoveAt(0);
    }
}
