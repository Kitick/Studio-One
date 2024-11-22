using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Indicator and Ground marker not working yet.

public class UnitSelectionManager : MonoBehaviour
{
    //Only one instance of this ever please
    //manages the control of unit, which one are selected

    public static UnitSelectionManager Instance { get; set; }

    private void Awake(){
        if (Instance != null && Instance != this){
            Destroy(gameObject);
        }
        else{
            Instance = this;
        }
    }

    //list of all units in game

    private List<GameObject> allUnitsList = new List<GameObject>();
    private List<GameObject> unitsSelected = new List<GameObject>();
	private Dictionary<GameObject, Unit> unitObjectMap = new Dictionary<GameObject, Unit>();

    //get layers
    public LayerMask clickable;
    public LayerMask floor;
    public GameObject floorMarker;

    private void Start(){

    }

    private void Update(){

        if (Input.GetMouseButtonDown(0)){
            //convert mouse position to world point
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0;

            //casting ray to detect units
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, Mathf.Infinity, clickable);

            if (hit.collider != null){
                if (Input.GetKey(KeyCode.LeftShift)){
                    MultiSelect(hit.collider.gameObject);
                }
                else{
                    ClickSelect(hit.collider.gameObject);
                }
            }
        	else{
                if (!Input.GetKey(KeyCode.LeftShift)){
                    DeselectAll();
                }

            }
        }

        //Floor Marker
        //also displays ground marker on sending units
        if (Input.GetMouseButtonDown(1) && unitsSelected.Count > 0){
    	    //convert mouse position to world point
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0;

            //casting ray to detect units
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, Mathf.Infinity, floor);

            if (hit.collider != null){
                Debug.Log("Raycast hit on ground " + hit.collider.name);
                floorMarker.transform.position = hit.point;

                //animation here later for selected bit of ground
                floorMarker.SetActive(false);
                floorMarker.SetActive(true);
            }
        }
    }

	public void AddToUnitList(Unit unit, GameObject unitObject){
		unitObjectMap.Add(unitObject, unit);
		allUnitsList.Add(unitObject);
	}

	public void RemoveFromUnitList(GameObject unitObject){
		unitObjectMap.Remove(unitObject);
		allUnitsList.Remove(unitObject);
	}

    private void MultiSelect(GameObject unitObject){
        bool hasUnit = unitsSelected.Contains(unitObject);

		EnableUnitMovement(unitObject, !hasUnit);
        ActivateSelectionIndicator(unitObject, !hasUnit);

		if (hasUnit){
            unitsSelected.Remove(unitObject);
        }
        else{
			unitsSelected.Add(unitObject);
        }
    }

    private void DeselectAll(){
		unitsSelected.ForEach(unitObject => {
			EnableUnitMovement(unitObject, false);
			ActivateSelectionIndicator(unitObject, false);
		});

        floorMarker.SetActive(false);
        unitsSelected.Clear();
    }

    private void ClickSelect(GameObject unitObject){
        //deselect when adding new units. Except maybe with shift
        DeselectAll();

        unitsSelected.Add(unitObject);

        //this puts some indicator around an active unit
        ActivateSelectionIndicator(unitObject, true);

        //enables or disables movement.
        EnableUnitMovement(unitObject, true);
    }

    private void EnableUnitMovement(GameObject unitObject, bool trigger){
        //enables movement script for the unit upon selection
        unitObjectMap.TryGetValue(unitObject, out Unit unit);
		unit.enabled = trigger;
    }

    //toggles on and off the indicator that appears around a unit to show it's been selected
    private void ActivateSelectionIndicator(GameObject unit, bool vis){
        //unit.transform.GetChild(0).gameObject.SetActive(vis);
    }
}