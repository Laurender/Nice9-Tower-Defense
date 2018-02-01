using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectScreen : MonoBehaviour
{

    [SerializeField]
    private string[] _levels;

    [SerializeField, Tooltip("Scale factor between world coordinates and the transform.")]
    private float _scale;

    private Vector3 _startLocation, _startPosition;

    [SerializeField]
    private float _minX;

    [SerializeField]
    private float _maxX;

    [SerializeField, Tooltip("Level lock, the highest open level, -1 uses stored values. 1 and 6 seem like most useful values standing for reset to default and open all.")]
    private int _levelLock = -1;

    [SerializeField]
    private Sprite _lockedLevelImage;

    [SerializeField]
    private Sprite _lockedButtonImage;

    [SerializeField, Tooltip("Reset whether the story slide is shown.")]
    private bool _forceStory;

    [SerializeField]
    private GameObject _storyImage;

    // Whether the story is shown
    private bool _showStory;

    // Use this for initialization
    void Start()
    {
        // Allow resetting the level lock for debug purposes. Otherwise uses stored value.
        if(_levelLock > 0)
        {
            PlayerPrefs.SetInt("LevelLock", _levelLock);
        }
        else
        {
             _levelLock = PlayerPrefs.GetInt("LevelLock", 1);
        }

        _showStory = _forceStory || PlayerPrefs.GetInt("ShowStory", 1) == 1;

        if (_showStory)
        {
            _storyImage.SetActive(true);
            PlayerPrefs.SetInt("ShowStory", 0);
        }

        int iterator = _levelLock * 3;

        UnityEngine.UI.Image[] results = gameObject.GetComponentsInChildren<UnityEngine.UI.Image>();

        while(iterator<results.Length)
        {
            // These are depth first! It said so in the tooltip but I still got it wrong first time... and the second too.
            // Anyway the order is : Button image, the invisible larger button you can press and the level image.
            results[iterator++].sprite = _lockedLevelImage;
            results[iterator++].sprite = _lockedButtonImage;
            results[iterator++].gameObject.GetComponent<UnityEngine.UI.Button>().interactable = false;
            

            
           
        }


    }

    public void ReturnToMenu()
    {
        MusicController.PlaySound(0);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public void OpenLevel(int level)
    {
        MusicController.PlaySound(0);

        // Level lock!
        if (level > _levelLock) return;

        UnityEngine.SceneManagement.SceneManager.LoadScene(_levels[level]);
    }

    public void HideStory()
    {
        _storyImage.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _startPosition = gameObject.transform.localPosition;
            Debug.Log("Start position.");
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 _currentLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float _scrollDistance = (_currentLocation.x - _startLocation.x) * _scale;
            ChangePosition(_scrollDistance);
        }

    }

    private void ChangePosition(float scrollDistance)
    {
        Vector3 _position = _startPosition;
        _position.x += scrollDistance;

        //Debug.Log("X position : " + _position.x.ToString());

        if (_position.x > _minX || _position.x < _maxX) return;

        gameObject.transform.localPosition = _position;
    }
}
