using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour {

    [SerializeField, Tooltip("Game objects to disable when screen is touched.")]
    private GameObject[] _partsOfTransition;

    public void Close()
    {
        foreach(GameObject go in _partsOfTransition)
        {
            go.SetActive(false);
        }
    }
}
