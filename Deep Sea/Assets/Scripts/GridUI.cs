using System;
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

    // State flags.
    private bool _aMenuIsOpen, _menuOnRight, _waitingForPair;

    
    private BuildMenu _buildMenu;
    
    private DeleteMenu _deleteMenu;

    private bool _isPaused, _isAccelerated, _gameOver, _pauseMenuOpen;

#region Serialized objects

    [SerializeField, Tooltip("Placeholder object")]
    private GameObject _placeHolder;
    [SerializeField, Tooltip("Pause menu object")]
    private GameObject _pauseMenu;
    [SerializeField, Tooltip("Game over display object")]
    private GameObject _gameOverDisplay;
    [SerializeField, Tooltip("Level pass display object")]
    private GameObject _levelPassDisplay;
    [SerializeField, Tooltip("Build menu object")]
    private GameObject _buildMenuObject;
    [SerializeField, Tooltip("Sell menu object")]
    private GameObject _sellMenuObject;

    [SerializeField]
    private Sprite _pauseSprite;

    [SerializeField]
    private Sprite _playSprite;

    [SerializeField]
    private GameObject _pausePlayGO;

    [SerializeField]
    private Sprite _speed1Sprite;

    [SerializeField]
    private Sprite _speed2Sprite;

    [SerializeField]
    private GameObject _speedyGO;
 

#endregion Serialized objects

    public bool HasTwoEnergy
    {
        get
        {
            return _popCap - _popCurrent >= 2;
        }
    }

    // Use this for initialization
    void Start()
    {
        
        _image = _energyBar.GetComponent<UnityEngine.UI.Image>();
        _image.sprite = _energyBarSprites[_popCap - _popCurrent];

        _energyBarContainer.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 15, 20);
        _energyBarContainer.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 150, 20);

        // Find the menus for later reference...
        _buildMenu = _buildMenuObject.GetComponent<BuildMenu>();
        _deleteMenu = _sellMenuObject.GetComponent<DeleteMenu>();
        

        // ...and hide most UI objects until needed. Most of these are unnecessary.
        _buildMenuObject.SetActive(false);
        _sellMenuObject.SetActive(false);
        _placeHolder.SetActive(false);
        _pauseMenu.SetActive(false);
        _gameOverDisplay.SetActive(false);
        _levelPassDisplay.SetActive(false);

        // start paused
        _isPaused = true;
        SetSpeed();
    }

    // Update is called once per frame
    void Update()
    {

        // Check for mouse and touch events.
        CheckForInputEvents();

    }

    private void CheckForInputEvents()
    {

        // Most UI does nothing if the game is over or pause menu is open.
        if (_gameOver || _pauseMenuOpen) return;

        //check for touch event
        /*if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector3 touchposition = (Vector3)Input.GetTouch (0).position;
            Vector3 worldposition = Camera.main.ScreenToWorldPoint (touchposition);
			Debug.Log ("touchposition: " + touchposition.x + " , " + touchposition.y);
			Debug.Log ("worldposition: " + worldposition.x + " , " + worldposition.y);
            ProcessTheEvent(worldposition);

            //ProcessTheEvent(Input.GetTouch(0).position);
        }*/

        //check for a click, can be removed on Android builds.
        if (Input.GetMouseButtonDown(0))
        {
            ProcessTheEvent(Input.mousePosition);
        }
    }

    private void ProcessTheEvent(Vector3 screenPoint)
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);

		Debug.Log ("worldPoint: " + worldPoint.x + " , " + worldPoint.y);

        // Close menu if click is on opposite side.
        if (_aMenuIsOpen && PointIsOnOppositeSide(worldPoint))
        {
            _buildMenu.DoNothing();
            _deleteMenu.DoNothing();

            // Avoid further processing.
            // TODO : Might want to allow open menu for the tile with same tap.
            return;
        }

        //iterate thru the array of colliders that overlap the point
        foreach (Collider2D collider in Physics2D.OverlapPointAll(worldPoint))
        {

            Grid tile = collider.gameObject.GetComponent<Grid>();

            // Open menu if clicked on a tile and one is not already open.
            if (tile != null && !_aMenuIsOpen)
            {
                
                if (_waitingForPair)
                {
                    // Check if completes the pair otherwise abort pair forming.
                    // Tiles that complete the pair should be animated.
                    if(tile.IsAnimated)
                    {
                        _buildMenu.CompletePair(tile);
                    }
                    else
                    {
                        _buildMenu.AbortPair();
                    }
                }
                else               
                {
                    // Open menu for tile, if not waiting for pair and menu is not open.
                    OpenMenu(tile);
                }
            }

            

            // TODO : the code for HUD buttons should probably go here?
        }

    }

    private bool PointIsOnOppositeSide(Vector3 worldPoint)
    {
        return _menuOnRight?worldPoint.x<0:worldPoint.x>0;
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

        if (!tile.HasTower)
        {
            //open buildmenu, if popCap allows.

            if (_popCurrent < _popCap)
            {

                _buildMenu.Open(tile);
                _aMenuIsOpen = true;
                _menuOnRight = tile.OnLeft;

            }

        }
        else
        {
            //open destroymenu

            _deleteMenu.Open(tile);
            _aMenuIsOpen = true;
            _menuOnRight = tile.OnLeft;

        }
    }

    public void ActivateGrid()
    {
        _aMenuIsOpen = false;
        _waitingForPair = false;
    }

    public void GetPair(Grid first)
    {
        _aMenuIsOpen = false;
        _waitingForPair = true;

    }

    public void PauseButton()
    {
        // Most UI does nothing if the game is over.
        if (_gameOver) return;

        if (_isPaused)
        {
            _pauseMenuOpen = false;
            _isPaused = false;
            _pauseMenu.SetActive(false);
        } else
        {
            _pauseMenuOpen = true;
            _isPaused = true;
            _pauseMenu.SetActive(true);
        }
        
        SetSpeed();

        
        //TODO: Graphics feedback.
    }

    public void SpeedButton()
    {
        _isAccelerated = _isAccelerated ? false : true;
        SetSpeed();
        
    }

    public void SetSpeed()
    {
        if (_isAccelerated)
        {
            Time.timeScale = 3;
            _speedyGO.GetComponent<UnityEngine.UI.Image>().sprite = _speed1Sprite;

        }
        else
        {
            Time.timeScale = 1;
            _speedyGO.GetComponent<UnityEngine.UI.Image>().sprite = _speed2Sprite;
        }

        if (_isPaused)
        {
            Time.timeScale = 0;
            _pausePlayGO.GetComponent<UnityEngine.UI.Image>().sprite = _playSprite;

        } else
        {
            _pausePlayGO.GetComponent<UnityEngine.UI.Image>().sprite = _pauseSprite;            
        }
    }

    // Allows exiting the game from placeholder button.
    public void Exit()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        _gameOver = true;
        _isPaused = true;
        SetSpeed();

        _gameOverDisplay.SetActive(true);

    }

    public void LevelPass()
    {
        _gameOver = true;
        _isPaused = true;
        SetSpeed();

        _levelPassDisplay.SetActive(true);

    }
}
