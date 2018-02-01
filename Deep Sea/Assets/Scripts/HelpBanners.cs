using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpBanners : MonoBehaviour {

    [SerializeField]
    private GameObject _buildBanner;

    [SerializeField]
    private GameObject _wavesBanner;


    private static HelpBanners _instance;

    private void Awake()
    {
        _instance = this;
    }

    public static void ShowBuildBanner()
    {
        _instance.StartCoroutine(_instance.Banner(_instance._buildBanner));
    }

    public static void ShowWavesBanner()
    {
        _instance.StartCoroutine(_instance.Banner(_instance._wavesBanner));
    }

    public IEnumerator Banner(GameObject banner)
    {
        banner.SetActive(true);

        for (float y = -100; y < 0; y += 5)
        {
            yield return new WaitForSecondsRealtime(.05f);
        }

        for (float y = 0; y > -100; y -= 5)
        {
            yield return new WaitForSecondsRealtime(.05f);
        }
        banner.SetActive(false);
    }
}
