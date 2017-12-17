﻿using UnityEngine;
using System.Collections;

public class MainMenuButtons : MonoBehaviour
{

    [SerializeField]
    private GameObject _buttons;

    private IEnumerator ShowMenu()
    {

        yield return new WaitForSeconds(5.2f);
        _buttons.SetActive(true);
    }



    // Use this for initialization
    void Start()
    {
        StartCoroutine(ShowMenu());
        _buttons.SetActive(false);
    }


    public void StartGame()
    {
        // Eventually should go to level select, for now just open first level.
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Tutorial_firstdraft");
    }
}
