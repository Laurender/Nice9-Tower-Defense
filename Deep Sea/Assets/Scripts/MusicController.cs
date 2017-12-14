﻿using System.Collections;
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

    // Sets _instance to the the MusicController of the current scene, hopefully.
    // This allows static calls to the methods. Probably should change other classes to work this way?
    void Start()
    {

        _instance = this;
        PlayMusic = true;
        PlaySFX = true;

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
            return _instance._playMusic;
        }

        set
        {
            _instance.SetPlay(value);
        }
    }

    public static bool PlaySFX
    {
        get
        {
            return _instance._playSFX;
        }

        set
        {
            _instance._playSFX = value;
        }
    }

    private void SetPlay(bool playMusic)
    {
        if (_playMusic == playMusic) return;

        _playMusic = playMusic;

        if (playMusic)
        {
            _music[_currentMusic].Play();
        }
        else
        {
            _music[_currentMusic].Stop();
        }
    }

    public static void PlaySound(int sound)
    {

        if (!_instance._playSFX) return;

        _instance._sounds[sound].Play();
    }

    public static void PlayEffect(int effect)
    {
        if (!_instance._playSFX) return;

        AudioSource.PlayClipAtPoint(_instance._effects[effect], Vector3.zero);

    }

    public static void StartElectric()
    {
        if(_instance._electricNoiseCounter==0)
        {
            _instance._electricNoise.Play();
        }

        _instance._electricNoiseCounter++;
    }

    public static void StopElectric()
    {
        _instance._electricNoiseCounter--;

        if (_instance._electricNoiseCounter == 0)
        {
            _instance._electricNoise.Stop();
        }

    }

    public static void StartSolar()
    {
        if (_instance._solarNoiseCounter == 0)
        {
            _instance._solarNoise.Play();
        }

        _instance._solarNoiseCounter++;
    }

    public static void StopSolar()
    {
        _instance._solarNoiseCounter--;

        if (_instance._solarNoiseCounter == 0)
        {
            _instance._solarNoise.Stop();
        }

    }

}
