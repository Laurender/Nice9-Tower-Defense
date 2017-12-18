using UnityEngine;
using System.Collections;

public class MainMenuButtons : MonoBehaviour
{

    [SerializeField]
    private GameObject _buttons;

    [SerializeField]
    private GameObject _wiki;

    [SerializeField]
    private GameObject _story;

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
        MusicController.PlaySound(0);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("LevelSelect");
    }

    public void OpenWiki()
    {
        MusicController.PlaySound(0);
        _buttons.SetActive(false);
        _wiki.SetActive(true);
    }

    public void CloseWiki()
    {
        MusicController.PlaySound(0);
        _wiki.SetActive(false);
        _buttons.SetActive(true);
    }

    public void OpenOptions()
    {
        MusicController.PlaySound(0);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Options");
    }

    public void ShowStory()
    {
        _story.SetActive(true);
    }

    public void HideStory()
    {
        _story.SetActive(false);
    }
}
