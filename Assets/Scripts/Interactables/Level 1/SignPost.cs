using UnityEngine;

[RequireComponent(typeof(MovableObject))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(MoveToInteractable))]
[RequireComponent(typeof(ObjectInteractionManager))]
public class SignPost : MonoBehaviour, InteractableInterface
{
    [SerializeField]
    private Sprite fixedSign;
    [SerializeField]
    private EItems requiredItem;
    private SpriteRenderer spriteRenderer;
    private MoveToInteractable moveToInteractable;

    private void Awake()
    {
        moveToInteractable = GetComponent<MoveToInteractable>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public MoveToInteractable GetMoveToInteractable()
    {
        return moveToInteractable;
    }

    public void Interact(Item heldItem)
    {
        if (heldItem.GetItem() == requiredItem)
        {
            spriteRenderer.sprite = fixedSign;
            GetComponent<ObjectInteractionManager>().RemoveTop();
            Destroy(this);
        }
    }

    public bool CanMoveTo()
    {
        return moveToInteractable.CanMoveTo();
    }

    public void AssignMoveToCallback()
    {
        moveToInteractable.AssignCallback(FinishMove);
    }

    public void FinishMove()
    {
        Interact(PlayerInventory.Instance.GetHeldItem());
    }
}
