using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{

	// Hit points for the base, should not work this simply, probably?
	[SerializeField]
	private int _hitPoints = 3;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	// The base takes damage here and gets destroyed when hit points drop to zero.
	public void takeDamage (int damage)
	{

		_hitPoints -= damage;
		if (_hitPoints <= 0) {
			Destroy (gameObject);

			// Should probably exit the game, but doesn't since we have nowhere to go!
		}
	}
}
