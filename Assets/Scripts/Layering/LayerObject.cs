using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Sprites;

[RequireComponent(typeof(PolygonCollider2D))]
public class LayerObject : MonoBehaviour
{

    protected LayerInfo parentLayer;
    [SerializeField]
    protected EScale scale;

    protected PolygonCollider2D col;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        parentLayer = GetComponentInParent<LayerInfo>();
        parentLayer.moveLayer += SwitchLayer;
        CheckCollider();
    }

    public virtual void SwitchLayer(int dir)
    {
        CheckCollider();
    }

    protected virtual void CheckCollider()
    {
        if (parentLayer.GetLayerDepth() >= 0 && col.enabled == false)
        {
            col.enabled = true;
        }
        else if (parentLayer.GetLayerDepth() < 0 && col.enabled == true)
        {
            col.enabled = false;
        }
    }
}
