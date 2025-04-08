using UnityEngine;

public enum EItems
{
    Hammer,
    Orb,
}

[RequireComponent(typeof(LayerTraveler))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(MoveToInteractable))]
public class Item : MonoBehaviour, InteractableInterface
{
    [SerializeField]
    private Vector3 heldPosition = Vector3.zero;
    [SerializeField]
    private Vector3 heldRotation = Vector3.zero;
    [SerializeField]
    private EItems item;

    private MoveToInteractable moveToInteractable;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moveToInteractable = GetComponent<MoveToInteractable>();
    }

    public EItems GetItem()
    {
        return item;
    }

    public void Pickup()
    {
        rb.gravityScale = 0.0f;
        transform.localPosition = heldPosition;
        transform.localEulerAngles = heldRotation;
    }

    public void Drop()
    {
        rb.gravityScale = 1.0f;
    }

    public void Toss()
    {
        rb.gravityScale = 1.0f;
        rb.AddForce(new Vector3(500, 0, 0));
    }

    public void FinishMove()
    {
        Interact(null);
    }

    public void Interact(Item heldItem)
    {
        PlayerInventory.Instance.GiveItem(gameObject);
    }

    public bool CanMoveTo()
    {
        return moveToInteractable.CanMoveTo();
    }

    public void AssignMoveToCallback()
    {
        moveToInteractable.AssignCallback(FinishMove);
    }

    public MoveToInteractable GetMoveToInteractable()
    {
        return moveToInteractable;
    }
}
