﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour {

    float _destroyTime = 1.00f;

    [SerializeField]
    Vector3 _direction;

    float _speed = 1.00f;

	// Use this for initialization
	void Start () {

        transform.rotation = Random.rotation;
        transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z);
        Vector2 direction = Random.insideUnitCircle;
        direction = direction.normalized;
        _direction = new Vector3(direction.x, direction.y, 0f);
	}
	
	// Update is called once per frame
	void Update () {

        transform.Translate(_direction * _speed * Time.deltaTime);

        //transform.position = new Vector3(transform.position.x + _direction.x*Time.deltaTime, transform.position.y + _direction.y*Time.deltaTime);

        _destroyTime -= Time.deltaTime;

        if (_destroyTime <= 0.01) {
            Destroy(this.gameObject);
        }
	}
}
