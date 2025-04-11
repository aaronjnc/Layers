using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private List<GameObject> finishGameObjects = new List<GameObject>();

    [SerializeField]
    private bool bLevel1;

    [SerializeField]
    private string secondScene;

    protected override void Awake()
    {
        Debug.Log("Awake Game Manager");
    }

    public void AddObject(GameObject obj)
    {
        if (finishGameObjects.Contains(obj))
        {
            return;
        }
        finishGameObjects.Add(obj);
    }

    public void FinishAction(GameObject obj)
    {
        if (finishGameObjects.Contains(obj))
        {
            finishGameObjects.Remove(obj);
        }
        if (finishGameObjects.Count == 0)
        {
            if (bLevel1)
            {
                SceneManager.LoadScene(secondScene);
            }
        }
    }
}

public enum EScale
{
    Fixed,
    Large,
    Medium,
    Small,
}