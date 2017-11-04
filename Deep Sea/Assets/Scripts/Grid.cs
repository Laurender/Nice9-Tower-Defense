using System;
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

    //will have the tower built on this piece of grid
	[SerializeField]
    GameObject currentTower;

    Animator _myAnim;

    [SerializeField]
    Sprite _mySprite;

    SpriteRenderer _myRend;

    private GridUI _gridUI;

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
#endregion properties

    void Start()
    {
        
        _myAnim = GetComponent<Animator>();
        _myRend = GetComponent<SpriteRenderer>();
        _gridUI = FindObjectOfType<GridUI>();
    }

    
    public void SetTower(GameObject tower)
    {
        if (tower.GetComponent<HatchTower>() != null)
        {
            tower.GetComponent<HatchTower>().SetRoadBools(_roadNorth, _roadEast, _roadSouth, _roadWest);
        }

        currentTower = tower;
        StopAnim();
        _gridUI.CountTowerBuild();
    }

    public void RemoveTower()
    {
        Destroy(currentTower);
        currentTower = null; // Now critical as ;
        _gridUI.CountTowerDestroy();
    }


    public void StartAnim()
    {
        _myAnim.enabled = true;
    }

    public void StopAnim()
    {
        _myAnim.enabled = false;
        _myRend.sprite = _mySprite;
    }
}
