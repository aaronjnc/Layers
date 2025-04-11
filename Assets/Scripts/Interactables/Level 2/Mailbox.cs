using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(MoveToInteractable))]
[RequireComponent(typeof(ObjectInteractionManager))]
public class Mailbox : MonoBehaviour, InteractableInterface
{
    private SpriteRenderer spriteRenderer;
    private MoveToInteractable moveToInteractable;

    [SerializeField]
    private Sprite cleanedMailbox;

    [SerializeField]
    private GameObject flowers;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveToInteractable = GetComponent<MoveToInteractable>();
    }

    public void FinishMove()
    {
        Interact(null);
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
        spriteRenderer.sprite = cleanedMailbox;
        GameObject flowerObj = Instantiate(flowers);
        PlayerInventory.Instance.GiveItem(flowerObj);
        Destroy(this);
    }
}
