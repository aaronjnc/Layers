using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Sprites;

public class LayerObject : MonoBehaviour
{

    protected LayerInfo parentLayer;
    [SerializeField]
    protected EScale scale;

    [SerializeField]
    protected Vector3 goalLocation = Vector3.zero;

    [SerializeField]
    protected float goalAcceptanceRadius = 0f;

    protected bool bLocked = false;

    [SerializeField]
    private List<Component> destroyComponents = new();

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
    }

    public virtual void Lock()
    {
        transform.position = new Vector3(goalLocation.x, goalLocation.y, transform.position.z);
        bLocked = true;
        Destroy(this);
        foreach (Component c in destroyComponents)
        {
            Destroy(c);
        }
    }
}
