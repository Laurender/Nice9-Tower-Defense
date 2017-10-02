using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempMoveLeft : MonoBehaviour {

	//just moves left, so I can test targeting
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector2.left * Time.deltaTime);
	}
}
