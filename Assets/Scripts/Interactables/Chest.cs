using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(MoveToInteractable))]
[RequireComponent(typeof(ObjectInteractionManager))]
public class Chest : MonoBehaviour, InteractableInterface
{
    [SerializeField]
    private Sprite newSprite;
    private MoveToInteractable moveToInteractable;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private GameObject givePlayer;

    private void Awake()
    {
        moveToInteractable = GetComponent<MoveToInteractable>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveToInteractable.AssignCallback(FinishMove);
    }

    public MoveToInteractable GetMoveToInteractable()
    {
        return moveToInteractable;
    }

    public void FinishMove()
    {
        Interact(null);
    }

    public void Interact(Item heldItem)
    {
        spriteRenderer.sprite = newSprite;
        GameObject spawnedObj = Instantiate(givePlayer);
        PlayerInventory.Instance.GiveItem(spawnedObj);
    }

    public bool CanMoveTo()
    {
        throw new System.NotImplementedException();
    }
}
