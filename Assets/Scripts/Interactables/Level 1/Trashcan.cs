using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : MonoBehaviour
{
    [SerializeField]
    private Sprite brokenTrashcan;
    [SerializeField]
    private List<GameObject> enableGameObjects = new();

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Smash()
    {
        foreach (GameObject go in enableGameObjects)
        {
            go.SetActive(true);
        }
        spriteRenderer.sprite = brokenTrashcan;
        transform.localScale = new Vector3(.2f, .2f, 1f);
    }
}
