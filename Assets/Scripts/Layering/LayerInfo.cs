using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerInfo : MonoBehaviour
{

    public delegate void MoveLayerDelegate(int dir);
    public MoveLayerDelegate moveLayer;

    [SerializeField]
    private LayerInfo topLayer;
    [SerializeField]
    private LayerInfo bottomLayer;

    [SerializeField]
    private int currentDepth;

    private bool bActiveLayer = false;

    private void Awake()
    {
        if (currentDepth == 0)
        {
            bActiveLayer = true;
        }
        if (topLayer != null)
        {
            topLayer.moveLayer += SwitchLayer;
        }
        if (bottomLayer != null)
        {
            bottomLayer.moveLayer += SwitchLayer;
        }
    }

    public LayerInfo SwitchActiveLayer(int dir)
    {
        if ((dir < 0 && bottomLayer == null) || (dir > 0 && topLayer == null))
        {
            return this;
        }
        currentDepth += dir;
        moveLayer(dir);
        return (dir < 0) ? bottomLayer : topLayer;
    }

    public void SwitchLayer(int dir)
    {
        currentDepth += dir;
        moveLayer(dir);
    }

    public bool IsActiveLayer()
    {
        return bActiveLayer;
    }

    public int GetLayerDepth()
    {
        return currentDepth;
    }
}
