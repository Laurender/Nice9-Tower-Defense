﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{

    //These tell if a direction from this grid has a piece of road next to it
    //These will be set on the editor
    [SerializeField]
    bool _roadNorth;

    [SerializeField]
    bool _roadEast;

    [SerializeField]
    bool _roadSouth;

    [SerializeField]
    bool _roadWest;

    [SerializeField]
    GameObject[] _pairedGrid;

    // will have the tower built on this piece of grid
    [SerializeField]
    GameObject currentTower;

	bool _isUpgraded = false;

    Animator _myAnim;

    [SerializeField]
    Sprite _mySprite;


    SpriteRenderer _myRend;

    private GridUI _gridUI;

    private float _startAnimationTime = 1.2f;

    private static bool _emphasizeStart, _emphasizeStop;

    private bool _ready;

    #region properties

    // If the grip tile has a tower.
    public bool HasTower
    {
        get
        {
            return currentTower != null;
        }
    }

    // Which side of the screen the grid is on. Used for menus.
    public bool OnLeft
    {
        get
        {
            return transform.position.x < 0;
        }
    }

    // Can it Laser tower
    public bool IsPaired
    {
        get
        {
            foreach (GameObject go in _pairedGrid)
            {
                if (!go.GetComponent<Grid>().HasTower) return true;
            }
            return false;
        }
    }

    public bool IsAnimated
    {
        get
        {
            return _myAnim.enabled;
        }
    }

    public enum TowerTypes { Empty, HarpoonTower, HatchTower, LaserTower, TeslaTower, Unknown };

    public TowerTypes CurrenTowerType
    {
        get
        {
            if (currentTower == null) return TowerTypes.Empty;
            if (currentTower.GetComponent<Tower>() != null) return TowerTypes.HarpoonTower;
            if (currentTower.GetComponent<HatchTower>() != null) return TowerTypes.HatchTower;
            if (currentTower.GetComponent<PairedTower>() != null) return TowerTypes.LaserTower;
            if (currentTower.GetComponent<TeslaTower>() != null) return TowerTypes.TeslaTower;
            return TowerTypes.Unknown;
        }
    }

	public bool IsUpgraded
	{
		get
		{
			return _isUpgraded;
		}
	}

    #endregion properties

    void Start()
    {

        _myAnim = GetComponent<Animator>();
        _myRend = GetComponent<SpriteRenderer>();

        _gridUI = FindObjectOfType<GridUI>();
    }

    void Update()
    {

        if (_ready) return;

        if (_emphasizeStart)
        {
            _startAnimationTime += Time.deltaTime;

            if (_startAnimationTime > 1.95f) _startAnimationTime = 0;
			if (_startAnimationTime > 0.8f && _startAnimationTime < 1.2f) _startAnimationTime = 1.2f;

            _myRend.color = new Color(_myRend.color.r, _myRend.color.g, _myRend.color.b, Mathf.Abs(_startAnimationTime-1));

        }

        if(_emphasizeStop)
        {
            _ready = true;
            _myRend.color = new Color(_myRend.color.r, _myRend.color.g, _myRend.color.b, .2f);
        }






    }

    public static void StopEmphasis()
    {
        
        _emphasizeStop = true;

    }

    public static void StartEmphasis()
    {
        _emphasizeStart = true;
        
    }

	public void SetUpgrade(bool state)
	{
		_isUpgraded = state;
	}

    #region Set, get and remove tower    
    public void SetTower(GameObject tower)
    {

        if (tower.GetComponent<HatchTower>() != null)
        {
            tower.GetComponent<HatchTower>().SetRoadBools(_roadNorth, _roadEast, _roadSouth, _roadWest);
        }

        MusicController.PlaySound(2);
        currentTower = tower;
        StopAnim();
    }

    public GameObject GetTower()
    {
        return currentTower;
    }

    public void RemoveTower(bool getPair = false)
    {
        if (!getPair && currentTower.GetComponent<PairedTower>() != null)
        {
            currentTower.GetComponent<PairedTower>().PairedTile.RemoveTower(true);
        }

 

        // Play sound only once.
        if(!getPair) MusicController.PlaySound(2);

        Destroy(currentTower);
        currentTower = null; // Now critical as ;
        Instantiate(_gridUI.SmokeEffectPrefab, transform);
    }
    #endregion

    #region Tile animation methods
    public void StartAnim()
    {
        if (!HasTower) _myAnim.enabled = true;
        // Alpha to 1
        _myRend.color = new Color(_myRend.color.r, _myRend.color.g, _myRend.color.b, 1f);
    }

    public void StopAnim()
    {
        _myAnim.enabled = false;
        _myRend.sprite = _mySprite;
        // Alpha to 25/225
        _myRend.color = new Color(_myRend.color.r, _myRend.color.g, _myRend.color.b, 0.2f);
    }

    #endregion

    #region Pair animation methods
    public void AnimatePairs()
    {
        foreach (GameObject go in _pairedGrid)
        {
            go.GetComponent<Grid>().StartAnim();
        }
    }

    public void DeAnimatePairs()
    {
        foreach (GameObject go in _pairedGrid)
        {
            go.GetComponent<Grid>().StopAnim();
        }
    }
    #endregion
}
