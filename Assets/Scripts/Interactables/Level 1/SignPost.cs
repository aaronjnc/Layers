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
    [SerializeField]
    private GameObject signPlatform;
    [SerializeField]
    private GameObject backSignPlatform;
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
        if (heldItem != null && heldItem.GetItem() == requiredItem)
        {
            spriteRenderer.sprite = fixedSign;
            signPlatform.SetActive(true);
            backSignPlatform.SetActive(false);
            GetComponent<ObjectInteractionManager>().RemoveTop();
            PlayerInventory.Instance.Take();
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
