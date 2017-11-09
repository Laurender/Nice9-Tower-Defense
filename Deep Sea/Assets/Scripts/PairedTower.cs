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

	//to which direction the paired tower is. N, E, S or W.
	//used to make laser
	string pairDirection;

	//distance to paired tower. used to make laser.
	float pairDistance;
	
	// Update is called once per frame. Contains the shooting of a laser
	void Update () {
		if (preparingToShoot) {
			shootCounter += Time.deltaTime;
			if (shootCounter >= shootingSpeed) {
				shootCounter = 0.0f;
				/*GameObject temp;
				temp = Instantiate (projectile, transform.position, Quaternion.identity);
				temp.GetComponent<LaserProjectile> ().SetTarget ((Vector2)_towerPair.transform.position);*/
				Shoot ();
				preparingToShoot = false;
				_towerPair.SendMessage ("ExpectLaser");
			}
		}
	}

	void Shoot(){
		if (pairDirection == "N") {
			for (int i = 0; i < pairDistance; i++) {
				Vector3 laserPosition = new Vector3 (transform.position.x, transform.position.y + 0.5f + ((float)i), transform.position.z);
				GameObject temp;
				temp = Instantiate (projectile, laserPosition, Quaternion.identity);
				temp.GetComponent<LaserPiece> ().Direction (false);
			}
		} else if (pairDirection == "E") {
			for (int i = 0; i < pairDistance; i++) {
				Vector3 laserPosition = new Vector3 (transform.position.x + 0.5f + ((float)i), transform.position.y, transform.position.z);
				GameObject temp;
				temp = Instantiate (projectile, laserPosition, Quaternion.identity);
				temp.GetComponent<LaserPiece> ().Direction (true);
			}
		} else if (pairDirection == "S") {
			for (int i = 0; i < pairDistance; i++) {
				Vector3 laserPosition = new Vector3 (transform.position.x, transform.position.y - 0.5f - ((float)i), transform.position.z);
				GameObject temp;
				temp = Instantiate (projectile, laserPosition, Quaternion.identity);
				temp.GetComponent<LaserPiece> ().Direction (false);
			}
		} else if (pairDirection == "W") {
			for (int i = 0; i < pairDistance; i++) {
				Vector3 laserPosition = new Vector3 (transform.position.x - 0.5f - ((float)i), transform.position.y, transform.position.z);
				GameObject temp;
				temp = Instantiate (projectile, laserPosition, Quaternion.identity);
				temp.GetComponent<LaserPiece> ().Direction (true);
			}
		}
	}

	//when laser hits this tower and was not shot by this tower, prepare to shoot a laser
	/*void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Laser" && expectingLaser) {
			expectingLaser = false;
			_towerPair.SendMessage ("ExpectLaser");
			other.gameObject.SendMessage ("HitTargetTower");
			preparingToShoot = true;
		}
	}*/

	//for the paired tower to tell this one to expect a laser soon
	public void ExpectLaser(){
		expectingLaser = true;
		preparingToShoot = true;
	}

	//called when tower is built by buildmenu. set everything important
	public void WhenBuilt(bool startsFiring, GameObject pairTower, GameObject pairGrid){
		_towerPair = pairTower;
		_gridPair = pairGrid;
		preparingToShoot = startsFiring;
		expectingLaser = !startsFiring;

		if (transform.position.x - 0.1f < _towerPair.transform.position.x && transform.position.x + 0.1f > _towerPair.transform.position.x) {
			if (transform.position.y > _towerPair.transform.position.y) {
				pairDirection = "S";
				pairDistance = transform.position.y - _towerPair.transform.position.y;
			} else {
				pairDirection = "N";
				pairDistance = _towerPair.transform.position.y - transform.position.y;
			}
		} else {
			if (transform.position.x > _towerPair.transform.position.x) {
				pairDirection = "W";
				pairDistance = transform.position.x - _towerPair.transform.position.x;
			} else {
				pairDirection = "E";
				pairDistance = _towerPair.transform.position.x - transform.position.x;
			}
		}
	}

    public Grid PairedTile
    {
        get
        {
            return _gridPair.GetComponent<Grid>();
        }
    }
}
