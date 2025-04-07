using UnityEngine;

[RequireComponent(typeof(MovableObject))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(MoveToInteractable))]
[RequireComponent(typeof(ObjectInteractionManager))]
public class SignPost : MonoBehaviour, InteractableInterface
{
    [SerializeField]
    private Sprite fixedSign;
    private SpriteRenderer spriteRenderer;
    private MoveToInteractable moveToInteractable;

    private void Awake()
    {
        moveToInteractable = GetComponent<MoveToInteractable>();
    }

    public MoveToInteractable GetMoveToInteractable()
    {
        return moveToInteractable;
    }

    public void Interact(Item heldItem)
    {
        throw new System.NotImplementedException();
    }

    public bool CanMoveTo()
    {
        throw new System.NotImplementedException();
    }
}
