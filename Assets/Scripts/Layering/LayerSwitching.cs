using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class LayerSwitching : MonoBehaviour
{
    [SerializeField]
    private int layer = 0;

    [SerializeField]
    private float playerZ = 0;

    private float maxY = 0f;

    private void Awake()
    {
        foreach (BoxCollider2D box in GetComponents<BoxCollider2D>())
        {
            if (box.isTrigger)
            {
                maxY = box.bounds.max.y;
                break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<LayerTraveler>(out var layerTraveler))
        {
            if (collision.bounds.min.y >= maxY)
            {
                layerTraveler.SwitchLayer(layer, playerZ);
                PlayerInventory.Instance.SwitchLayer(layer);
            }
            else
            {
                layerTraveler.SwitchLayer(layer - 1, playerZ - 1);
                PlayerInventory.Instance.SwitchLayer(layer - 1);
            }
        }
    }
}
