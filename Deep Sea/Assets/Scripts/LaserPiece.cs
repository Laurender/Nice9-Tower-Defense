using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPiece : MonoBehaviour {

	//how much damage laser gives to enemies
	[SerializeField]
	int _damage = 1;

	//how long laser will be active
	[SerializeField]
	float _lifetime = 0.2f;

	float lifeCounter = 0.0f;
	
	// Update is called once per frame
	void Update () {
		if (lifeCounter < _lifetime) {
			lifeCounter += Time.deltaTime;
		} else {
			Destroy (this.gameObject);
		}
	}

	public void Direction(bool isHorizontal){
		if (isHorizontal) {
			transform.eulerAngles = new Vector3 (0.0f, 0.0f, 0.0f);
		} else {
			transform.eulerAngles = new Vector3 (0.0f, 0.0f, 90.0f);
		}
	}

	// Handles hitting an enemy. Projectiles will only hit enemies.
	protected void OnTriggerEnter2D (Collider2D target)
	{

		Enemy e = target.gameObject.GetComponent<Enemy> ();

		// If an enemy was hit, the enemy takes damage and the projectile is destroyed.
		if (e != null) {
			e.takeDamage (_damage);
		}
	}
}
