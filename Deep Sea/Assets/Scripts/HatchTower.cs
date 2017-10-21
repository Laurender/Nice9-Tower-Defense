using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchTower : MonoBehaviour {

	[SerializeField]
	bool northHasRoad = true;

	[SerializeField]
	bool eastHasRoad = true;

	[SerializeField]
	bool southHasRoad = true;

	[SerializeField]
	bool westHasRoad = true;
    

	//these tell if different directions have room for a hatch
	bool buildNorth = false;
	bool buildEast = false;
	bool buildSouth = false;
	bool buildWest = false;

	//the hatch to be dropped
	[SerializeField]
	GameObject hatch;

	//timers
	float startTimer = 5f;
	float timer = 0f;
	const float maxTimer = 5f;
	float northTimer = 5f;
	float eastTimer = 5f;
	float southTimer = 5f;
	float westTimer = 5f;

	//tells if initial hatches have been spawned
	bool hasSpawned = false;
	
	// Update is called once per frame
	void Update () {
		//Spawn the first hatches
		if (timer < startTimer) {
			timer += Time.deltaTime;
		} else if (!hasSpawned) {
			SpawnHatches ();
		} else {

			//Spawn new hatches if there is room for them
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

	//This spawns the initial hatches.
	//Needs to be changed when we can tell if neighbouring squares have road on them
	void SpawnHatches(){
		SpawnNorth ();

		SpawnEast ();

		SpawnSouth ();

		SpawnWest ();

		hasSpawned = true;
	}

	//A destroyed hatch tells the tower a new hatch can be built
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

	//Spawns a hatch to the north of the tower
	void SpawnNorth(){
		if (northHasRoad) {
			Vector3 location = new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z);
			GameObject tHatch = Instantiate (hatch, transform.position, Quaternion.identity);
			tHatch.GetComponent<Hatch> ().AsMade (this.gameObject, "north", location);
		}
	}

	//Spawns a hatch to the east of the tower
	void SpawnEast(){
		if (eastHasRoad) {
			Vector3 location = new Vector3 (transform.position.x + 1, transform.position.y, transform.position.z);
			GameObject tHatch = Instantiate (hatch, transform.position, Quaternion.identity);
			tHatch.GetComponent<Hatch> ().AsMade (this.gameObject, "east", location);
		}
	}

	//Spawns a hatch to the south of the tower
	void SpawnSouth(){
		if (southHasRoad) {
			Vector3 location = new Vector3 (transform.position.x, transform.position.y - 1, transform.position.z);
			GameObject tHatch = Instantiate (hatch, transform.position, Quaternion.identity);
			tHatch.GetComponent<Hatch> ().AsMade (this.gameObject, "south", location);
		}
	}

	//Spawns a hatch to the west of the tower
	void SpawnWest(){
		if (westHasRoad) {
			Vector3 location = new Vector3 (transform.position.x - 1, transform.position.y, transform.position.z);
			GameObject tHatch = Instantiate (hatch, transform.position, Quaternion.identity);
			tHatch.GetComponent<Hatch> ().AsMade (this.gameObject, "west", location);
		}
	}

	public void SetRoadBools(bool nR, bool eR, bool sR, bool wR){
		northHasRoad = nR;
		eastHasRoad = eR;
		southHasRoad = sR;
		westHasRoad = wR;
	}
}
