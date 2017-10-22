using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour {

    // Makes build menu accessible from static context, causes problems if scene has more than one build menu.
    static private GameObject _buildMenu = null;

	// Use this for initialization
	void Start () {

        if(_buildMenu != null)
        {
            // This might actually happen when changing scenes, but currently I am thinking to make menu separately for every level.
            Debug.LogError("Code is trying to set up a second build menu.");
        }

        _buildMenu = gameObject;
        _buildMenu.SetActive(false);
   
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    static public void Open(Grid tile, bool isLeft)
    {
        
        _buildMenu.SetActive(true);

        if (isLeft)
        {
            _buildMenu.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 50, 150);
            Debug.Log("Menu open on left.");
        }
        else
        {
            _buildMenu.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 50, 150);
            Debug.Log("Menu open on right.");
        }

        Grid.isActive = false;

        
    }

    // A complex function that does nothing. Well, it hides the menu.
    public void DoNothing()
    {
        _buildMenu.SetActive(false);
        Grid.isActive = true;
    }
}
