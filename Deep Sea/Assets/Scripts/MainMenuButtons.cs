using UnityEngine;
using System.Collections;

public class MainMenuButtons : MonoBehaviour
{

    [SerializeField]
    private GameObject _buttons;

    [SerializeField]
    private GameObject _wiki;

    private IEnumerator ShowMenu()
    {

        yield return new WaitForSeconds(2.2f);
        _buttons.SetActive(true);
    }



    // Use this for initialization
    void Start()
    {
        MusicController.ChangeMusic(1);
        StartCoroutine(ShowMenu());
        _buttons.SetActive(false);
    }


    public void StartGame()
    {
        
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("LevelSelect");
    }

    public void OpenWiki()
    {
        _buttons.SetActive(false);
        _wiki.SetActive(true);
    }

    public void CloseWiki()
    {
        _wiki.SetActive(false);
        _buttons.SetActive(true);
    }

    public void OpenOptions()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Options");
    }
}
