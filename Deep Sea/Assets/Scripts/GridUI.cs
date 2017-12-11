﻿using System;
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

    [SerializeField]
    private GameObject _smokeEffectPrefab;

    private WaveCounter _waveCounter;

    private UnityEngine.UI.Image _image;

    private int _popCurrent = 0;

    // State flags.
    private bool _aMenuIsOpen, _menuOnRight, _waitingForPair;

    
    private BuildMenu _buildMenu;
    
    private DeleteMenu _deleteMenu;

    private bool _isPaused, _isAccelerated, _gameOver, _pauseMenuOpen, _hasStarted;

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
    [SerializeField, Tooltip("Wiki screen object")]
    private GameObject _wikiScreen;

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

    public GameObject SmokeEffectPrefab
    {
        get
        {
            return _smokeEffectPrefab;
        }
    }

    // Use this for initialization
    void Start()
    {
        
        _image = _energyBar.GetComponent<UnityEngine.UI.Image>();
        _image.sprite = _energyBarSprites[_popCap - _popCurrent];

        _energyBarContainer.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 15, 20);
        _energyBarContainer.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 150, 20);

        // Find the menus and wave counter for later reference...
        _buildMenu = _buildMenuObject.GetComponent<BuildMenu>();
        _deleteMenu = _sellMenuObject.GetComponent<DeleteMenu>();
        _waveCounter = FindObjectOfType<WaveCounter>();
        

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

        
        if (Input.GetMouseButtonDown(0))
        {
            ProcessTheEvent(Input.mousePosition);

            // If an input is detected stop emphasizing the grid.
            Grid.StopEmphasis();
        }
    }

    private void ProcessTheEvent(Vector3 screenPoint)
    {
        // Convert to world coordinates.
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(screenPoint);

        // Close menu if click is on opposite side.
        if (_aMenuIsOpen && PointIsOnOppositeSide(worldPoint))
        {
            _buildMenu.DoNothing();
            _deleteMenu.DoNothing();

            // Avoid further processing.
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
            if(!_hasStarted)
            {
                _hasStarted = true;
                GameObject.Find("PlayButtonGlow").SetActive(false);
                _waveCounter.StartWaves();
                Debug.Log("has started");
            }
            _pauseMenuOpen = false;
            _isPaused = false;
            _pauseMenu.SetActive(false);
            _wikiScreen.SetActive(false); 
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

        if (!_hasStarted)
        {
            Time.timeScale = .5f;
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

    public void ReloadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    // The wiki opens from and closes to pause menu?
    public void OpenWiki()
    {

        
        _pauseMenu.SetActive(false);
        _wikiScreen.SetActive(true);
    }

    public void CloseWiki()
    {

   
        _pauseMenu.SetActive(true);
        _wikiScreen.SetActive(false);
    }
}
