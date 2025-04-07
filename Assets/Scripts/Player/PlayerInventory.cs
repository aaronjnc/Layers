using UnityEngine;

public class PlayerInventory : Singleton<PlayerInventory>
{
    private Item currentlyHeld;

    public Item GetHeldItem() { return currentlyHeld; }

    public void GiveItem(GameObject item)
    {
        if (currentlyHeld != null)
        {
            Drop();
        }
        currentlyHeld = item.GetComponent<Item>();
        item.transform.SetParent(gameObject.transform, false);
        currentlyHeld.Pickup();
    }

    public void Drop()
    {
        currentlyHeld.gameObject.transform.SetParent(null);
        currentlyHeld.Drop();
        currentlyHeld = null;
    }

    public void Toss()
    {
        currentlyHeld.gameObject.transform.SetParent(null);
        currentlyHeld.Toss();
        currentlyHeld = null;
    }
}
