using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{

	[SerializeField, Tooltip ("Waypoints of the route. The first is the spawn point and the last should be the base.")]
	private GameObject[] _waypoints;

	// Use this for initialization
	void Start ()
	{
		if (_waypoints.Length < 2) {
			Debug.LogError ("All Routes need at least two points, the spawn and the end.");
		}
	}
	
	


	public Vector3 GetPosition (int index)
	{
		return _waypoints [index].transform.position;
	
	}

	public bool IsEnd (int index)
	{
		return (index >= _waypoints.Length);
	}
}
