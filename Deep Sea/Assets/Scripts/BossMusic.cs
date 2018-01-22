using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusic : MonoBehaviour {

    private static int _bossCount;

	static int BossCount
    {
        set
        {
            _bossCount = value;

            if (_bossCount < 0) _bossCount = 0;

            if (_bossCount > 0) MusicController.OverlayMusic(4);

            if (_bossCount == 0) MusicController.EndOverlayMusic(4);

        }

        get
        {
            return _bossCount;
        }
    }
	void Start ()
    {
        BossCount++;
	}

    void OnDestroy()
    {
        BossCount--;
    }
}
