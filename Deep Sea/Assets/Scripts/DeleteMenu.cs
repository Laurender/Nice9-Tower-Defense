using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteMenu : MonoBehaviour {

    // Using static to get data from the static function.
    private Grid _gridTemp;

    private GridUI _gridUI;

    // Use this for initialization
    void Start () {

        _gridUI = FindObjectOfType<GridUI>();
       
        
    }

    public void Open(Grid tile)
    {

        gameObject.SetActive(true);

        if (tile.OnLeft)
        {
            gameObject.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 50, 150);
            Debug.Log("Menu open on left.");
        }
        else
        {
            gameObject.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 50, 150);
            Debug.Log("Menu open on right.");
        }

        _gridTemp = tile;


    }

    // A complex function that does nothing. Well, it hides the menu.
    public void DoNothing()
    {
        gameObject.SetActive(false);
        _gridUI.ActivateGrid();
    }

    public void DeleteTower()
    {
        _gridTemp.RemoveTower();
        DoNothing();
    }

}
