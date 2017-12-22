using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCounter : MonoBehaviour
{

    [SerializeField, Tooltip("Wave counter text")]
    private GameObject _textObject;

    [SerializeField, Tooltip("Wave counter container")]
    private GameObject _container;

    [SerializeField, Tooltip("Routes available for enemies, select route in enemy prefab, if not using the first one.")]
    private GameObject[] _routes;

    [SerializeField, Tooltip("Sets the level as endless. Waves are spawned in random order without end")]
    private bool _endless;

    private int _currentCount, _totalCount, _endedCount;
    private UnityEngine.UI.Text _text;
    private int _enemyCount;
    private Wave[] waves;

    private Vector3 _destination;
    private Vector3 _slideOut;
    private Vector3 _position;
    private bool _isSlidingOut;


    public void EnemyCount(int enemies)
    {
        _enemyCount = enemies;
    }

    // Use this for initialization
    void Start()
    {

        waves = gameObject.GetComponents<Wave>();
        _totalCount = waves.Length;

        _text = _textObject.GetComponent<UnityEngine.UI.Text>();

        if (_endless)
        {
            _text.text = "ENDLESS";
        }
        else
        {
            _text.text = "WAVE : " + _currentCount.ToString() + "/" + _totalCount.ToString();

        }


        _container.SetActive(true);
        _position = _textObject.transform.localPosition;
        _destination = _position;
        _slideOut = _position + Vector3.up * 80;



    }

    public void WaveCount()
    {
        if (_endless)
        {
            _currentCount = UnityEngine.Random.Range(0, _totalCount);
        }
        else
        {
            _currentCount++;
            _isSlidingOut = true;
            _destination = _slideOut;
        }
        // _text.text = "WAVE : " + _currentCount.ToString() + "/" + _totalCount.ToString();

    }

    public void EnemyDied()
    {
        _enemyCount--;
        //Debug.Log(_enemyCount);
        if (_enemyCount == 0)
        {
            if (_currentCount < _totalCount || _endless)
            {
                if (waves[_currentCount] != null)
                // Turns out this gets called after the game is closed´, which causes 'errors' in Unity.
                {
                    waves[_currentCount].Trigger(_routes);
                }
            }
            else
            {
                FindObjectOfType<GridUI>().LevelPass();
            }
        }
    }

    // This method allows delaying the first wave until the game is first unpaused.
    public void StartWaves()
    {
        waves[0].Trigger(_routes);
    }

    void Update()
    {
        // If the object is not at the target destination
        if (_destination != _textObject.transform.localPosition)
        {
            // Move towards the destination each frame until the object reaches it
            IncrementPosition();
        }
        else
        {
            if (_isSlidingOut)
            {
                _isSlidingOut = false;
                _destination = _position;
                _text.text = "WAVE : " + _currentCount.ToString() + "/" + _totalCount.ToString();
            }
        }
    }

    void IncrementPosition()
    {
        // Calculate the next position
        float delta = 60f * Time.deltaTime;
        Vector3 currentPosition = _textObject.transform.localPosition;
        Vector3 nextPosition = Vector3.MoveTowards(currentPosition, _destination, delta);

        // Move the object to the next position
        _textObject.transform.localPosition = nextPosition;
    }
}
