using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WikiTabsController : MonoBehaviour {

    int _currentTab;

    [SerializeField]
    private GameObject[] _tab;

    [SerializeField]
    private GameObject _story;

    [SerializeField]
    private GameObject[] _icon;

    [SerializeField]
    private Sprite[] _selectedImage;

    [SerializeField]
    private Sprite[] _deselectedImage;

  
    // Use this for initialization
    void Start () {

        // The default tab is the first, harpoon, tower.
        _currentTab = 0;
        Select(_currentTab);
       

    }

    private void Select(int tab)
    {
        
        _tab[tab].SetActive(true);
        _icon[tab].GetComponent<UnityEngine.UI.Image>().sprite = _selectedImage[tab];
    }

    private void Deselect(int tab)
    {
        _tab[tab].SetActive(false);
        _icon[tab].GetComponent<UnityEngine.UI.Image>().sprite = _deselectedImage[tab];
    }
    public void ChangeCurrentTab(int tab)
    {
        MusicController.PlaySound(1);
        Deselect(_currentTab);
        _currentTab = tab;
        Select(_currentTab);

    }

    public void ShowStory()
    {
        _story.SetActive(true);
    }

    public void HideStory()
    {
        _story.SetActive(false);
    }
}
