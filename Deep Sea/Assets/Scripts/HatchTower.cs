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

	//tells if a Hatch is currently moving to it's place, preventing others from spawning
	bool building = false;

	//the hatch to be dropped
	[SerializeField]
	GameObject hatch;

	//the hatch dropped when tower is upgraded
	[SerializeField]
	GameObject uHatch;

	//timers
	float startTimer = 5f;
	float timer = 0f;
	//const float maxTimer = 5f;
	float northTimer = 15f;
	float eastTimer = 15f;
	float southTimer = 15f;
	float westTimer = 15f;
	float northTimerMax = 15f;
	float eastTimerMax = 15f;
	float southTimerMax = 15f;
	float westTimerMax = 15f;
	float moveTimer = 1.5f;

	//tells if initial hatches have been spawned
	bool hasSpawned = false;
	
	// Update is called once per frame
	void Update () {
		//Spawn the first hatches
		if (timer < startTimer) {
			timer += Time.deltaTime;
		} else if (!hasSpawned) {
			StartCoroutine(SpawnHatches ());
			hasSpawned = true;
		} else {

			//Spawn new hatches if there is room for them
			if (northTimer < northTimerMax) {
				northTimer += Time.deltaTime;
			} else if(buildNorth && !building){
				StartCoroutine(SpawnNorth ());
				buildNorth = false;
			}

			if (eastTimer < eastTimerMax) {
				eastTimer += Time.deltaTime;
			} else if(buildEast && !building){
				StartCoroutine(SpawnEast ());
				buildEast = false;
			}

			if (southTimer < southTimerMax) {
				southTimer += Time.deltaTime;
			} else if(buildSouth && !building){
				StartCoroutine(SpawnSouth ());
				buildSouth = false;
			}

			if (westTimer < westTimerMax) {
				westTimer += Time.deltaTime;
			} else if(buildWest && !building){
				StartCoroutine(SpawnWest ());
				buildWest = false;
			}
		}
	}

	//This spawns the initial hatches.
	//Needs to be changed when we can tell if neighbouring squares have road on them
	/*void SpawnHatches(){
		SpawnNorth ();

		SpawnEast ();

		SpawnSouth ();

		SpawnWest ();

		hasSpawned = true;
	}*/

	IEnumerator SpawnHatches(){
		building = true;
		StartCoroutine (SpawnNorth());
		do {
			yield return new WaitForSeconds (0.1f);
		} while (building);
		building = true;
		StartCoroutine (SpawnEast());
		do {
			yield return new WaitForSeconds (0.1f);
		} while (building);
		building = true;
		StartCoroutine (SpawnSouth());
		do {
			yield return new WaitForSeconds (0.1f);
		} while (building);
		building = true;
		StartCoroutine (SpawnWest());
	}

	//A destroyed hatch tells the tower a new hatch can be built
	public void HatchDestroyed(string direction){
		if (direction == "north") {
			northTimer = 0f;
			northTimerMax = Random.Range (10f, 15f);
			buildNorth = true;
		} else if (direction == "east") {
			eastTimer = 0f;
			eastTimerMax = Random.Range (10f, 15f);
			buildEast = true;
		} else if (direction == "south") {
			southTimer = 0f;
			southTimerMax = Random.Range (10f, 15f);
			buildSouth = true;
		} else if (direction == "west") {
			westTimer = 0f;
			westTimerMax = Random.Range (10f, 15f);
			buildWest = true;
		}
	}

	//Spawns a hatch to the north of the tower
	/*void SpawnNorth(){
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
	}*/

	IEnumerator SpawnNorth(){
		building = true;
		if (northHasRoad) {
			Vector3 location = new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z);
			GameObject tHatch = Instantiate (hatch, transform.position, Quaternion.identity);
			tHatch.GetComponent<Hatch> ().AsMade (this.gameObject, "north", location);
			yield return new WaitForSeconds (moveTimer);
		}
		building = false;
	}

	IEnumerator SpawnEast(){
		building = true;
		if (eastHasRoad) {
			Vector3 location = new Vector3 (transform.position.x + 1, transform.position.y, transform.position.z);
			GameObject tHatch = Instantiate (hatch, transform.position, Quaternion.identity);
			tHatch.GetComponent<Hatch> ().AsMade (this.gameObject, "east", location);
			yield return new WaitForSeconds (moveTimer);
		}
		building = false;
	}

	IEnumerator SpawnSouth(){
		building = true;
		if (southHasRoad) {
			Vector3 location = new Vector3 (transform.position.x, transform.position.y - 1, transform.position.z);
			GameObject tHatch = Instantiate (hatch, transform.position, Quaternion.identity);
			tHatch.GetComponent<Hatch> ().AsMade (this.gameObject, "south", location);
			yield return new WaitForSeconds (moveTimer);
		}
		building = false;
	}

	IEnumerator SpawnWest(){
		building = true;
		if (westHasRoad) {
			Vector3 location = new Vector3 (transform.position.x - 1, transform.position.y, transform.position.z);
			GameObject tHatch = Instantiate (hatch, transform.position, Quaternion.identity);
			tHatch.GetComponent<Hatch> ().AsMade (this.gameObject, "west", location);
			yield return new WaitForSeconds (moveTimer);
		}
		building = false;
	}

	public void SetRoadBools(bool nR, bool eR, bool sR, bool wR){
		northHasRoad = nR;
		eastHasRoad = eR;
		southHasRoad = sR;
		westHasRoad = wR;
	}

	public void UpgradeTower(){
		hatch = uHatch;
	}
}
