using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PairedTower : MonoBehaviour {

	//The tower built at the same time, that is paired with this one
	[SerializeField]
	GameObject _towerPair;

	//The grid across the road from this one
	[SerializeField]
	GameObject _gridPair;

	//Time in seconds between shooting projectiles
	[SerializeField]
	private float shootingSpeed;

	//timer, when == shootingSpeed, shoots a projectile
	private float shootCounter = 0.0f;

	//The laser shot by this tower
	[SerializeField]
	private GameObject projectile;

	//tells if this tower is about to shoot a laser
	[SerializeField]
	bool preparingToShoot = false;

	//tells if this tower is expecting a laser to hit it
	//Needed so the tower doesn't destroy the laser it itself shot
	[SerializeField]
	bool expectingLaser = false;
	
	// Update is called once per frame. Contains the shooting of a laser
	void Update () {
		if (preparingToShoot) {
			shootCounter += Time.deltaTime;
			if (shootCounter >= shootingSpeed) {
				shootCounter = 0.0f;
				GameObject temp;
				temp = Instantiate (projectile, transform.position, Quaternion.identity);
				temp.GetComponent<LaserProjectile> ().SetTarget ((Vector2)_towerPair.transform.position);
				preparingToShoot = false;
			}
		}
	}

	//when laser hits this tower and was not shot by this tower, prepare to shoot a laser
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Laser" && expectingLaser) {
			expectingLaser = false;
			_towerPair.SendMessage ("ExpectLaser");
			other.gameObject.SendMessage ("HitTargetTower");
			preparingToShoot = true;
		}
	}

	//for the paired tower to tell this one to expect a laser soon
	public void ExpectLaser(){
		expectingLaser = true;
	}

	//called when tower is built by buildmenu. set everything important
	public void WhenBuilt(bool startsFiring, GameObject pairTower, GameObject pairGrid){
		_towerPair = pairTower;
		_gridPair = pairGrid;
		preparingToShoot = startsFiring;
		expectingLaser = !startsFiring;
	}
}
