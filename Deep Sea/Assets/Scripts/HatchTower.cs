using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchTower : MonoBehaviour {

	/* these will be used later. will tell if hatch can be put on various sides
	bool northHasRoad = true;
	bool eastHasRoad = true;
	bool southHasRoad = true;
	bool westHasRoad = true;
    */

	//the hatch to be dropped
	[SerializeField]
	GameObject hatch;

	//timers
	float startTimer = 5f;
	//float waveTimer = 20f;
	float timer = 0f;

	bool hasSpawned = false;
	
	// Update is called once per frame
	void Update () {
		if (timer < startTimer) {
			timer += Time.deltaTime;
		} else if (!hasSpawned){
			SpawnHatches ();
		}
	}

	void SpawnHatches(){
		Vector3 location = new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z);
		Instantiate (hatch,location,Quaternion.identity);

		location = new Vector3 (transform.position.x + 1, transform.position.y, transform.position.z);
		Instantiate (hatch,location,Quaternion.identity);

		location = new Vector3 (transform.position.x, transform.position.y - 1, transform.position.z);
		Instantiate (hatch,location,Quaternion.identity);

		location = new Vector3 (transform.position.x - 1, transform.position.y, transform.position.z);
		Instantiate (hatch,location,Quaternion.identity);

		hasSpawned = true;
	}
}
