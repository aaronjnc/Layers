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

    private float maxY = 0f;

    private void Awake()
    {
        maxY = GetComponent<BoxCollider2D>().bounds.max.y;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<LayerTraveler>(out var layerTraveler))
        {
            if (collision.bounds.min.y >= maxY)
            {
                layerTraveler.SwitchLayer(layer);
            }
            else
            {
                layerTraveler.SwitchLayer(layer - 1);
            }
        }
    }
}
