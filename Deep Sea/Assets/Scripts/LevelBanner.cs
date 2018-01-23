using UnityEngine;
using System.Collections;

public class LevelBanner : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _images;

 

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<UnityEngine.UI.Image>().sprite = _images[LevelManager.CurrentLevel];

    }

  
}
