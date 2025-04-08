using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LayerTraveler : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> layerScales = new List<Vector3>();

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private List<Sprite> layerSprites = new List<Sprite>();

    [SerializeField]
    private int currentLayer = 0;

    [SerializeField]
    private float shrinkSpeed = 0.5f;

    private bool bScaling = true;

    private Collider2D col;

    [SerializeField]
    private float acceptanceScale = 1f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    public void SwitchLayer(int layer)
    {
        if (layer >= layerScales.Count)
        {
            Debug.LogError("Invalid layer " + layer);
            return;
        }
        if (currentLayer == layer && gameObject.transform.lossyScale == layerScales[currentLayer])
        {
            return;
        }
        currentLayer = layer;
        bScaling = true;
        if (layerSprites.Count > layer)
        {
            spriteRenderer.sprite = layerSprites[layer];
        }
        /*if (gameObject == PlayerMovement.Instance.gameObject)
        {
            LayerTraveler[] layerTravelers = gameObject.GetComponentsInChildren<LayerTraveler>();
            foreach (LayerTraveler layerTraveler in layerTravelers)
            {
                layerTraveler.SwitchLayer(layer);
            }
        }*/
    }

    private void FixedUpdate()
    {
        if (bScaling)
        {
            float prevMinY = col.bounds.min.y;
            gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, layerScales[currentLayer], shrinkSpeed * Time.deltaTime);
            float newY = transform.position.y - (col.bounds.min.y - prevMinY);
            transform.position.Set(transform.position.x, newY, transform.position.z);
            if (Vector3.Distance(gameObject.transform.localScale, layerScales[currentLayer]) <= acceptanceScale)
            {
                gameObject.transform.localScale = layerScales[currentLayer];
                bScaling = false;
            }
        }
    }

    public int GetCurrentLayer()
    {
        return currentLayer;
    }
}
