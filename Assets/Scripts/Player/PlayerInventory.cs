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
        currentlyHeld.Pickup();
        foreach (Collider2D col in item.GetComponents<Collider2D>())
        {
            if (!col.isTrigger)
            {
                col.enabled = false;
            }
        }
        item.transform.SetParent(gameObject.transform, false);

    }

    public void Drop()
    {
        currentlyHeld.gameObject.transform.SetParent(null);
        foreach (Collider2D col in currentlyHeld.gameObject.GetComponents<Collider2D>())
        {
            if (!col.isTrigger)
            {
                col.enabled = false;
            }
        }
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
