using UnityEngine;
using System.Collections;

public class LevelBanner : MonoBehaviour
{

    [SerializeField, Tooltip("The banner moves from x coordinate (this value) to -(this value).")]
    private float _moveLimits;

    [SerializeField]
    private float _animationDuration;

    private float _speed;

    private Vector3 _position;

    [SerializeField]
    private Sprite[] _images;

    // Use this for initialization
    void Start()
    {
        // Sets banner at starting position. Which should be outside the right edge.
        _position = gameObject.transform.localPosition;
        _position.x = _moveLimits;
        gameObject.transform.localPosition = _position;

    }

    // Update is called once per frame
    void Update()
    {
        // Move object left.
        _position.x -= _speed * Time.deltaTime;
        gameObject.transform.localPosition = _position;

        // Until banner is outside left edge and can be destroyed.
        if (_position.x < -_moveLimits)
        {
            Destroy(gameObject);
        }
    }

    public void Initialize(int level)
    {
        // Set up sprite renderer with correct graphics for the level.
        gameObject.GetComponent<SpriteRenderer>().sprite = _images[level];

        // Set movement speed;
        _speed = 2 * _moveLimits / _animationDuration;

    }
}
