using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPattern : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1f);
	}
}
