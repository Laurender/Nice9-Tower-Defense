using UnityEngine;
using System.Collections;

public class MainMenuUP : MonoBehaviour
{

    // How long the main menu 'animations' last. Set in code as editing one file is actually simpler than using editor for multiple objects.
    private float _animationTime = 5f;

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
        // Set start position
        _position = gameObject.transform.localPosition;
        _position.y = _startYPosition;
        gameObject.transform.localPosition = _position;

        _speed = (_endYPosition - _startYPosition) / _animationTime;
    }

    // Update is called once per frame
    void Update()
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
