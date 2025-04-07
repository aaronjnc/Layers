using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Trashcan : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> enableGameObjects = new();

    public void Smash()
    {
        foreach (GameObject go in enableGameObjects)
        {
            go.SetActive(true);
        }
    }
}
