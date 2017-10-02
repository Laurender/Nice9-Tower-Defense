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

	//List of enemies within collider
	List <GameObject> enemies;

	//List that tells if enemy should be given priority when choosing a target
	List <bool> enemySide;

	//After which wayPoint enemy is considered to be behind the tower, or gets priority when choosing a target
	private int pastRouteSpot;

	//the index of current target enemy on the list enemies
	private int target;

	// Use this for initialization
	void Start () {
		enemies = new List<GameObject> ();
		enemySide = new List<bool> ();
		target = -1;
		shootCounter = 0.0f;
	}

	//if tower has a target, shoot a projectile at it with frequency based on shootingSpeed
	void Update () {
		if (target >= 0) {
			shootCounter += Time.deltaTime;
			if (shootCounter >= shootingSpeed) {
				shootCounter = 0.0f;
				GameObject temp;
				temp = Instantiate (projectile, transform.position, Quaternion.identity);
				//Add this after making the Projectile script
				temp.GetComponent<Projectile> ().SetTarget ((Vector2) enemies[target].transform.position);
			}
		}
	}

	//Set new target when old one leaves range
	void NewTarget(){
		int _newTarget = 0;
		//Finds the enemy that first entered towers range, prioritised by enemySide
		/*for (int i = 0; i < enemies.Count; i++) {
			if (enemySide [i]) {
				_newTarget = i;
				i = enemies.Count;
			}
		}*/

		target = _newTarget;
	}

	//When enemy enters range, add to list enemies and check if it should be made target
	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("Triggered");
		if (other.tag == "Enemy") {
			enemies.Add (other.gameObject);
			Debug.Log ("Got in");
			if (enemies.Count == 1) {
				target = 0;
			}
			//We need a GetTargetIndex-function for Enemy
			/*if (other.GetComponent<Enemy> ().GetTargetIndex () >= pastRouteSpot) {
				enemySide.Add (true);
			} else {
				enemySide.Add (false);
			}*/
			//if enemySide[other] is true and enemySide[target] is false, set other as target
		}
	}

	//When enemy leaves range, remove it from list enemies. If it was the target, find new one, unless no enemies in list enemies
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Enemy") {
			if (enemies.IndexOf (other.gameObject) == target) {
				//enemySide.RemoveAt (enemies.IndexOf (other.gameObject));
				enemies.Remove (other.gameObject);
				if (enemies.Count != 0) {
					NewTarget ();
				} else {
					target = -1;
				}
			}
		}
	}

	//set pastRouteSpot. Called when tower is built, information comes from the square the tower is built on
	public void SetPastRouteSpot(int spot){
		pastRouteSpot = spot;
	}
}
