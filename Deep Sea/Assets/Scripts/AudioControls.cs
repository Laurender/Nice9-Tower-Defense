using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControls : MonoBehaviour
{

    [SerializeField]
    UnityEngine.UI.Image _soundImage;

    [SerializeField]
    Sprite _soundEnabled;

    [SerializeField]
    Sprite _soundDisabled;

    [SerializeField]
    UnityEngine.UI.Image _musicImage;

    [SerializeField]
    Sprite _musicEnabled;

    [SerializeField]
    Sprite _musicDisabled;


    // Use this for initialization
    void Start()
    {

        _soundImage.sprite = MusicController.PlaySFX ? _soundEnabled : _soundDisabled;
        _musicImage.sprite = MusicController.PlayMusic ? _musicEnabled : _musicDisabled;

    }

    public void MusicButton()
    {
        MusicController.PlayMusic = MusicController.PlayMusic ? false : true;
        _musicImage.sprite = MusicController.PlayMusic ? _musicEnabled : _musicDisabled;
    }

    public void SoundButton()
    {
        MusicController.PlaySFX = MusicController.PlaySFX ? false : true;
        _soundImage.sprite = MusicController.PlaySFX ? _soundEnabled : _soundDisabled;
    }


}
