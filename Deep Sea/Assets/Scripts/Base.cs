using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{

    // Hit points for the base, should not work this simply, probably?
    [SerializeField]
    private int _health = 4;

    [SerializeField]
    private GameObject _healthPrefab;

    [SerializeField]
    private Sprite[] _healthBarSprites;

    private GameObject _healthBar;
    private SpriteRenderer _sprRenderer;

    // Use this for initialization
    void Start()
    {
        _healthBar = Instantiate(_healthPrefab);
        _sprRenderer = _healthBar.GetComponent<SpriteRenderer>();
        _sprRenderer.sprite = _healthBarSprites[_health];
    }

    // Update is called once per frame
    void Update()
    {

    }

    // The base takes damage here and gets destroyed when hit points drop to zero.
    public void takeDamage(int damage)
    {

        _health -= damage;
        if (_health <= 0)
        {

            Destroy(gameObject);
            Destroy(_healthBar);

            // Should probably exit the game, but doesn't since we have nowhere to go!
        } else
        {
            _sprRenderer.sprite = _healthBarSprites[_health];
        }
    }
}
