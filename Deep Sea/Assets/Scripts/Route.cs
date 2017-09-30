using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class is a component to add to a level that defines a movement route
 * for the attackers to follow. It is basically a UI convenience that allows us
 * to make routes in the editor and to define several routes per level if necessary.
 */
public class Route : MonoBehaviour {

	[SerializeField, Tooltip("Waypoints of the route. The first is the spawn point and the last should be the base.")]
	private GameObject[] _waypoints;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
