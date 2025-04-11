using UnityEngine;

[RequireComponent(typeof(MoveToInteractable))]
[RequireComponent(typeof(ObjectFinish))]
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
        GetComponent<ObjectFinish>().Finish();
        Destroy(gameObject);
    }
}
