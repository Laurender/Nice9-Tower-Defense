using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaTower : MonoBehaviour {

	//Time in seconds for shooting to start
	[SerializeField]
	private float shootingSpeed = 2.0f;

	//Time in seconds for how long shooting lasts
	[SerializeField]
	private float shootingTime = 2.0f;

	[SerializeField]
	private float damageTime = 1.0f;

	//timer, when == shootingSpeed, shoots a projectile
	private float shootCounter = 0.0f;

	//used to decide if damage should be given
	private float damageCounter = 0.0f;

	bool _shooting = false;

	int _damage = 1;

	Collider2D _myColl;

	SpriteRenderer _childSprite;





	// Use this for initialization
	void Start () {
		_myColl = GetComponent<Collider2D> ();
		_myColl.enabled = false;
		_childSprite = transform.GetChild (0).GetComponent<SpriteRenderer> ();
		_childSprite.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!_shooting) {
			if (shootCounter < shootingSpeed) {
				shootCounter += Time.deltaTime;	
			} else {
                MusicController.StartElectric();
				_shooting = true;
				shootCounter = 0.0f;
				_myColl.enabled = true;
				_childSprite.enabled = true;
			}
		} else {
			if (shootCounter < shootingTime) {
				shootCounter += Time.deltaTime;	
			} else {
                MusicController.StopElectric();
				_shooting = false;
				shootCounter = 0.0f;
				_myColl.enabled = false;
				_childSprite.enabled = false;
				damageCounter = 0.0f;
			}

			damageCounter += Time.deltaTime;
			if (damageCounter >= damageTime + Time.deltaTime) {
				damageCounter = 0.0f;
			}
		}
	}

	/*void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Enemy" && _shooting) {
			if (damageCounter >= damageTime) {
				other.gameObject.GetComponent<Enemy> ().takeDamage (_damage);
				Debug.Log ("Does damage");
			}
		}
	}*/

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Enemy" && _shooting) {
			other.gameObject.GetComponent<Enemy> ().takeDamage (_damage);
			Debug.Log ("Does damage");
		}
	}

	public void UpgradeTower(){
		_damage = 2;
	}

}
