using UnityEngine;
using System.Collections;

public class MainMenuButtons : MonoBehaviour
{

    [SerializeField]
    private GameObject _buttons;

    private IEnumerator ShowMenu() {

        yield return new WaitForSeconds(5.2f);
        _buttons.SetActive(true);
    }

     

    // Use this for initialization
    void Start()
    {        
        StartCoroutine(ShowMenu());
        _buttons.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
