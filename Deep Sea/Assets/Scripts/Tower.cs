using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {

	//Time in seconds between shooting projectiles
	[SerializeField]
	private float shootingSpeed;

	//timer, when == shootingSpeed, shoots a projectile
	private float shootCounter;

	//GameObject used as projectile
	[SerializeField]
	private GameObject projectile;

	Transform cannon;

	//List of enemies within collider
	[SerializeField]
	List <GameObject> enemies;

	//List that tells if enemy should be given priority when choosing a target
	[SerializeField]
	List <int> enemySide;

	//After which wayPoint enemy is considered to be behind the tower, or gets priority when choosing a target
	private int pastRouteSpot;

	//the index of current target enemy on the list enemies
	[SerializeField]
	private int target;

	// Use this for initialization
	void Start () {
		enemies = new List<GameObject> ();
		enemySide = new List<int> ();
		target = -1;
		shootCounter = 0.0f;
		cannon = transform.GetChild (0);
	}

	//if tower has a target, shoot a projectile at it with frequency based on shootingSpeed
	void Update () {
		if (target >= 0 && enemies[target].GetComponent<Enemy>().IsActive()) {
			shootCounter += Time.deltaTime;
			if (shootCounter >= shootingSpeed) {
				shootCounter = 0.0f;
				GameObject temp;
				temp = Instantiate (projectile, transform.position, Quaternion.identity);
				temp.GetComponent<Projectile> ().SetTarget ((Vector2) enemies[target].transform.position);
			}
			Vector3 vectorToTarget = enemies[target].transform.position - cannon.transform.position;
			float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg);
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
			cannon.transform.rotation = Quaternion.Slerp(cannon.transform.rotation, q, Time.deltaTime*3);
		}
	}

	//Set new target when old one leaves range
	void NewTarget(){
		int _newTarget = 0;
		//Finds the enemy that first entered towers range, prioritised by enemySide
		for (int i = 0; i < enemies.Count; i++) {
			if (enemySide [i] > enemySide[_newTarget]) {
				_newTarget = i;
			}
		}

		target = _newTarget;
	}

	//When enemy enters range, add to list enemies and check if it should be made target
	void OnTriggerEnter2D(Collider2D other){
		//Debug.Log ("Triggered");
		if (other.tag == "Enemy" /*&& other.GetComponent<Enemy>().IsActive()*/) {
			enemies.Add (other.gameObject);
			//Debug.Log ("Got in");
			if (enemies.Count == 1) {
				target = 0;
			}

			//save the targetindex of new enemy, that is how far on it's route it is
			enemySide.Add (other.GetComponent<Enemy> ().GetTargetIndex ());

			enemySide [target] = enemies [target].GetComponent<Enemy> ().GetTargetIndex ();

			//Check if new enemy is ahead of current target
			if(enemySide[enemies.IndexOf(other.gameObject)] > enemySide[target]){
				target = enemies.IndexOf (other.gameObject);
			}
		}
	}

	//When enemy leaves range, remove it from list enemies. If it was the target, find new one, unless no enemies in list enemies
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Enemy") {
			if (enemies.IndexOf (other.gameObject) == target) {
				enemySide.RemoveAt (enemies.IndexOf (other.gameObject));
				enemies.Remove (other.gameObject);
				if (enemies.Count != 0) {
					NewTarget ();
				} else {
					target = -1;
				}
			} else {
				if (enemies.IndexOf (other.gameObject) < target) {
					target--;
				}
				enemySide.RemoveAt (enemies.IndexOf (other.gameObject));
				enemies.Remove (other.gameObject);
			}
		}
	}

}
