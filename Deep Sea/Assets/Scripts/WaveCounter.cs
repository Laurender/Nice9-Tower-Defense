using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCounter : MonoBehaviour
{

    [SerializeField, Tooltip("Wave counter text")]
    private GameObject _textObject;

    [SerializeField, Tooltip("Wave counter container")]
    private GameObject _container;

    [SerializeField, Tooltip("Routes available for enemies, select route in enemy prefab, if not using the first one.")]
    private GameObject[] _routes;

    [SerializeField, Tooltip("Sets the level as endless. Waves are spawned in random order without end")]
    private bool _endless;

	[SerializeField]
	private GameObject _enemy1;

	[SerializeField]
	private GameObject _enemy2;

	[SerializeField]
	private GameObject _enemy3;

	[SerializeField]
	private GameObject _enemy4;

	[SerializeField]
	private GameObject _boss;

	private List <GameObject> _endlessWave;

    private int _currentCount, _totalCount, _endedCount;
    private UnityEngine.UI.Text _text;
    private int _enemyCount;
    private Wave[] waves;

    private Vector3 _destination;
    private Vector3 _slideOut;
    private Vector3 _position;
    private bool _isSlidingOut;


    public void EnemyCount(int enemies)
    {
        _enemyCount = enemies;
    }

    // Use this for initialization
    void Start()
    {

        waves = gameObject.GetComponents<Wave>();
        _totalCount = waves.Length;

        _text = _textObject.GetComponent<UnityEngine.UI.Text>();

        if (_endless)
        {
			_text.text = "ENDLESS : " + (_currentCount+1).ToString();
			_endlessWave = new List<GameObject> ();
        }
        else
        {
            _text.text = "WAVE : " + _currentCount.ToString() + "/" + _totalCount.ToString();

        }


        _container.SetActive(true);
        _position = _textObject.transform.localPosition;
        _destination = _position;
        _slideOut = _position + Vector3.up * 80;



    }

    public void WaveCount()
    {
        if (_endless)
        {
            _currentCount = UnityEngine.Random.Range(0, _totalCount);
        }
        else
        {
            _currentCount++;
            _isSlidingOut = true;
            _destination = _slideOut;
        }
        // _text.text = "WAVE : " + _currentCount.ToString() + "/" + _totalCount.ToString();

    }

    public void EnemyDied()
    {
        _enemyCount--;
        //Debug.Log(_enemyCount);
        if (_enemyCount == 0)
        {
			if (_currentCount < _totalCount && !_endless) 
			{
				if (waves [_currentCount] != null)
 				{                
					// Turns out this gets called after the game is closed´, which causes 'errors' in Unity.
					waves [_currentCount].Trigger (_routes);
				}
			} else if (_endless) 
			{
				_currentCount++;
				_text.text = "ENDLESS : " + (_currentCount + 1).ToString();
				SendEndless ();
			}
            else
            {
                FindObjectOfType<GridUI>().LevelPass();
            }
        }
    }

    // This method allows delaying the first wave until the game is first unpaused.
    public void StartWaves()
    {
		if (_endless) 
		{
			SendEndless ();
		} 
		else 
		{
			waves [0].Trigger (_routes);
		}
    }

    void Update()
    {
        // If the object is not at the target destination
        if (_destination != _textObject.transform.localPosition)
        {
            // Move towards the destination each frame until the object reaches it
            IncrementPosition();
        }
        else
        {
            if (_isSlidingOut)
            {
                _isSlidingOut = false;
                _destination = _position;
                _text.text = "WAVE : " + _currentCount.ToString() + "/" + _totalCount.ToString();
            }
        }
    }

    void IncrementPosition()
    {
        // Calculate the next position
        float delta = 60f * Time.deltaTime;
        Vector3 currentPosition = _textObject.transform.localPosition;
        Vector3 nextPosition = Vector3.MoveTowards(currentPosition, _destination, delta);

        // Move the object to the next position
        _textObject.transform.localPosition = nextPosition;
    }

	void SendEndless()
	{
		_endlessWave.Clear();
		//Debug.Log (_currentCount % 10);
		switch (_currentCount % 15) 
		{
		case 0:
			for (int i = 0; i < 4 + (((int)_currentCount / 15) * 10); i++) 
			{
				_endlessWave.Add (_enemy1);
			}
			for (int i = 0; i < 3 + (((int)_currentCount / 15) * 2); i++) 
			{
				_endlessWave.Add (_enemy2);
			}
			_endlessWave.Add (_enemy3);
			for (int i = 0; i < (((int)_currentCount / 15) * 4); i++) 
			{
				_endlessWave.Add (_enemy4);
			}
			StartCoroutine(SpawnEndless ());
			break;
		case 1:
			for (int i = 0; i < 4 + (((int)_currentCount / 15)*6); i++) 
			{
				_endlessWave.Add(_enemy1);
			}
			for (int i = 0; i < 4 + (((int)_currentCount / 15)*4); i++) 
			{
				_endlessWave.Add(_enemy2);
			}
			for (int i = 0; i < 1 + (int)_currentCount / 15; i++) 
			{
				_endlessWave.Add(_enemy3);
			}
			for (int i = 0; i < 1 + (((int)_currentCount / 15) * 7); i++) 
			{
				_endlessWave.Add (_enemy4);
			}
			StartCoroutine(SpawnEndless ());
			break;
		case 2:
			for (int i = 0; i < 5 + (((int)_currentCount / 15)*9); i++) 
			{
				_endlessWave.Add(_enemy1);
			}
			for (int i = 0; i < 4 + (int)_currentCount / 15; i++) 
			{
				_endlessWave.Add(_enemy2);
			}
			for (int i = 0; i < 1 + (((int)_currentCount / 15) * 2); i++) 
			{
				_endlessWave.Add(_enemy3);
			}
			for (int i = 0; i < 2 + (((int)_currentCount / 15) * 6); i++) 
			{
				_endlessWave.Add (_enemy4);
			}
			StartCoroutine(SpawnEndless ());
			break;
		case 3:
			for (int i = 0; i < 5 + (((int)_currentCount / 15)*5); i++) 
			{
				_endlessWave.Add(_enemy1);
			}
			for (int i = 0; i < 4 + (((int)_currentCount / 15) * 2); i++) 
			{
				_endlessWave.Add(_enemy2);
			}
			for (int i = 0; i < 2 + (((int)_currentCount / 15) * 2); i++) 
			{
				_endlessWave.Add(_enemy3);
			}
			for (int i = 0; i < 3 + (((int)_currentCount / 15) * 9); i++) 
			{
				_endlessWave.Add (_enemy4);
			}
			StartCoroutine(SpawnEndless ());
			break;
		case 4:
			for (int i = 0; i < 5 + (((int)_currentCount / 15)*7); i++) 
			{
				_endlessWave.Add(_enemy1);
			}
			for (int i = 0; i < 4 + (((int)_currentCount / 15) * 2); i++) 
			{
				_endlessWave.Add(_enemy2);
			}
			for (int i = 0; i < 3 + (((int)_currentCount / 15) * 3); i++) 
			{
				_endlessWave.Add(_enemy3);
			}
			for (int i = 0; i < 3 + (((int)_currentCount / 15) * 5); i++) 
			{
				_endlessWave.Add (_enemy4);
			}
			StartCoroutine(SpawnEndless ());
			break;
		case 5:
			for (int i = 0; i < 8 + (((int)_currentCount / 15)*4); i++) 
			{
				_endlessWave.Add(_enemy1);
			}
			for (int i = 0; i < 4 + (int)_currentCount / 15; i++) 
			{
				_endlessWave.Add(_enemy2);
			}
			_endlessWave.Add(_enemy3);
			for (int i = 0; i < 3 + (((int)_currentCount / 15) * 4); i++) 
			{
				_endlessWave.Add (_enemy4);
			}
			StartCoroutine(SpawnEndless ());
			break;
		case 6:
			for (int i = 0; i < 8 + (((int)_currentCount / 15)*6); i++) 
			{
				_endlessWave.Add(_enemy1);
			}
			for (int i = 0; i < 5; i++) 
			{
				_endlessWave.Add(_enemy2);
			}
			for (int i = 0; i < 2; i++) 
			{
				_endlessWave.Add(_enemy3);
			}
			for (int i = 0; i < 3 + (((int)_currentCount / 15) * 3); i++) 
			{
				_endlessWave.Add (_enemy4);
			}
			StartCoroutine(SpawnEndless ());
			break;
		case 7:
			for (int i = 0; i < 7 + (((int)_currentCount / 15)*5); i++) 
			{
				_endlessWave.Add(_enemy1);
			}
			for (int i = 0; i < 5 + (int)_currentCount / 15; i++) 
			{
				_endlessWave.Add(_enemy2);
			}
			for (int i = 0; i < 2 + (int)_currentCount / 15; i++) 
			{
				_endlessWave.Add(_enemy3);
			}
			for (int i = 0; i < 6 + (int)_currentCount / 15; i++) 
			{
				_endlessWave.Add (_enemy4);
			}
			StartCoroutine(SpawnEndless ());
			break;
		case 8:
			for (int i = 0; i < 7 + (((int)_currentCount / 15)*7); i++) 
			{
				_endlessWave.Add(_enemy1);
			}
			for (int i = 0; i < 6; i++) 
			{
				_endlessWave.Add(_enemy2);
			}
			for (int i = 0; i < 3 + (int)_currentCount / 15; i++) 
			{
				_endlessWave.Add(_enemy3);
			}
			for (int i = 0; i < 6; i++) 
			{
				_endlessWave.Add (_enemy4);
			}
			StartCoroutine(SpawnEndless ());
			break;
		case 9:
			for (int i = 0; i < 10 + (((int)_currentCount / 15)*6); i++) 
			{
				_endlessWave.Add(_enemy1);
			}
			for (int i = 0; i < 4; i++) 
			{
				_endlessWave.Add(_enemy2);
			}
			for (int i = 0; i < 5; i++) 
			{
				_endlessWave.Add(_enemy3);
			}
			for (int i = 0; i < 5 + (int)_currentCount / 15; i++) 
			{
				_endlessWave.Add (_enemy4);
			}
			StartCoroutine(SpawnEndless ());
			break;
		case 10:
			for (int i = 0; i < 12 + (((int)_currentCount / 15)*2); i++) 
			{
				_endlessWave.Add(_enemy1);
			}
			for (int i = 0; i < 5; i++) 
			{
				_endlessWave.Add(_enemy2);
			}
			for (int i = 0; i < 1 + (int)_currentCount / 15; i++) 
			{
				_endlessWave.Add(_enemy3);
			}
			for (int i = 0; i < 7 + (((int)_currentCount / 15)*2); i++) 
			{
				_endlessWave.Add (_enemy4);
			}
			StartCoroutine(SpawnEndless ());
			break;
		case 11:
			for (int i = 0; i < 14 + (((int)_currentCount / 15)*2); i++) 
			{
				_endlessWave.Add(_enemy1);
			}
			for (int i = 0; i < 4 + (((int)_currentCount / 15) * 2); i++) 
			{
				_endlessWave.Add(_enemy2);
			}
			for (int i = 0; i < 2 + (int)_currentCount / 15; i++) 
			{
				_endlessWave.Add(_enemy3);
			}
			for (int i = 0; i < 6 + (int)_currentCount / 15; i++) 
			{
				_endlessWave.Add (_enemy4);
			}
			StartCoroutine(SpawnEndless ());
			break;
		case 12:
			for (int i = 0; i < 12 + (((int)_currentCount / 15)*4); i++) 
			{
				_endlessWave.Add(_enemy1);
			}
			for (int i = 0; i < 6 + (((int)_currentCount / 15) * 2); i++) 
			{
				_endlessWave.Add(_enemy2);
			}
			for (int i = 0; i < 3 + (int)_currentCount / 15; i++) 
			{
				_endlessWave.Add(_enemy3);
			}
			for (int i = 0; i < 7 + (((int)_currentCount / 15) * 2); i++) 
			{
				_endlessWave.Add (_enemy4);
			}
			StartCoroutine(SpawnEndless ());
			break;
		case 13:
			for (int i = 0; i < 14 + (((int)_currentCount / 15)*2); i++) 
			{
				_endlessWave.Add(_enemy1);
			}
			for (int i = 0; i < 6 + (((int)_currentCount / 15) * 2); i++) 
			{
				_endlessWave.Add(_enemy2);
			}
			for (int i = 0; i < 4 + (int)_currentCount / 15; i++) 
			{
				_endlessWave.Add(_enemy3);
			}
			for (int i = 0; i < 6 + (((int)_currentCount / 15) * 2); i++) 
			{
				_endlessWave.Add (_enemy4);
			}
			StartCoroutine(SpawnEndless ());
			break;
		case 14:
			for (int i = 0; i < 1 + (int)_currentCount / 15; i++) 
			{
				_endlessWave.Add(_boss);
			}
			for (int i = 0; i < 10 + (((int)_currentCount / 10)*5); i++) 
			{
				_endlessWave.Add(_enemy1);
			}
			StartCoroutine(SpawnEndless ());
			break;
		default:
			Debug.Log("Endless not working");
			break;
		}
	}
	IEnumerator SpawnEndless()
	{
		_enemyCount = _endlessWave.Count;
		BarPanel.Money += 20;

		//used to refer to the instantiated object within the loop.
		GameObject tempReference;

		for (int i = 0; i < _endlessWave.Count; i++) 
		{
			tempReference = Instantiate (_endlessWave[i]);

			(tempReference.GetComponent<Enemy> ()).SetRoute (_routes);

			// Wait until time to spawn next enemy. There will be a redundant wait after last enemy.
			yield return new WaitForSeconds (0.5f);
		}
	}
}
