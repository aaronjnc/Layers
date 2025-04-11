using UnityEngine;

public class ObjectFinish : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.AddObject(gameObject);
    }

    public void Finish()
    {
        GameManager.Instance.FinishAction(gameObject);
    }
}
