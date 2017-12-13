using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WikiTabsController : MonoBehaviour {

    GameObject _currentTab;

    [SerializeField]
    GameObject _tower1Tab;

    [SerializeField]
    GameObject _tower2Tab;

    [SerializeField]
    GameObject _tower3Tab;

    [SerializeField]
    GameObject _tower4Tab;

    [SerializeField]
    GameObject _enemy1Tab;

    [SerializeField]
    GameObject _enemy2Tab;


    [SerializeField]
    GameObject _enemy3Tab;

    [SerializeField]
    GameObject _enemy4Tab;




    // Use this for initialization
    void Start () {

        // The default tab is the first, harpoon, tower.
        _currentTab = _tower1Tab;
        _currentTab.SetActive(true);

    }

    public void ChangeCurrentTab(GameObject tab)
    {
        _currentTab.SetActive(false);
        _currentTab = tab;
        _currentTab.SetActive(true);
    }

    public void Tower1()
    {
        ChangeCurrentTab(_tower1Tab);
    }

    public void Tower2()
    {
        ChangeCurrentTab(_tower2Tab);
    }

    public void Tower3()
    {
        ChangeCurrentTab(_tower3Tab);
    }

    public void Tower4()
    {
        ChangeCurrentTab(_tower4Tab);
    }

    public void Enemy1()
    {
        ChangeCurrentTab(_enemy1Tab);
    }

    public void Enemy2()
    {
        ChangeCurrentTab(_enemy2Tab);
    }

    public void Enemy3()
    {
        ChangeCurrentTab(_enemy3Tab);
    }

    public void Enemy4()
    {
        ChangeCurrentTab(_enemy4Tab);
    }


}
