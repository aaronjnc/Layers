using UnityEngine;

[RequireComponent(typeof(MoveToInteractable))]

public class Ladder : MovableObject
{
    [SerializeField]
    private GameObject finalLadder;
    [SerializeField]
    private GameObject dropLayer;

    public override void Lock()
    {
        finalLadder.SetActive(true);
        dropLayer.SetActive(true);
        Destroy(gameObject);
    }
}
