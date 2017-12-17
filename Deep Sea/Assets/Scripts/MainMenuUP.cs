using UnityEngine;
using System.Collections;

public class MainMenuUP : MonoBehaviour
{

    // How long the main menu 'animations' last. Set in code as editing one file is actually simpler than using editor for multiple objects.
    private float _animationTime = 5f;

    [SerializeField]
    private float _startYPosition;

    [SerializeField]
    private float _endYPosition;

    private Vector3 _position;

    private float _speed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
