using System;
using UnityEngine;

public interface InteractableInterface
{
    public void Interact(Item heldItem);
    public bool CanMoveTo();
    public MoveToInteractable GetMoveToInteractable();
}
