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

	bool buildNorth = false;
	bool buildEast = false;
	bool buildSouth = false;
	bool buildWest = false;

	//the hatch to be dropped
	[SerializeField]
	GameObject hatch;

	//timers
	float startTimer = 5f;
	//float waveTimer = 20f;
	float timer = 0f;
	const float maxTimer = 5f;
	float northTimer = 5f;
	float eastTimer = 5f;
	float southTimer = 5f;
	float westTimer = 5f;

	bool hasSpawned = false;
	
	// Update is called once per frame
	void Update () {
		if (timer < startTimer) {
			timer += Time.deltaTime;
		} else if (!hasSpawned) {
			SpawnHatches ();
		} else {
			if (northTimer < maxTimer) {
				northTimer += Time.deltaTime;
			} else if(buildNorth){
				SpawnNorth ();
				buildNorth = false;
			}

			if (eastTimer < maxTimer) {
				eastTimer += Time.deltaTime;
			} else if(buildEast){
				SpawnEast ();
				buildEast = false;
			}

			if (southTimer < maxTimer) {
				southTimer += Time.deltaTime;
			} else if(buildSouth){
				SpawnSouth ();
				buildSouth = false;
			}

			if (westTimer < maxTimer) {
				westTimer += Time.deltaTime;
			} else if(buildWest){
				SpawnWest ();
				buildWest = false;
			}
		}
	}

	void SpawnHatches(){
		SpawnNorth ();

		SpawnEast ();

		SpawnSouth ();

		SpawnWest ();

		hasSpawned = true;
	}

	public void HatchDestroyed(string direction){
		if (direction == "north") {
			northTimer = 0f;
			buildNorth = true;
		} else if (direction == "east") {
			eastTimer = 0f;
			buildEast = true;
		} else if (direction == "south") {
			southTimer = 0f;
			buildSouth = true;
		} else if (direction == "west") {
			westTimer = 0f;
			buildWest = true;
		}
	}

	void SpawnNorth(){
		Vector3 location = new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z);
		GameObject tHatch = Instantiate (hatch,location,Quaternion.identity);
		tHatch.GetComponent<Hatch> ().AsMade (this.gameObject, "north");
	}

	void SpawnEast(){
		Vector3 location = new Vector3 (transform.position.x + 1, transform.position.y, transform.position.z);
		GameObject tHatch = Instantiate (hatch,location,Quaternion.identity);
		tHatch.GetComponent<Hatch> ().AsMade (this.gameObject, "east");
	}

	void SpawnSouth(){
		Vector3 location = new Vector3 (transform.position.x, transform.position.y - 1, transform.position.z);
		GameObject tHatch = Instantiate (hatch,location,Quaternion.identity);
		tHatch.GetComponent<Hatch> ().AsMade (this.gameObject, "south");
	}

	void SpawnWest(){
		Vector3 location = new Vector3 (transform.position.x - 1, transform.position.y, transform.position.z);
		GameObject tHatch = Instantiate (hatch,location,Quaternion.identity);
		tHatch.GetComponent<Hatch> ().AsMade (this.gameObject, "west");
	}
}
