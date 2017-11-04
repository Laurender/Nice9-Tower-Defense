using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour {

    [SerializeField, Tooltip("The prefab to use for harpoon tower.")]
    private GameObject _harpoonPrefab;

    [SerializeField, Tooltip("The prefab to use for hatch tower.")]
    private GameObject _hatchPrefab;

    [SerializeField, Tooltip("If advanced towers are enabled.")]
    private bool _enableAdvancedTowers;


    private Grid _gridTemp;

    private GridUI _gridUI;

    // These are needed to enable and disable.
    private UnityEngine.UI.Button _laserButton, _teslaButton;

    // Use this for initialization
    void Start () {

        _gridUI = FindObjectOfType<GridUI>();
        _laserButton = gameObject.transform.Find("LaserTowerButton").gameObject.GetComponent< UnityEngine.UI.Button>();
        _teslaButton = gameObject.transform.Find("TeslaTowerButton").gameObject.GetComponent<UnityEngine.UI.Button>();

        // Disable buttons for advanced towers unless they are enabled.
        if (!_enableAdvancedTowers)
        {
            _laserButton.interactable = false;
            _teslaButton.interactable = false;
        }
    }
	
	
    public void Open(Grid tile)
    {
        
        gameObject.SetActive(true);


        if (tile.OnLeft)
        {
            gameObject.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 50, 240);
            Debug.Log("Menu open on left.");
        }
        else
        {
            gameObject.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 50, 240);
            Debug.Log("Menu open on right.");
        }

		tile.StartAnim ();

        // Control whether the button to build laser towers is active.
        if(tile.IsPaired && _enableAdvancedTowers)
        {
            _laserButton.interactable = true;
        } else
        {
            _laserButton.interactable = false;
        }

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
