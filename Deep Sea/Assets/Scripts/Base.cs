using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{

    // Hit points for the base, should not work this simply, probably?
    // Now all levels have same initial health.
    private int _health = 10;

	private Animator _animator;


    // Use this for initialization
    void Start()
    {
        BarPanel.Health = _health;
		_animator = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // The base takes damage here and gets destroyed when hit points drop to zero.
    public void takeDamage(int damage)
    {

        _health -= damage;
		_animator.SetInteger ("Health", _health);
        if (_health <= 0)
        {

            Destroy(gameObject);
            BarPanel.Health = 0;

            FindObjectOfType<GridUI>().GameOver();

        } else
        {
            BarPanel.Health = _health;

            if(_health > 1)
            {
                MusicController.PlaySound(3);
            }
            else
            {

                MusicController.PlaySound(4);
            }
        }
    }
}
