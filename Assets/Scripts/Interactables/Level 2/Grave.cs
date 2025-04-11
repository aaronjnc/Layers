using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(MoveToInteractable))]
[RequireComponent(typeof(ObjectInteractionManager))]
[RequireComponent(typeof(ObjectFinish))]
public class Grave : MonoBehaviour, InteractableInterface
{
    private SpriteRenderer spriteRenderer;
    private MoveToInteractable moveToInteractable;

    [SerializeField]
    private Sprite fullGraveSprite;
    [SerializeField]
    private GameObject flowers;

    [SerializeField]
    private EItems requiredItem;

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
            spriteRenderer.sprite = fullGraveSprite;
            PlayerInventory.Instance.Take();
            flowers.SetActive(true);
            GetComponent<ObjectFinish>().Finish();
            Destroy(this);
        }
    }
}
