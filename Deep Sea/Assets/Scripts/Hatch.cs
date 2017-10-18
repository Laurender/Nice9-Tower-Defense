using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch : MonoBehaviour {

	[SerializeField]
	int health = 5;

	float catchChance = 0.25f;

	float stopTime = 2.0f;

	List<GameObject> caughtFishes;

	GameObject tower;

	string direction;

	void Start(){
		caughtFishes = new List<GameObject>();
	}

	void Update(){
		if (health <= 0) {
			BeDestroyed ();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Enemy") {
			TryToCatch(other.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Enemy") {
			TryToCatch(other.gameObject);
		}
	}

	public void Remove(GameObject enemy){
		caughtFishes.Remove (enemy);
		health--;
		if (health <= 0) {
			BeDestroyed ();
		}
	}

	public void TryToCatch(GameObject enemy){
		float rndm = Random.Range (0f, 1f);
		if (rndm <= catchChance) {
			caughtFishes.Add (enemy);
			enemy.GetComponent<Enemy> ().HatchStop (this.gameObject, stopTime);
		}
	}

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

	public void AsMade(GameObject tow, string dir){
		tower = tow;
		direction = dir;
	}
}
