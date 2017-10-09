using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch : MonoBehaviour {

	[SerializeField]
	int health = 5;

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Enemy") {
			health--;
			if (health <= 0) {
				Destroy (this.gameObject);
			} else {
				other.gameObject.GetComponent<Enemy> ().HatchStop();
			}
		}
	}
}
