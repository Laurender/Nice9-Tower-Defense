using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

	private Vector2 target;
	private Vector2 _direction;

	[SerializeField]
	private float _speed = 1.0f;

	[SerializeField]
	private int _damage = 1;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Translate (Vector2.right * _speed * Time.deltaTime);
	}

	public void SetTarget (Vector2 tar)
	{
		target = tar;
		_direction = (target - (Vector2)transform.position).normalized;
		transform.right = _direction;
	}

	// Handles hitting an enemy. Projectiles will only hit enemies.
	protected void OnTriggerEnter2D (Collider2D target)
	{

		Enemy e = target.gameObject.GetComponent<Enemy> ();

		// If an enemy was hit, the enemy takes damage and the projectile is destroyed.
		if (e != null) {
			e.takeDamage (_damage);
			Destroy (gameObject);
		}


		
	}
}
