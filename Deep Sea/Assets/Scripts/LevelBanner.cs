using UnityEngine;
using System.Collections;

public class LevelBanner : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _images;

    private float _animationTime = .2f;

    [SerializeField]
    private float _startYPosition;

    [SerializeField]
    private float _endYPosition;

    private Vector3 _position;

    private float _speed;

    private float _elapsedTime;

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<UnityEngine.UI.Image>().sprite = _images[LevelManager.CurrentLevel];

        // Set start position
        _position = gameObject.transform.localPosition;
        _position.y = _startYPosition;
        gameObject.transform.localPosition = _position;

        _speed = (_endYPosition - _startYPosition) / _animationTime;
    }

    private void Update()
    {

        if (_elapsedTime > _animationTime) return;

        _position.y += _speed * Time.deltaTime;
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _animationTime)
        {
            _position.y = _endYPosition;
        }

        gameObject.transform.localPosition = _position;

    }
}
