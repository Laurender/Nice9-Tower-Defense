using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectScreen : MonoBehaviour {

    [SerializeField]
    private string[] _levels;

    private Vector3 _startLocation, _startPosition;
    

    // Use this for initialization
    void Start () {
		
	}
	
    public void ReturnToMenu()
    {
        MusicController.PlaySound(0);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

    public void OpenLevel(int level)
    {
        MusicController.PlaySound(0);
        UnityEngine.SceneManagement.SceneManager.LoadScene(_levels[level]);
    }

    void Update()
    {
       if(Input.GetMouseButtonDown(0))
        {
            _startLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _startPosition = gameObject.transform.localPosition;
        }

       if(Input.GetMouseButton(0))
        {
            Vector3 _currentLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float _scrollDistance = _currentLocation.x - _startLocation.x;
        }
    }
}
