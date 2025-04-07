using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ObjectInteractionManager : MonoBehaviour
{
    [SerializeField]
    private List<InteractableInterface> interactables = new();

    public void InteractList(Item heldItem)
    {
        foreach (InteractableInterface interactable in interactables)
        {
            interactable.Interact(heldItem);
        }
    }
}
