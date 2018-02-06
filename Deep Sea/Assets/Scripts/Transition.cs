using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour {

    [SerializeField, Tooltip("Game objects to disable when screen is touched.")]
    private GameObject[] _partsOfTransition;

    private static bool _closed;

    public void Awake()
    {
        foreach (GameObject go in _partsOfTransition)
        {
            go.SetActive(true);
        }

        _closed = false;

    }

    public static bool IsClosed
    {
        get { return _closed; }
       
    }
    

    public void Close()
    {

        StartCoroutine(CloseThis());

        
    }

    // Is actually closed on the mouse up AFTER the tap to avoid activating a menu.
    private IEnumerator CloseThis()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
        yield return new WaitForFixedUpdate();

        HelpBanners.ShowBuildBanner();
        foreach (GameObject go in _partsOfTransition)
        {

            go.SetActive(false);
        }

        _closed = true;
    }

 

}
