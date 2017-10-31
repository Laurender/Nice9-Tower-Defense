using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUI : MonoBehaviour {


  
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

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
        foreach(Collider2D collider in Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(wp)))
        {
                    
            Grid grid = collider.gameObject.GetComponent<Grid>();

            if (grid!=null)
            {
                grid.OpenMenu();
            }
        }
        
    }
}
