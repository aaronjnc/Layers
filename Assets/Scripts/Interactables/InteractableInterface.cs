using System;
using UnityEngine;

public interface InteractableInterface
{
    public void Interact(Item heldItem);
    public bool CanMoveTo();
    public void AssignMoveToCallback();
    public MoveToInteractable GetMoveToInteractable();
}
