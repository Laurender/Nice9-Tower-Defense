using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBoundary : MonoBehaviour {

	// Destroy projectile that flies out of the level

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Projectile") {
			Destroy (other.gameObject);
		}
	}
}
