using UnityEngine;
using System.Collections;

public class BarPanel : MonoBehaviour
{
    // Instance for this level;
    private static BarPanel _instance;

    #region Links to game objects in the prefab.

    [SerializeField]
    private GameObject _moneyDisplay;

    [SerializeField]
    private UnityEngine.UI.Text _moneyText;

    [SerializeField]
    private GameObject _healthDisplay;

    [SerializeField]
    private UnityEngine.UI.Text _healthText;

    #endregion

    #region Store health and money and provide static properties to access them.

    private int _money, _health;

    public static int Money
    {
        get
        {
            return _instance._money;
        }

        set
        {
            _instance._money = value;
            _instance._moneyText.text = value.ToString();
            _instance._moneyAnimate = _instance._animationTime;
        }
    }

    public static int Health
    {
        get
        {
            return _instance._health;
        }

        set
        {
            _instance._health = value;
            _instance._healthText.text = value.ToString()+"/10";
            _instance._healthAnimate = _instance._animationTime;
        }
    }

    #endregion

    #region fields for the animations

    [SerializeField]
    private float _animationTime;

    private float _moneyAnimate, _healthAnimate;

    private float _moneyBlink, _healthBlink;

    private bool _moneyLarge, _healthLarge;

    #endregion


    // Use this for initialization
    void Awake()
    {
        // Set instance to the one in the current level.
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // This removes an error when running in editor.
        // Can probably be removed in actual builds.
        if(_instance==null)
        {
            _instance = this;
        }

        if (_moneyAnimate > 0)
        {
            _moneyAnimate -= Time.deltaTime;

            if (_moneyBlink > 0)
            {
                _moneyBlink -= Time.deltaTime;
            }

            if (_moneyBlink <= 0)
            {
                _moneyBlink = .2f;
                _moneyDisplay.transform.localScale = _moneyLarge ? Vector3.one : Vector3.one * 1.2f;
                _moneyLarge = _moneyLarge ? false : true;
            }

            if (_moneyAnimate <= 0)
            {
                _moneyAnimate = 0;
                _moneyDisplay.transform.localScale = Vector3.one; ;
            }
        }


        if (_healthAnimate > 0)
        {
            _healthAnimate -= Time.deltaTime;

            if (_healthBlink > 0)
            {
                _healthBlink -= Time.deltaTime;
            }
            if (_healthBlink <= 0)
            {
                _healthBlink = .2f;
                _healthDisplay.transform.localScale = _healthLarge ? Vector3.one : Vector3.one * 1.2f;
                _healthLarge = _healthLarge ? false : true;
            }

            if (_healthAnimate <= 0)
            {
                _healthAnimate = 0;
                _healthDisplay.transform.localScale = Vector3.one;
            }
        }
    }


}
