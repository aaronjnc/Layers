using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(MoveToInteractable))]
[RequireComponent(typeof(ObjectInteractionManager))]
public class Lantern : MonoBehaviour, InteractableInterface
{
    private MoveToInteractable moveToInteractable;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private EItems requiredItem;
    [SerializeField]
    private Sprite litLantern;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveToInteractable = GetComponent<MoveToInteractable>();
    }

    public void FinishMove()
    {
        Interact(PlayerInventory.Instance.GetHeldItem());
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
        if (heldItem != null && heldItem.GetItem() == requiredItem)
        {
            spriteRenderer.sprite = litLantern;
            PlayerInventory.Instance.Take();
            Destroy(this);
        }
    }
}
