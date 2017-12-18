﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectScreen : MonoBehaviour {

    [SerializeField]
    private string[] _levels;

	// Use this for initialization
	void Start () {
		
	}
	
    public void ReturnToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public void OpenLevel(int level)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_levels[level]);
    }
	
}