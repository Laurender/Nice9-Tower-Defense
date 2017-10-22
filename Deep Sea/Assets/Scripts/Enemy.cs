using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

	// This field sets the speed for the enemy and should be set at the prefab!
	[SerializeField]
	private float _maxSpeed;

	private float _speed;

	// Hit points for the enemy, set here instead of in a separate component as Enemy is the only thing that has these. The base also takes damage but differently.
	[SerializeField]
	private int _hitPoints = 3;

	// The damage the enemy does if, it gets to the base.
	[SerializeField, Tooltip ("The damage the enemy does, if it gets to the base.")]
	private int _damage = 1;

	private Route _route;

	private GameObject currentHatch;

	private int _targetIndex;
	private Vector3 _target, _direction;

	SpriteRenderer mySR;
	Color myColor;

	bool isActive = false;


	// Use this for initialization
	void Start ()
	{
		_speed = _maxSpeed;
		mySR = GetComponent<SpriteRenderer> ();
		myColor = mySR.color;
		myColor.a = 0.5f;
		mySR.color = myColor;
	}
	
	// Update is called once per frame
	void Update ()
	{

		// Enemy can be moved only if it has a route, ie. was spawned in a wavr.
		if (_route) {
			MoveEnemy ();
		}

	}

	public void SetRoute (Route route)
	{

		// Stores reference to the route object.
		_route = route;

		// Gets first point, the spawn point, and places the enemy there.
		transform.position = _target = _route.GetPosition (0);

	}

	// Code for moving the enemy along the route.
	private void MoveEnemy ()
	{


		// Check if current target has been reached and update the target if necessary.
		if (Vector3.Distance (transform.position, _target) < .02f) {

			_targetIndex++;

			// Check if the end of route has been reached and do appropriate action.
			if (_route.IsEnd (_targetIndex)) {

				EndReached ();
				return;
			}

			_target = _route.GetPosition (_targetIndex);
			_direction = (_target - transform.position).normalized;

		}

		// Do actual move.

		transform.Translate (_direction * _speed * Time.deltaTime, Space.World);
		Vector3 vectorToTarget = _target - transform.position;
		float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg)+90;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime*3);


	}

	// The code for doing what needs to happen when enemy reaches the base.
	private void EndReached ()
	{

		// Currently just destroys the enemy.
		Destroy (gameObject);
	}

	public int GetTargetIndex ()
	{
		return _targetIndex;
	}

	// The enemy takes damage here and gets destroyed when hit points drop to zero.
	public void takeDamage (int damage)
	{

		_hitPoints -= damage;
		if (_hitPoints <= 0) {
			Destroy (gameObject);
		}
	}

	// Handles hitting the base.
	protected void OnTriggerEnter2D (Collider2D target)
	{

		Base b = target.gameObject.GetComponent<Base> ();

		// If the base was hit, the base takes damage and the enemy is destroyed.
		if (b != null) {
			b.takeDamage (_damage);
			Destroy (gameObject);
		}

		if (target.tag == "Start") {
			myColor.a = 1f;
			mySR.color = myColor;
			isActive = true;
		}



	}

	//When this hits a drop from hatch, it stops for some time
	public void HatchStop(GameObject hatch, float stopTime)
	{
		currentHatch = hatch;
		StartCoroutine (Stop (stopTime));
	}

	public void HatchRelease()
	{
		_speed = _maxSpeed;
		currentHatch = null;
	}

	private IEnumerator Stop(float secs)
	{
		_speed = 0f;
		yield return new WaitForSeconds (secs);
		if (currentHatch != null) {
			currentHatch.GetComponent<Hatch> ().Remove (this.gameObject);
			currentHatch = null;
		}
		_speed = _maxSpeed;
	}

	public bool IsActive(){
		return isActive;
	}
}
