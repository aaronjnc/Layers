using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : MonoBehaviour
{
    [SerializeField]
    private MoveTo startOrb;
    [SerializeField]
    private GameObject endOrb;
    [SerializeField]
    private MoveTo startBridge;
    [SerializeField]
    private GameObject endBridge;
    [SerializeField]
    private Sprite brokenTrashcan;

    private MoveToInteractable moveToInteractable;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        moveToInteractable = GetComponent<MoveToInteractable>();
    }

    public void Smash()
    {
        startOrb.MoveToLocation(endOrb.transform.position, true, .2f, OrbStop);
        startBridge.MoveToLocation(endBridge.transform.position, true, .2f, BridgeStop);
        spriteRenderer.sprite = brokenTrashcan;
    }

    public void OrbStop()
    {
        Destroy(startOrb.gameObject);
        endOrb.SetActive(true);
        moveToInteractable.SetPrereq(true);
    }

    public void BridgeStop()
    {
        Destroy(startBridge.gameObject);
        endBridge.SetActive(true);
    }
}
