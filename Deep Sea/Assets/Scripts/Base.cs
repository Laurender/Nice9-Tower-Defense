using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{

    // Hit points for the base, should not work this simply, probably?
    [SerializeField]
    private int _health = 4;

    [SerializeField]
    private GameObject _healthBar;

    [SerializeField]
    private Sprite[] _healthBarSprites;

    private UnityEngine.UI.Image _image;

    // Use this for initialization
    void Start()
    {
        _image = _healthBar.GetComponent<UnityEngine.UI.Image>();
        _image.sprite = _healthBarSprites[_health];
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
            _healthBar.SetActive(false);

            FindObjectOfType<GridUI>().GameOver();

        } else
        {
            _image.sprite = _healthBarSprites[_health];
        }
    }
}
