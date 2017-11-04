using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour {

    [SerializeField, Tooltip("The prefab to use for harpoon tower.")]
    private GameObject _harpoonPrefab;

    [SerializeField, Tooltip("The prefab to use for hatch tower.")]
    private GameObject _hatchPrefab;

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

		tile.StartAnim ();

        _gridTemp = tile;

        
    }

    // A complex function that does nothing. Well, it hides the menu.
    public void DoNothing()
    {
        gameObject.SetActive(false);
        _gridUI.ActivateGrid();
		_gridTemp.StopAnim ();
    }

    public void BuildHarpoonTower()
    {
        GameObject temp = Instantiate(_harpoonPrefab);
        temp.GetComponent<Transform>().position = _gridTemp.GetComponent<Transform>().position;
        _gridTemp.SetTower(temp);
        DoNothing();
      
    }

    public void BuildHatchTower()
    {
        GameObject temp = Instantiate(_hatchPrefab);
        temp.GetComponent<Transform>().position = _gridTemp.GetComponent<Transform>().position;
        _gridTemp.SetTower(temp);
        DoNothing();
    }
}
