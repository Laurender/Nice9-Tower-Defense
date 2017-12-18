using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsScreen : MonoBehaviour {

    [SerializeField]
    private GameObject _credits;

	// Use this for initialization
	void Start () {
		
	}
	
	public void ReturnToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public void OpenCredits()
    {
        _credits.SetActive(true);
    }

    public void CloseCredits()
    {
        _credits.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
