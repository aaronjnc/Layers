using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
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
        moveToInteractable.AssignCallback(Interact);
    }

    public MoveToInteractable GetMoveToInteractable()
    {
        return moveToInteractable;
    }

    public void Interact()
    {
        spriteRenderer.sprite = newSprite;
        PlayerInventory.Instance.GiveItem(givePlayer);
    }
}
