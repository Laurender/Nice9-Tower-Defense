using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch : MonoBehaviour {

	//health
	[SerializeField]
	int health = 5;

	//chance to catch/stop enemy
	float catchChance = 0.25f;

	//amount of seconds caught enemy is stopped for
	float stopTime = 2.0f;

	//list of caught/stopped enemies.
	//used to release them when hatch is destroyed
	List<GameObject> caughtFishes;

	//pointer to the tower that spawned this hatch.
	//used to tell the tower when this hatch is destroyed
	GameObject tower;

	//string used to tell the tower in which direction from it this hatch was spawned in
	string direction;

	//tells when hatch was made. used to destroy older hatch upon collision
	float spawnTime;

	Vector3 _target;

	Vector2 _direction;

	bool _onTarget = false;

	//start function
	void Start(){
		caughtFishes = new List<GameObject>();
		spawnTime = Time.time;
	}

	//update function
	void Update(){
		/*if (health <= 0) {
			BeDestroyed ();
		}*/
		if (Vector3.Distance (transform.position, _target) < .02f) {
			transform.position = _target;
			_onTarget = true;
		} else if (!_onTarget) {
			transform.Translate (_direction * Time.deltaTime);
		}
	}

	//When enemy tries to enter hatch
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Enemy") {
			TryToCatch (other.gameObject);
		} else if (other.tag == "Hatch") {
			if (spawnTime >= other.gameObject.GetComponent<Hatch> ().GetSpawnTime ()) {
				BeDestroyed ();
			}
		}
	}

	//When enemy tries to leave the hatch
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Enemy") {
			TryToCatch(other.gameObject);
		}
	}

	//caught enemy tells when it gets out by damaging the hatch
	public void Remove(GameObject enemy){
		caughtFishes.Remove (enemy);
		health--;
		if (health <= 0) {
			BeDestroyed ();
		}
	}

	//Try to catch/stop an enemy
	public void TryToCatch(GameObject enemy){
		float rndm = Random.Range (0f, 1f);
		if (rndm <= catchChance) {
			caughtFishes.Add (enemy);
			enemy.GetComponent<Enemy> ().HatchStop (this.gameObject, stopTime);
		}
	}

	//When out of health, release all caught enemies and tell tower it can build new hatch
	void BeDestroyed(){
		for (int i = caughtFishes.Count - 1; i >= 0; i--) {
			if (caughtFishes [i] != null) {
				caughtFishes [i].GetComponent<Enemy> ().HatchRelease ();
			}
			caughtFishes.RemoveAt (i);
		}
		tower.GetComponent<HatchTower> ().HatchDestroyed (direction);
		Destroy (this.gameObject);
	}

	public float GetSpawnTime(){
		return spawnTime;
	}

	//As this hatch is made, it gets information from the tower that made it
	public void AsMade(GameObject tow, string dir, Vector2 tar){
		tower = tow;
		direction = dir;
		_target = tar;
		_direction = (_target - transform.position).normalized;
	}
}
