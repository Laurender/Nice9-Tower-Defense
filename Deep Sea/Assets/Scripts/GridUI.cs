using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUI : MonoBehaviour
{

    [SerializeField, Tooltip("Maximum number of towers.")]
    private int _popCap = 6;

    [SerializeField, Tooltip("Energy bar sprites.")]
    private Sprite[] _energyBarSprites;

    [SerializeField, Tooltip("Energy bar prefab")]
    private GameObject _energyBarPrefab;

    private GameObject _energyBar;
    private SpriteRenderer _sprRenderer;
    private static GridUI _staticReference;

    private int _popCurrent = 0;

    // Use this for initialization
    void Start()
    {
        _energyBar = Instantiate(_energyBarPrefab);
        _sprRenderer = _energyBar.GetComponent<SpriteRenderer>();
        _sprRenderer.sprite = _energyBarSprites[_popCap - _popCurrent];

        // Allows using static references without a real singleton.
        // Acceptable here since objects of this type are only created with Unity editor.
        _staticReference = this;
    }

    // Update is called once per frame
    void Update()
    {

        // Check for mouse and touch events.
        CheckForInputEvents();

    }

    private void CheckForInputEvents()
    {
        //check for touch event
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ProcessTheEvent(Input.GetTouch(0).position);
        }

        //check for a click, can be removed on Android builds.
        if (Input.GetMouseButtonDown(0))
        {
            ProcessTheEvent(Input.mousePosition);
        }
    }

    private void ProcessTheEvent(Vector3 wp)
    {

        //iterate thru the array of colliders that overlap the point
        foreach (Collider2D collider in Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(wp)))
        {

            Grid grid = collider.gameObject.GetComponent<Grid>();

            if (grid != null)
            {
                grid.OpenMenu(_popCurrent<_popCap);
            }
        }

    }

    // Redirect static methods to last created actual object.
    // Acceptable here since objects of this type are only created with Unity editor.
    // If this assumption is ever broken this code WILL NOT WORK.
    public static void CountTowerBuild() { _staticReference._CountTowerBuild(); }
    public static void CountTowerDestroy() { _staticReference._CountTowerDestroy(); }

    private void _CountTowerBuild()
    {
        _popCurrent++;
        _sprRenderer.sprite = _energyBarSprites[_popCap - _popCurrent];
    }

    private void _CountTowerDestroy()
    {
        _popCurrent--;
        _sprRenderer.sprite = _energyBarSprites[_popCap - _popCurrent];
    }
}
