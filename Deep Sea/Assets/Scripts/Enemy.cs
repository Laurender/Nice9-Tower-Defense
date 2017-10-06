using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	// This field sets the speed for the enemy and should be set at the prefab!
	[SerializeField]
	private float _speed;

	// Hit points for the enemy, set here instead of in a separate component as Enemy is the only thing that has these. The base also takes damage but differently.
	[SerializeField]
	private int _hitPoints = 3;

	private Route _route;

	private int _targetIndex;
	private Vector3 _target, _direction;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		// Enemy can be moved only if it has a route, ie. was spawned in a wavr.
		if (_route) {
			MoveEnemy ();
		}

	}

	public void SetRoute(Route route) {

		// Stores reference to the route object.
		_route = route;

		// Gets first point, the spawn point, and places the enemy there.
		transform.position = _target = _route.GetPosition(0);

	}

	// Code for moving the enemy along the route.
	private void MoveEnemy() {


		// Check if current target has been reached and update the target if necessary.
		if (Vector3.Distance (transform.position, _target) < .2f) {

			_targetIndex++;

			// Check if the end of route has been reached and do appropriate action.
			if (_route.IsEnd(_targetIndex)) {

				EndReached ();
				return;
			}

			_target = _route.GetPosition (_targetIndex);
			_direction = (_target - transform.position).normalized;

		}

		// Do actual move.

		transform.Translate (_direction * _speed * Time.deltaTime);

	}

	// The code for doing what needs to happen when enemy reaches the base.
	private void EndReached(){

		// Currently just destroys the enemy.
		Destroy (gameObject);
	}

	public int GetTargetIndex(){
		return _targetIndex;
	}

	// The enemy takes damage here and gets destroyed when hit points drop to zero.
	public void takeDamage(int damage) {

		_hitPoints -= damage;
		if (_hitPoints <= 0) {
			Destroy (gameObject);
		}
	}


}
