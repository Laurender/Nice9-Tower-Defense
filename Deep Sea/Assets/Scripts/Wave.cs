using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{

	[SerializeField, Tooltip ("Time in seconds from scene start to wave start.")]
	private float _waveStart;

	[SerializeField, Tooltip ("Time in seconds between enemies in the wave.")]
	private float _spawnInterval;

	[SerializeField, Tooltip ("A previously defined route. You need to create this as a GameObject in the scene. Use the prefab.")]
	private GameObject _route;

	[SerializeField, Tooltip ("The prefabs to use as enemies, in order.")]
	private GameObject[] _enemies;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine (Spawner ());
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	// The coroutine that spawns the enemies on time.
	private IEnumerator Spawner ()
	{
		//used to refer to the instantiated object within the loop.
		GameObject tempReference;

		// Wait the time until start spawning.
		yield return new WaitForSeconds (_waveStart);

		foreach (GameObject go in _enemies) {

			tempReference = Instantiate(go);

			(tempReference.GetComponent<Enemy>()).SetRoute(_route.GetComponent<Route>());

			// Wait until time to spawn next enemy. There will be a redundant wait after last enemy.
			yield return new WaitForSeconds (_spawnInterval);
		}
	}

}
