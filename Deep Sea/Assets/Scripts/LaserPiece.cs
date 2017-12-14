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

	[SerializeField]
	RuntimeAnimatorController _animLaserEnd;

	[SerializeField]
	RuntimeAnimatorController _animLaserMid;

	Animator _myAnimator;

	float lifeCounter = 0.0f;

	void Awake(){
		_myAnimator = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		if (lifeCounter < _lifetime) {
			lifeCounter += Time.deltaTime;
		} else {

            MusicController.StopSolar();
			Destroy (this.gameObject);
		}
	}

	public void AsMade(string direction, string startMidEnd, float startPoint){

        MusicController.StartSolar();

		if (direction == "E") {
			transform.eulerAngles = new Vector3 (0.0f, 0.0f, 0.0f);
		} else if (direction == "N") {
			transform.eulerAngles = new Vector3 (0.0f, 0.0f, 90.0f);
		} else if (direction == "W") {
			transform.eulerAngles = new Vector3 (0.0f, 0.0f, 180.0f);
		} else {
			transform.eulerAngles = new Vector3 (0.0f, 0.0f, 270.0f);
		}
		if (startMidEnd == "S" || startMidEnd == "E") {
			_myAnimator.runtimeAnimatorController = _animLaserEnd;
			if (startMidEnd == "E") {
				Vector3 newEuler = transform.eulerAngles;
				newEuler.z -= 180.0f;
				transform.eulerAngles = newEuler;
			}
			_myAnimator.Play ("Laser_end", -1, 0.0f);
		} else {
			_myAnimator.runtimeAnimatorController = _animLaserMid;

			_myAnimator.Play ("Laser", -1, startPoint);
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
