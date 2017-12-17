using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteMenu : MonoBehaviour
{

    private Grid _gridTemp;

    private GridUI _gridUI;

    private int _sellPrice, _upgradePrice;

    #region Serialized Fields

    [SerializeField]
    private GameObject _towerImage;

    [SerializeField]
    private GameObject _towerText;

    [SerializeField]
    private GameObject _upgradeImage;

    [SerializeField]
    private GameObject _sellImage;

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
    private Sprite _upgrade1;

    [SerializeField]
    private Sprite _upgrade2;

    [SerializeField]
    private Sprite _sell1;

    [SerializeField]
    private Sprite _sell2;

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
                    _towerImage.GetComponent<UnityEngine.UI.Image>().sprite = _harpoonIcon;
                    _towerText.GetComponent<UnityEngine.UI.Image>().sprite = _harpoonText;
                    _sellImage.GetComponent<UnityEngine.UI.Image>().sprite = _sell1;
					tile.GetTower ().GetComponent<Tower> ().ShowRange (true);
                    _sellPrice = 30;
                    break;
                }
            case Grid.TowerTypes.HatchTower:
                {
                    _towerImage.GetComponent<UnityEngine.UI.Image>().sprite = _hatchIcon;
                    _towerText.GetComponent<UnityEngine.UI.Image>().sprite = _hatchText;
                    _sellImage.GetComponent<UnityEngine.UI.Image>().sprite = _sell1;
                    _sellPrice = 40;
                    break;
                }
            case Grid.TowerTypes.LaserTower:
                {
                    _towerImage.GetComponent<UnityEngine.UI.Image>().sprite = _laserIcon;
                    _towerText.GetComponent<UnityEngine.UI.Image>().sprite = _laserText;
                    _sellImage.GetComponent<UnityEngine.UI.Image>().sprite = _sell2;
                    _sellPrice = 60;
                    break;
                }
            case Grid.TowerTypes.TeslaTower:
                {
                    _towerImage.GetComponent<UnityEngine.UI.Image>().sprite = _teslaIcon;
                    _towerText.GetComponent<UnityEngine.UI.Image>().sprite = _teslaText;
                    _sellImage.GetComponent<UnityEngine.UI.Image>().sprite = _sell2;
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
