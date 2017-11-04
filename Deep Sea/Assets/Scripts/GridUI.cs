using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUI : MonoBehaviour
{

    [SerializeField, Tooltip("Maximum number of towers.")]
    private int _popCap = 6;

    [SerializeField, Tooltip("Energy bar sprites.")]
    private Sprite[] _energyBarSprites;

    [SerializeField, Tooltip("Energy bar container")]
    private GameObject _energyBarContainer;

    [SerializeField, Tooltip("Energy bar")]
    private GameObject _energyBar;

    private UnityEngine.UI.Image _image;

    private int _popCurrent = 0;
    private bool _aMenuIsOpen;
    private bool _menuOnLeft;
    private BuildMenu _buildMenu;
    private DeleteMenu _deleteMenu;

    // Use this for initialization
    void Start()
    {
        
        _image = _energyBar.GetComponent<UnityEngine.UI.Image>();
        _image.sprite = _energyBarSprites[_popCap - _popCurrent];

        _energyBarContainer.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 15, 20);
        _energyBarContainer.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 150, 20);

        // Find the menus for later reference...
        _buildMenu = FindObjectOfType<BuildMenu>();
        _deleteMenu = FindObjectOfType<DeleteMenu>();

        // ...and hide them until needed.
        _buildMenu.gameObject.SetActive(false);
        _deleteMenu.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        // Check for mouse and touch events.
        CheckForInputEvents();

    }

    private void CheckForInputEvents()
    {
        //check for touch event
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ProcessTheEvent(Input.GetTouch(0).position);
        }

        //check for a click, can be removed on Android builds.
        if (Input.GetMouseButtonDown(0))
        {
            ProcessTheEvent(Input.mousePosition);
        }
    }

    private void ProcessTheEvent(Vector3 wp)
    {

        //iterate thru the array of colliders that overlap the point
        foreach (Collider2D collider in Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(wp)))
        {

            Grid grid = collider.gameObject.GetComponent<Grid>();

            if (grid != null)
            {
                OpenMenu(grid);
            }

            // TODO : the code for HUD buttons should probably go here?
        }

    }

    public void CountTowerBuild()
    {
        _popCurrent++;
        _image.sprite = _energyBarSprites[_popCap - _popCurrent];
    }

    public void CountTowerDestroy()
    {
        _popCurrent--;
        _image.sprite = _energyBarSprites[_popCap - _popCurrent];
    }

	//Increases pop cap, allowing the player to build more towers
	//Does not allow pop cap to be greater than 6
	public void IncreasePopCap(){
		if (_popCap < 6) {
			_popCap++;
			_image.sprite = _energyBarSprites[_popCap - _popCurrent];
		}
	}

    private void OpenMenu(Grid tile)
    {

        // If a menu is already open, do not open another.
        if (_aMenuIsOpen)
        {
            // If click was on the opposite side, close any open menus.
            if (tile.OnLeft == _menuOnLeft)
            {
                _buildMenu.DoNothing();
                _deleteMenu.DoNothing();
            }

            return;
        }

        if (!tile.HasTower)
        {
            //open buildmenu, if popCap allows.

            if (_popCurrent < _popCap)
            {
                
                _buildMenu.Open(tile);
                _aMenuIsOpen = true;
                _menuOnLeft = tile.OnLeft;
 
            }

        }
        else
        {
            //open destroymenu

            _deleteMenu.Open(tile);
            _aMenuIsOpen = true;
            _menuOnLeft = tile.OnLeft;

        }
    }

    public void ActivateGrid()
    {
        _aMenuIsOpen = false;
    }

}
