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

    // Use this for initialization
    void Start()
    {

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

        Debug.Log("X position : " + _position.x.ToString());

        if (_position.x > _minX || _position.x < _maxX) return;

        gameObject.transform.localPosition = _position;
    }
}
