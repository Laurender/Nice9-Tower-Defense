using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    [SerializeField]
    private AudioSource[] _music;

    [SerializeField]
    private AudioSource[] _sounds;

    [SerializeField]
    private AudioClip[] _effects;

    [SerializeField]
    private AudioSource _electricNoise;
    private int _electricNoiseCounter;

    [SerializeField]
    private AudioSource _solarNoise;
    private int _solarNoiseCounter;

    private int _currentMusic;

    private bool _playMusic, _playSFX;

    static MusicController _instance;
    private static bool _paused;

    // Sets _instance to the the MusicController of the current scene, hopefully.
    // This allows static calls to the methods. Probably should change other classes to work this way?
    void Awake()
    {

        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        PlayMusic = (PlayerPrefs.GetInt("PlayMusic", 1) == 1);
        PlaySFX = (PlayerPrefs.GetInt("PlaySFX", 1) == 1);
        

    }

    public static void ChangeMusic(int _nextMusic)
    {

        if (_instance == null) return;


        _instance.IntChangeMusic(_nextMusic);

    }

    private void IntChangeMusic(int _nextMusic)
    {
        if (_nextMusic == _currentMusic) return;

        if (_playMusic)
        {
            _music[_currentMusic].Stop();
            _music[_nextMusic].Play();
        }

        _currentMusic = _nextMusic;


    }

    public static bool PlayMusic
    {

        

        get
        {
            if (_instance == null) return false;
            return _instance._playMusic;
        }

        set
        {
            if (_instance == null) return;
            _instance.SetPlayMusic(value);
        }
    }

    public static bool PlaySFX
    {
        get
        {
            if (_instance == null) return false;
            return _instance._playSFX;
        }

        set
        {
            if (_instance == null) return;
            _instance.SetPlaySFX(value);

        }
    }

    private void SetPlayMusic(bool playMusic)
    {
  
        if (_playMusic == playMusic) return;

        _playMusic = playMusic;
        PlayerPrefs.SetInt("PlayMusic", _playMusic ? 1 : 0);

        if (playMusic)
        {
            _music[_currentMusic].Play();
        }
        else
        {
            _music[_currentMusic].Stop();
        }
    }

    private void SetPlaySFX(bool playSFX)
    {
        if (_playSFX == playSFX) return;

        _playSFX = playSFX;
        PlayerPrefs.SetInt("PlaySFX", _playSFX ? 1 : 0);

    }

    public static void PlaySound(int sound)
    {
        if (_instance == null) return;
        if (!_instance._playSFX) return;

        _instance._sounds[sound].Play();
    }

    public static void PlayEffect(int effect)
    {
        if (_instance == null) return;
        if (!_instance._playSFX) return;

        AudioSource.PlayClipAtPoint(_instance._effects[effect], Vector3.zero);

    }

    public static void StartElectric()
    {
        if (_instance == null) return;
        if (_instance._electricNoiseCounter == 0 && !_paused)
        {
            _instance._electricNoise.Play();
        }

        _instance._electricNoiseCounter++;
    }

    public static void StopElectric()
    {
        if (_instance == null || _instance._electricNoiseCounter == 0) return;

        _instance._electricNoiseCounter--;

        if (_instance._electricNoiseCounter == 0)
        {
            _instance._electricNoise.Stop();
        }

    }

    public static void StartSolar()
    {
        if (_instance == null) return;
        if (_instance._solarNoiseCounter == 0 && !_paused)
        {
            _instance._solarNoise.Play();
        }

        _instance._solarNoiseCounter++;
    }

    public static void StopSolar()
    {
        if (_instance == null || _instance._solarNoiseCounter == 0) return;

        _instance._solarNoiseCounter--;
      

        if (_instance._solarNoiseCounter == 0)
        {
            _instance._solarNoise.Stop();
        }

    }

    public static void EndSfx()
    {
        _paused = false;

        if (_instance == null) return;
        _instance._solarNoise.Stop();
        _instance._solarNoiseCounter = 0;

        _instance._electricNoise.Stop();
        _instance._electricNoiseCounter = 0;



    }

    public static void PauseSfx()
    {
        _paused = true;
        if (_instance == null) return;
        _instance._solarNoise.Stop();
        _instance._electricNoise.Stop();
    }

    public static void ResumeSfx()
    {
        _paused = false;
        if (_instance == null) return;
        if (_instance._solarNoiseCounter > 0) _instance._solarNoise.Play();
        if (_instance._electricNoiseCounter > 0) _instance._electricNoise.Play();

    }

}
