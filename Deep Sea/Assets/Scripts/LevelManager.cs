using UnityEngine;
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

    // Use this for initialization
    void Start()
    {
        // Tries to find the active scene from array of scenes.
        _currentLevel = System.Array.IndexOf(_sceneNames, UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

        // If scene was found it was a legit level and the banner animation for it is shown.
        if (_currentLevel != -1)
        {
            LevelCeremony(_currentLevel);
        }

        Debug.Log("Scene name : " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

    }

    private void LevelCeremony(int currentLevel)
    {
        Debug.Log("Detected level : " + _currentLevel.ToString());
        Instantiate(_bannerPrefab).GetComponent<LevelBanner>().Initialize(currentLevel);
    }
}
