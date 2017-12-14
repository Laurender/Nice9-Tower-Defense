using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    [SerializeField]
    private AudioSource[] _music;

    [SerializeField]
    private AudioSource[] _SFX;

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

        _instance._SFX[sound].Play();
    }



}
