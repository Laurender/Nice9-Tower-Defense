using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;

public class Enemy : MonoBehaviour
{

	// This field sets the speed for the enemy and should be set at the prefab!
	[SerializeField]
	private float _maxSpeed;

	private float _speed;

	// Hit points for the enemy, set here instead of in a separate component as Enemy is the only thing that has these. The base also takes damage but differently.
	[SerializeField]
	private int _hitPoints = 3;

    [SerializeField, Tooltip("Armor value that protects from weaker damage.")]
    private int _armor;

    [SerializeField]
    private int _usesRoute;

    // The damage the enemy does if, it gets to the base.
    [SerializeField, Tooltip ("The damage the enemy does, if it gets to the base.")]
	private int _damage = 1;

	private Route _route;

	private GameObject currentHatch;

    [SerializeField]
    private GameObject explosionPrefab;

    [SerializeField]
    private GameObject damagePrefab;

	private int _targetIndex;
	private Vector3 _target, _direction;
    private WaveCounter _waveCounter;

	private Animator _animator;

	SpriteRenderer mySR;
	Color myColor;

	bool isActive = false;
    

    [SerializeField, Tooltip("The money added when this enemy is killed. Leave this at 0 until levels with new balance exist.")]
    private int _reward;

    // Use this for initialization
    void Start ()
	{
		_speed = _maxSpeed;
		mySR = GetComponent<SpriteRenderer> ();
		myColor = mySR.color;
		myColor.a = 0.5f;
		mySR.color = myColor;
        _waveCounter = FindObjectOfType<WaveCounter>();
		_animator = GetComponent<Animator> ();
		_animator.SetInteger ("Health", _hitPoints);

       
    }
	
	// Update is called once per frame
	void Update ()
	{

		// Enemy can be moved only if it has a route, ie. was spawned in a wavr.
		if (_route) {
			MoveEnemy ();
		}

	}

	public void SetRoute (GameObject[] routes)
	{
        Route route = routes[_usesRoute].GetComponent<Route>();

        // Check if the route is long enough to use.
        if(route.IsEnd(0))
        {
            EndReached();
            return;
        }

		// Stores reference to the route object.
		_route = route;

		// Gets first point, the spawn point, and places the enemy there.
		transform.position = _target = _route.GetPosition (0);

        // Sets the target for movement, old code did this automatically.
        _targetIndex = 1;
        _target = _route.GetPosition(_targetIndex);
        _direction = (_target - transform.position).normalized;


    }

	// Code for moving the enemy along the route.
	private void MoveEnemy ()
	{

        float distanceBefore = Vector3.Distance(transform.position, _target);
        transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
        if(Vector3.Distance(transform.position, _target)>distanceBefore)
        {
            _targetIndex++;
            transform.position = _target;

            // Check if the end of route has been reached and do appropriate action.
            if (_route.IsEnd(_targetIndex))
            {

                EndReached();
                return;
            }

            _target = _route.GetPosition(_targetIndex);
            _direction = (_target - transform.position).normalized;
        }

  //      // Check if current target has been reached and update the target if necessary.
  //      if (Vector3.Distance (transform.position, _target) < .1f) {

		//	_targetIndex++;

		//	// Check if the end of route has been reached and do appropriate action.
		//	if (_route.IsEnd (_targetIndex)) {

		//		EndReached ();
		//		return;
		//	}

		//	_target = _route.GetPosition (_targetIndex);
		//	_direction = (_target - transform.position).normalized;

		//}

		//// Do actual move.

		//transform.Translate (_direction * _speed * Time.deltaTime, Space.World);
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
        if (isActive)
        {
            // Armor negates weaker hits.
            if (damage <= _armor) return;

            _hitPoints -= damage;
            _animator.SetInteger("Health", _hitPoints);
            Instantiate(damagePrefab, transform.position, Quaternion.identity);

            if (_hitPoints <= 0)
            {
                MusicController.PlayEffect(1);
                _waveCounter.EnemyDied();
                BarPanel.Money += _reward;
				_speed = 0.0f;

				if (Social.localUser.authenticated) {
					PlayGamesPlatform.Instance.IncrementAchievement (GPGSIds.achievement_baby_fisher, 1, (bool success) => {
						//Debug.Log ("" + success);
					});
					PlayGamesPlatform.Instance.IncrementAchievement (GPGSIds.achievement_fishermans_apprentice, 1, (bool success) => {
						//Debug.Log ("" + success);
					});
					PlayGamesPlatform.Instance.IncrementAchievement (GPGSIds.achievement_professional_fisher, 1, (bool success) => {
						//Debug.Log ("" + success);
					});
					PlayGamesPlatform.Instance.IncrementAchievement (GPGSIds.achievement_defishenator, 1, (bool success) => {
						//Debug.Log ("" + success);
					});

					PlayerPrefs.SetInt ("EnemiesKilled", PlayerPrefs.GetInt ("EnemiesKilled") + 1);
					if (PlayerPrefs.GetInt ("EnemiesKilled") >= 100000) {
						PlayGamesPlatform.Instance.ReportProgress (GPGSIds.achievement_the_overfisher, 100f, (bool success) => {
							//Debug
						});
					}

					if (PlayerPrefs.GetInt ("EnemiesKilled") >= 1000000) {
						PlayGamesPlatform.Instance.ReportProgress (GPGSIds.achievement_extinction, 100f, (bool success) => {
							//Debug
						});
					}
				}

                StartCoroutine(BeDestroyed());
                isActive = false;
            }
        }
	}

	IEnumerator BeDestroyed(){
        
		yield return new WaitForSeconds (0.625f);
		Destroy (gameObject);
	}

	// Handles hitting the base.
	protected void OnTriggerEnter2D (Collider2D target)
	{

		Base b = target.gameObject.GetComponent<Base> ();

		// If the base was hit, the base takes damage and the enemy is destroyed.
		if (b != null) {
            _waveCounter.EnemyDied();
            b.takeDamage (_damage);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
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
