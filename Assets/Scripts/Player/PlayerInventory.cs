using UnityEngine;

public class PlayerInventory : Singleton<PlayerInventory>
{
    private Item currentlyHeld;

    public void GiveItem(GameObject item)
    {
        if (currentlyHeld != null)
        {
            Drop();
        }
        currentlyHeld = item.GetComponent<Item>();
        currentlyHeld.gameObject.transform.SetParent(gameObject.transform, false);
        currentlyHeld.Pickup();
    }

    public void Drop()
    {
        currentlyHeld.gameObject.transform.SetParent(null);
        currentlyHeld.Drop();
    }

    public void Toss()
    {
        currentlyHeld.gameObject.transform.SetParent(null);
        currentlyHeld.Toss();
    }
}
