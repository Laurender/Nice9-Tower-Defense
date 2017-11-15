using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour {

#region Serialized fields
    [SerializeField, Tooltip("The prefab to use for harpoon tower.")]
    private GameObject _harpoonPrefab;

    [SerializeField, Tooltip("The prefab to use for hatch tower.")]
    private GameObject _hatchPrefab;

    [SerializeField, Tooltip("The prefab to use for laser tower.")]
    private GameObject _laserPrefab;

    [SerializeField, Tooltip("The prefab to use for Tesla tower.")]
    private GameObject _teslaPrefab;

    [SerializeField, Tooltip("If hatch towers are enabled.")]
    private bool _enableHatchTowers;

    [SerializeField, Tooltip("If laser towers are enabled.")]
    private bool _enableLaserTowers;

    [SerializeField, Tooltip("If tesla towers are enabled.")]
    private bool _enableTeslaTowers;

    [SerializeField, Tooltip("Hatch tower button.")]
    private UnityEngine.UI.Button _hatchTowerButton;

    [SerializeField, Tooltip("Laser tower button.")]
    private UnityEngine.UI.Button _laserTowerButton;

    [SerializeField, Tooltip("Tesla tower button.")]
    private UnityEngine.UI.Button _teslaTowerButton;

    #endregion

    #region Paired tower state
    // Extra state needed for the paired towers.
    private GameObject _firstTower;
    private Grid _firstGrid;
#endregion

    private Grid _gridTemp;

    private GridUI _gridUI;

    // Use this for initialization
    void Start () {

        _gridUI = FindObjectOfType<GridUI>();

        // Disable buttons for advanced towers unless they are enabled.
        _hatchTowerButton.interactable = _enableHatchTowers;
        _laserTowerButton.interactable = _enableLaserTowers;
        _teslaTowerButton.interactable = _enableTeslaTowers;






    }
	
	
    public void Open(Grid tile)
    {
        
        gameObject.SetActive(true);


        if (tile.OnLeft)
        {
            gameObject.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 50, 260);
            Debug.Log("Menu open on left.");
        }
        else
        {
            gameObject.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 50, 260);
            Debug.Log("Menu open on right.");
        }

		tile.StartAnim ();

        // Control whether the button to build laser towers is active.
        if(tile.IsPaired && _enableLaserTowers && _gridUI.HasTwoEnergy)
        {
            _laserTowerButton.interactable = true;
        } else
        {
            _laserTowerButton.interactable = false;
        }

        // Control button for tesla towers.
        if(_enableTeslaTowers&&_gridUI.HasTwoEnergy)
        {
            _teslaTowerButton.interactable = true;
        } else
        {
            _teslaTowerButton.interactable = false;
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
        BasicTowerBuild(_harpoonPrefab);
 
      
    }

    public void BuildHatchTower()
    {
        BasicTowerBuild(_hatchPrefab);
 
    }

    public void BuildLaserTower()
    {
        Debug.Log("Building laser tower.");
        // Store state for first of pair.
        _firstTower = Instantiate(_laserPrefab);
        _firstGrid = _gridTemp;

        _firstTower.GetComponent<Transform>().position = _gridTemp.GetComponent<Transform>().position;
        _gridTemp.SetTower(_firstTower);

        // Set up to get the pair.
        gameObject.SetActive(false);
        _gridUI.GetPair(_gridTemp);
        _gridTemp.StopAnim();
        _gridTemp.AnimatePairs();

    }

    public void CompletePair(Grid tile)
    {
        GameObject temp = Instantiate(_laserPrefab);
        temp.GetComponent<Transform>().position = tile.GetComponent<Transform>().position;
        tile.SetTower(temp);

        _firstTower.GetComponent<PairedTower>().WhenBuilt(false, temp, tile.gameObject);
        temp.GetComponent<PairedTower>().WhenBuilt(true, _firstTower, _firstGrid.gameObject);

        _firstGrid.DeAnimatePairs();
        _gridUI.ActivateGrid();

    }

    // Use stored state to reset back to normal.
    public void AbortPair()
    {
        _firstGrid.RemoveTower(false);
        _firstGrid.DeAnimatePairs();

        _gridUI.ActivateGrid();
    }

 

    public void BuildTeslaTower()
    {
        // This tower consumes extra energy.
        _gridUI.CountTowerBuild();

        BasicTowerBuild(_teslaPrefab);       

    }

    private void BasicTowerBuild(GameObject preFab)
    {
        GameObject temp = Instantiate(preFab);
        temp.GetComponent<Transform>().position = _gridTemp.GetComponent<Transform>().position;
        _gridTemp.SetTower(temp);

        DoNothing();
    }
}
