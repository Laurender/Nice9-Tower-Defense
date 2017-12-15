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
            _instance._healthText.text = value.ToString();
            _instance._healthAnimate = _instance._animationTime;
        }
    }

    #endregion

    #region fields for the animations

    [SerializeField]
    private float _animationTime;

    private float _moneyAnimate, _healthAnimate;

    #endregion


    // Use this for initialization
    void Start()
    {
        // Set instance to the one in the current level.
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(_moneyAnimate>0)
        {
            _moneyAnimate -= Time.deltaTime;

            // Actual animation
            
            if(_moneyAnimate<0)
            {
                _moneyAnimate = 0;
            }
        }
    

            if(_healthAnimate>0)
        {
            _healthAnimate -= Time.deltaTime;

            // Actual animation
            
            if(_healthAnimate<0)
            {
                _healthAnimate = 0;
            }
        }
    }


}
