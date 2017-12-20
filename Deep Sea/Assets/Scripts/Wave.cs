using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{

	[SerializeField, Tooltip ("Time in seconds from scene start to wave start.")]
	private float _waveStart;

	[SerializeField, Tooltip ("Time in seconds between enemies in the wave.")]
	private float _spawnInterval;

	[SerializeField, Tooltip ("Amount of money player gets at the start of this wave.")]
	private int _moneyReward;

	[SerializeField, Tooltip ("The prefabs to use as enemies, in order.")]
	private GameObject[] _enemies;

    

	// Use this for initialization
	void Start ()
	{
		//StartCoroutine (Spawner ());
	}

    public void Trigger(GameObject[] routes){

        
        StartCoroutine(Spawner(routes));
        
    }

    

	// The coroutine that spawns the enemies on time.
	private IEnumerator Spawner (GameObject[] routes)
	{
		//used to refer to the instantiated object within the loop.
		GameObject tempReference;

        //wavecounter reference;
        WaveCounter waveCounter = gameObject.GetComponent<WaveCounter>();

        // Wait the time until start spawning.
        //yield return new WaitForSeconds (_waveStart);

        BarPanel.Money += _moneyReward;

        waveCounter.WaveCount();
        waveCounter.EnemyCount(_enemies.Length);

		foreach (GameObject go in _enemies) {

			tempReference = Instantiate (go);

			(tempReference.GetComponent<Enemy> ()).SetRoute (routes);

            // Wait until time to spawn next enemy. There will be a redundant wait after last enemy.
            yield return new WaitForSeconds (_spawnInterval);
		}

      
    }

}
