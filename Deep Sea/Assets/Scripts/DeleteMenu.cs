using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteMenu : MonoBehaviour {

    // Makes delete menu accessible from static context, causes problems if scene has more than one delete menu.
    static private GameObject _deleteMenu = null;

    // Using static to get data from the static function.
    static private Grid _gridTemp;

    // Use this for initialization
    void Start () {
        if (_deleteMenu != null)
        {
            // This might actually happen when changing scenes, but currently I am thinking to make menu separately for every level.
            Debug.LogError("Code is trying to set up a second delete menu.");
        }

        _deleteMenu = gameObject;

        _deleteMenu.SetActive(false);
    }

    static public void Open(Grid tile, bool isLeft)
    {

        _deleteMenu.SetActive(true);

        if (isLeft)
        {
            _deleteMenu.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 50, 150);
            Debug.Log("Menu open on left.");
        }
        else
        {
            _deleteMenu.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 50, 150);
            Debug.Log("Menu open on right.");
        }

        Grid.isActive = false;

        _gridTemp = tile;


    }

    // A complex function that does nothing. Well, it hides the menu.
    public void DoNothing()
    {
        _deleteMenu.SetActive(false);
        Grid.isActive = true;
    }

    public void DeleteTower()
    {
        _gridTemp.RemoveTower();
        DoNothing();
    }

}
