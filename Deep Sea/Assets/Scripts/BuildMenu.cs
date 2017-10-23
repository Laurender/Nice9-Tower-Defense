using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour {

    // Makes build menu accessible from static context, causes problems if scene has more than one build menu.
    static private GameObject _buildMenu = null;

    // Using static to get data from the static function.
    static private Grid _gridTemp;

    [SerializeField, Tooltip("The prefab to use for harpoon tower.")]
    private GameObject _harpoonPrefab;

    [SerializeField, Tooltip("The prefab to use for hatch tower.")]
    private GameObject _hatchPrefab;

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

		tile.StartAnim ();

        _gridTemp = tile;

        
    }

    // A complex function that does nothing. Well, it hides the menu.
    public void DoNothing()
    {
        _buildMenu.SetActive(false);
        Grid.isActive = true;
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
