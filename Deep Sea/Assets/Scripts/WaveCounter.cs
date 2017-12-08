using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCounter : MonoBehaviour {

    [SerializeField, Tooltip("Wave counter text")]
    private GameObject _textObject;

    [SerializeField, Tooltip("Wave counter container")]
    private GameObject _container;

    private int _currentCount, _totalCount, _endedCount;
    private UnityEngine.UI.Text _text;
    private int _enemyCount;
    private Wave[] waves;

    
    public void EnemyCount(int enemies)
    {
        _enemyCount = enemies;
    }

    // Use this for initialization
    void Start () {

        waves = gameObject.GetComponents<Wave>();
        _totalCount = waves.Length;

        _text = _textObject.GetComponent<UnityEngine.UI.Text>();

        _text.text = "WAVE : "+_currentCount + "/" + _totalCount;

        //_container.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 15, 20);
        //_container.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 75, 20);

        _container.SetActive(true);

        // Trigger first wave;
        //waves[0].Trigger();

    }

    public void WaveCount()
    {
        _currentCount++;

        _text.text = _currentCount + "/" + _totalCount;

    }

    public void EnemyDied()
    {
        _enemyCount--;
        //Debug.Log(_enemyCount);
        if(_enemyCount==0)
        {
            if(_currentCount<_totalCount)
            {
                waves[_currentCount].Trigger();
            } else
            {
                FindObjectOfType<GridUI>().LevelPass();
            }
        }
    }

    // This method allows delaying the first wave until the game is first unpaused.
    public void StartWaves()
    {
        waves[0].Trigger();
    }
}
