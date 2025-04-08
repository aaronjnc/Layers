using UnityEngine;

[RequireComponent(typeof(MoveToInteractable))]
[RequireComponent(typeof(BoxCollider2D))]
public class Orb : MonoBehaviour, InteractableInterface
{
    private MoveToInteractable moveToInteractable;

    [SerializeField]
    private Vector3 lightPos = Vector3.zero;

    [SerializeField]
    private GameObject chest;

    private void Awake()
    {
        moveToInteractable = GetComponent<MoveToInteractable>();
    }
    public void AssignMoveToCallback()
    {
        moveToInteractable.AssignCallback(FinishMove);
    }

    public void FinishMove()
    {
        Interact(PlayerInventory.Instance.GetHeldItem());
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
        if (heldItem.GetItem() == EItems.Orb)
        {
            PlayerInventory.Instance.Drop();
            GameObject itemObj = heldItem.gameObject;
            Destroy(heldItem);
            Destroy(itemObj.GetComponent<Rigidbody2D>());
            Destroy(itemObj.GetComponent<LayerTraveler>());
            itemObj.transform.position = lightPos;
            chest.SetActive(true);
            Destroy(gameObject);
        }
    }
}
