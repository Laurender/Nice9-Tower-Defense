using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

	//These tell if a direction from this grid has a piece of road next to it
	//These will be set on the editor
	[SerializeField]
	bool _roadNorth;

	[SerializeField]
	bool _roadEast;

	[SerializeField]
	bool _roadSouth;

	[SerializeField]
	bool _roadWest;

	//tells if this piece of grid is on the left side of the screen
	//to be used by buildmenu to tell it which side of the screen to appear
	bool _onLeft = false;

	//tells if has a tower built on it
	bool hasTower = false;

    public static bool isActive = true;

	//will have the tower built on this piece of grid
	GameObject currentTower;

	Collider2D _myCollider;

	void Start(){
		if (transform.position.x < 0) {
			_onLeft = true;
		}
		_myCollider = GetComponent<Collider2D> ();
	}

	void Update () {
		
		//when user touches on this piece of grid
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			//touch is on this piece of grid
			Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			if (_myCollider == Physics2D.OverlapPoint(wp))
			{
				//Open build or destroy menu
				OpenMenu ();
			}
		}
	}

	//when user clicks while mouse is over this piece of grid
	void OnMouseDown(){

		OpenMenu ();
	}


	void OpenMenu(){

        // Grid objects are inactive when the menu is open.
        if(!isActive)
        {
            return;
        }

		if (!hasTower) {
            //open buildmenu
            //build menu needs to get a reference to this piece of grid, and call Grid.SetTower(towerChosen) when a tower is built
            //build menu should also look at _onLeft to decide on which side of the screen to appear on

            BuildMenu.Open(this, _onLeft);
            
        } else {
            //open destroymenu

            DeleteMenu.Open(this, _onLeft);
        }
	}

	public void SetTower(GameObject tower){
		if (tower.GetComponent<HatchTower> () != null) {
			tower.GetComponent<HatchTower> ().SetRoadBools (_roadNorth, _roadEast, _roadSouth, _roadWest);
		}
		currentTower = tower;
		hasTower = true;
	}

    public void RemoveTower()
    {
        hasTower = false;
        Destroy(currentTower);
        currentTower = null; // Just in case;
    }
}
