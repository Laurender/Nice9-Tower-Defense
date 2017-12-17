﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteMenu : MonoBehaviour
{

    private Grid _gridTemp;

    private GridUI _gridUI;

    private int _sellPrice, _upgradePrice;

    #region Serialized Fields

    [SerializeField]
    private UnityEngine.UI.Image _towerImage;

    [SerializeField]
    private UnityEngine.UI.Image _towerText;

    [SerializeField]
    private UnityEngine.UI.Image _upgradeImage;

    [SerializeField]
    private UnityEngine.UI.Image _sellImage;

    [SerializeField]
    private Sprite _harpoonIcon;

    [SerializeField]
    private Sprite _harpoonText;

    [SerializeField]
    private Sprite _hatchIcon;

    [SerializeField]
    private Sprite _hatchText;

    [SerializeField]
    private Sprite _laserIcon;

    [SerializeField]
    private Sprite _laserText;

    [SerializeField]
    private Sprite _teslaIcon;

    [SerializeField]
    private Sprite _teslaText;

    [SerializeField]
    private Sprite[] _sellSprite;

    [SerializeField]
    private Sprite[] _upgradeEnabled;

    [SerializeField]
    private Sprite[] _upgradeDisabled;

    #endregion Serialized Fields

    // Use this for initialization
    void Start()
    {

        _gridUI = FindObjectOfType<GridUI>();


    }

    public void Open(Grid tile)
    {

        gameObject.SetActive(true);

        if (tile.OnLeft)
        {
            gameObject.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 50, 320);
            Debug.Log("Menu open on left.");
        }
        else
        {
            gameObject.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 50, 320);
            Debug.Log("Menu open on right.");
        }

        _gridTemp = tile;

        switch (tile.CurrenTowerType)
        {
            case Grid.TowerTypes.HarpoonTower:
                {
                    _towerImage.sprite = _harpoonIcon;
                    _towerText.sprite = _harpoonText;
                    _sellImage.sprite = _sellSprite[0];
                    _upgradeImage.sprite = (BarPanel.Money >= 60) ? _upgradeEnabled[0] : _upgradeDisabled[0];

					tile.GetTower ().GetComponent<Tower> ().ShowRange (true);
                    _sellPrice = 30;
                    break;
                }
            case Grid.TowerTypes.HatchTower:
                {
                    _towerImage.sprite = _hatchIcon;
                    _towerText.sprite = _hatchText;
                    _sellImage.sprite = _sellSprite[1];
                    _upgradeImage.sprite = (BarPanel.Money >= 80) ? _upgradeEnabled[1] : _upgradeDisabled[1];

                    _sellPrice = 40;
                    break;
                }
            case Grid.TowerTypes.LaserTower:
                {
                    _towerImage.sprite = _laserIcon;
                    _towerText.sprite = _laserText;
                    _sellImage.sprite = _sellSprite[2];
                    _upgradeImage.sprite = (BarPanel.Money >= 120) ? _upgradeEnabled[2] : _upgradeDisabled[2];

                    _sellPrice = 60;
                    break;
                }
            case Grid.TowerTypes.TeslaTower:
                {
                    _towerImage.sprite = _teslaIcon;
                    _towerText.sprite = _teslaText;
                    _sellImage.sprite = _sellSprite[2];
                    _upgradeImage.sprite = (BarPanel.Money >= 120) ? _upgradeEnabled[2] : _upgradeDisabled[2];

                    _sellPrice = 60;
                    break;
                }
        }

    }

    // A complex function that does nothing. Well, it hides the menu.
    public void CleanUp()
    {
        if (!gameObject.activeSelf) return;
		if (_gridTemp.GetTower () != null && _gridTemp.CurrenTowerType == Grid.TowerTypes.HarpoonTower) 
		{
			_gridTemp.GetTower ().GetComponent<Tower> ().ShowRange (false);
		}
       
        gameObject.SetActive(false);
        _gridUI.ActivateGrid();
    }

    public void Exit()
    {
         MusicController.PlaySound(1);
        CleanUp();
    }

    public void DeleteTower()
    {
        _gridTemp.RemoveTower();
        BarPanel.Money += _sellPrice;
        CleanUp();
    }

}
