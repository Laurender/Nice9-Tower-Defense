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

	//the laser shot when tower is upgraded
	[SerializeField]
	private GameObject uProjectile;

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

	float shootingTime = 0.75f;

	private Animator _animator;

	[SerializeField]
	private Sprite touchUpgradeIcon;

	void Start(){
		_animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame. Contains the shooting of a laser
	void Update () {
		if (preparingToShoot) {
			shootCounter += Time.deltaTime;
			if (shootCounter >= shootingSpeed) {
				/*GameObject temp;
				temp = Instantiate (projectile, transform.position, Quaternion.identity);
				temp.GetComponent<LaserProjectile> ().SetTarget ((Vector2)_towerPair.transform.position);*/
				Shoot ();
				StartCoroutine (Shooting ());
				preparingToShoot = false;
				_towerPair.SendMessage ("ExpectLaser");
			}
		}
	}

	void Shoot(){
		if (pairDirection == "N") {
			
			Vector3 laserPosition = new Vector3 (transform.position.x, transform.position.y+0.21875f, transform.position.z);
			GameObject temp;
			temp = Instantiate (projectile, laserPosition, Quaternion.identity);
			temp.GetComponent<LaserPiece> ().AsMade (pairDirection, "S", 0.0f);
			temp.GetComponent<SpriteRenderer> ().sortingOrder = 2;

			for (int i = 0; i < pairDistance -1; i++) {
				laserPosition = new Vector3 (transform.position.x, transform.position.y + 1.0f + ((float)i) +0.21875f, transform.position.z);
				temp = Instantiate (projectile, laserPosition, Quaternion.identity);
				temp.GetComponent<LaserPiece> ().AsMade (pairDirection, "M", (1 / 7) * (i % 8));
			}

			laserPosition = new Vector3 (transform.position.x, _towerPair.transform.position.y+0.21875f, transform.position.z);
			temp = Instantiate (projectile, laserPosition, Quaternion.identity);
			temp.GetComponent<LaserPiece> ().AsMade (pairDirection, "E", 0.0f);

		} else if (pairDirection == "E") {
			
			Vector3 laserPosition = new Vector3 (transform.position.x, transform.position.y+0.15625f, transform.position.z);
			GameObject temp;
			temp = Instantiate (projectile, laserPosition, Quaternion.identity);
			temp.GetComponent<LaserPiece> ().AsMade (pairDirection, "S", 0.0f);
			temp.GetComponent<SpriteRenderer> ().sortingOrder = 2;

			for (int i = 0; i < pairDistance - 1; i++) {
				laserPosition = new Vector3 (transform.position.x + 1.0f + ((float)i), transform.position.y+0.15625f, transform.position.z);
				temp = Instantiate (projectile, laserPosition, Quaternion.identity);
				temp.GetComponent<LaserPiece> ().AsMade (pairDirection, "M", (1 / 7) * (i % 8));
			}

			laserPosition = new Vector3 (_towerPair.transform.position.x, transform.position.y+0.15625f, transform.position.z);
			temp = Instantiate (projectile, laserPosition, Quaternion.identity);
			temp.GetComponent<LaserPiece> ().AsMade (pairDirection, "E", 0.0f);
			temp.GetComponent<SpriteRenderer> ().sortingOrder = 2;

		} else if (pairDirection == "S") {

			Vector3 laserPosition = new Vector3 (transform.position.x, transform.position.y+0.21875f, transform.position.z);
			GameObject temp;
			temp = Instantiate (projectile, laserPosition, Quaternion.identity);
			temp.GetComponent<LaserPiece> ().AsMade (pairDirection, "S", 0.0f);

			for (int i = 0; i < pairDistance - 1; i++) {
				laserPosition = new Vector3 (transform.position.x, transform.position.y - 1.0f - ((float)i) +0.21875f, transform.position.z);
				temp = Instantiate (projectile, laserPosition, Quaternion.identity);
				temp.GetComponent<LaserPiece> ().AsMade (pairDirection, "M", (1 / 7) * (i % 8));
			}

			laserPosition = new Vector3 (transform.position.x, _towerPair.transform.position.y+0.21875f, transform.position.z);
			temp = Instantiate (projectile, laserPosition, Quaternion.identity);
			temp.GetComponent<LaserPiece> ().AsMade (pairDirection, "E", 0.0f);
			temp.GetComponent<SpriteRenderer> ().sortingOrder = 2;

		} else if (pairDirection == "W") {

			Vector3 laserPosition = new Vector3 (transform.position.x, transform.position.y+0.15625f, transform.position.z);
			GameObject temp;
			temp = Instantiate (projectile, laserPosition, Quaternion.identity);
			temp.GetComponent<LaserPiece> ().AsMade (pairDirection, "S", 0.0f);
			temp.GetComponent<SpriteRenderer> ().sortingOrder = 2;

			for (int i = 0; i < pairDistance - 1; i++) {
				laserPosition = new Vector3 (transform.position.x - 1.0f - ((float)i), transform.position.y+0.15625f, transform.position.z);
				temp = Instantiate (projectile, laserPosition, Quaternion.identity);
				temp.GetComponent<LaserPiece> ().AsMade (pairDirection, "M", (1 / 7) * (i % 8));
			}

			laserPosition = new Vector3 (_towerPair.transform.position.x, transform.position.y+0.15625f, transform.position.z);
			temp = Instantiate (projectile, laserPosition, Quaternion.identity);
			temp.GetComponent<LaserPiece> ().AsMade (pairDirection, "E", 0.0f);
			temp.GetComponent<SpriteRenderer> ().sortingOrder = 2;

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
		shootCounter = -1f * shootingTime;
		StartCoroutine (Shooting ());
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

	IEnumerator Shooting(){
		_animator.SetBool ("Shooting", true);

		yield return new WaitForSeconds(shootingTime);

		_animator.SetBool ("Shooting", false);
	}

	public void UpgradeTower(){
		projectile = uProjectile;
		shootingTime = 1.5f;
		shootCounter = 0f;
		_animator.SetBool ("Shooting", false);
		_animator.SetBool ("Upgraded", true);
		transform.Find ("TouchIcon").GetComponent<SpriteRenderer> ().sprite = touchUpgradeIcon;
	}
}
