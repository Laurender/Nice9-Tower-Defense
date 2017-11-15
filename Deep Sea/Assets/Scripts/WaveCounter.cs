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
    private static bool _allWaves;

    public static bool AllWaves
    {
        get
        {
            return _allWaves;
        }

      
    }


    // Use this for initialization
    void Start () {

        Wave[] waves = gameObject.GetComponents<Wave>();
        _totalCount = waves.Length;

        _text = _textObject.GetComponent<UnityEngine.UI.Text>();

        _text.text = _currentCount + "/" + _totalCount;

        _container.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 15, 20);
        _container.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 75, 20);

        _container.SetActive(true);

    }

    public void WaveCount()
    {
        _currentCount++;

        _text.text = _currentCount + "/" + _totalCount;

    }

    public void WaveEnd() {
        _endedCount++;
        if(_endedCount==_totalCount)
        {
            _allWaves = true;
        }
    }

   
}
