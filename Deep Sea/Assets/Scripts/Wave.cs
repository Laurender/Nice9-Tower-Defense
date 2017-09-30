using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

	[SerializeField, Tooltip("Time in seconds from scene start to wave start.")]
	private float _waveStart;

	[SerializeField, Tooltip("Time in seconds between enemies in the wave.")]
	private float _spawnInterval;

	[SerializeField, Tooltip("A previously defined route. You need to create this as a GameObject in the scene. Use the prefab.")]
	private Route _route;

	[SerializeField, Tooltip("The prefabs to use as enemies, in order.")]
	private GameObject[] _enemies;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
