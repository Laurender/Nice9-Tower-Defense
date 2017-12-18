﻿using UnityEngine;
using System.Collections;
using System;

// Level manager for levels.
public class LevelManager : MonoBehaviour
{
    [SerializeField, Tooltip("The level scenes in order.")]
    private string[] _sceneNames;

    private int _currentLevel;

    [SerializeField]
    private GameObject _bannerPrefab;

    [SerializeField]
    private UnityEngine.UI.Image _failLevelImage;

    [SerializeField]
    private UnityEngine.UI.Image _passLevelImage;

    [SerializeField]
    private UnityEngine.UI.Image _pauseLevelImage;

    [SerializeField]
    private Sprite[] _images;

    [SerializeField]
    private Sprite[] _imagesForPause;

    // Use this for initialization
    void Start()
    {
        // Tries to find the active scene from array of scenes.
        _currentLevel = System.Array.IndexOf(_sceneNames, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

        // If scene was found it was a legit level and the banner animation for it is shown.
        if (_currentLevel != -1)
        {
            LevelCeremony(_currentLevel);
            _failLevelImage.sprite = _images[_currentLevel];
            _passLevelImage.sprite = _images[_currentLevel];
            _pauseLevelImage.sprite = _imagesForPause[_currentLevel];
        }

        Debug.Log("Scene name : " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

    }

    private void LevelCeremony(int currentLevel)
    {
        Debug.Log("Detected level : " + _currentLevel.ToString());
        Instantiate(_bannerPrefab).GetComponent<LevelBanner>().Initialize(currentLevel);
    }

    public void OpenNextLevel()
    {
        int _nextLevel = _currentLevel + 1;

        if (_nextLevel >= _sceneNames.Length)
        {
            // Current is last level available, maybe we need to do something?
            return;
        }

        if (_sceneNames[_nextLevel].Length < 3)
        {
            // Not valid scene name. Probably means it is not ready, so do nothing.
            return;
        }

        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(_sceneNames[_nextLevel]);


    }
}