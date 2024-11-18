using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Indicator and Ground marker not working yet
//Weird sprite rotation bug in beginning of playmode that requires turning off navmeshagent to select the unit, then turning it back on.

public class UnitSelectionManager : MonoBehaviour
{
    //Only one instance of this ever please
    //manages the control of unit, which one are selected

    public static UnitSelectionManager Instance { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    //list of all units in game

    public List<GameObject> allUnitsList = new List<GameObject>();
    public List<GameObject> unitsSelected = new List<GameObject>();

    //get layers
    public LayerMask clickable;
    public LayerMask floor;
    public GameObject floorMarker;

    private Camera cam;

    //get camera
    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            //convert mouse position to world point
            Vector3 worldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0;


            //casting ray to detect units
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, Mathf.Infinity, clickable);

            if (hit.collider != null)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    MultiSelect(hit.collider.gameObject);
                }
                else
                {
                    clickSelect(hit.collider.gameObject);
                }
            }
            else
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    DeselectAll();
                }

            }
        }

        //Floor Marker
        //also displays ground marker on sending units
        if (Input.GetMouseButtonDown(1) && unitsSelected.Count > 0)
        {
            //convert mouse position to world point
            Vector3 worldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0;


            //casting ray to detect units
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, Mathf.Infinity, floor);

            if (hit.collider != null)
            {
                Debug.Log("Raycast hit on ground " + hit.collider.name);
                floorMarker.transform.position = hit.point;

                //animation here later for selected bit of ground
                floorMarker.SetActive(false);
                floorMarker.SetActive(true);
            }


        }
    }

    private void MultiSelect(GameObject unit)
    {
        if (unitsSelected.Contains(unit) == false)
        {
            unitsSelected.Add(unit);
            activateSelectionIndicator(unit, true);
            EnableUnitMovement(unit, true);
        }
        else
        {
            EnableUnitMovement(unit, false);
            activateSelectionIndicator(unit, false);
            unitsSelected.Remove(unit);
        }
    }

    private void DeselectAll()
    {
        foreach (var unit in unitsSelected)
        {
            EnableUnitMovement(unit, false);
            activateSelectionIndicator(unit, false);
        }
        floorMarker.SetActive(false);
        unitsSelected.Clear();
    }

    private void clickSelect(GameObject unit)
    {
        //deselect when adding new units. Except maybe with shift
        DeselectAll();

        unitsSelected.Add(unit);

        //this puts some indicator around an active unit
        activateSelectionIndicator(unit, true);

        //enables or disables movement.
        EnableUnitMovement(unit, true);
    }

    private void EnableUnitMovement(GameObject unit, bool trigger)
    {
        //enables movement script for the unit upon selection
        unit.GetComponent<UnitMove>().enabled = trigger;
    }


    //toggles on and off the indicator that appears around a unit to show it's been selected
    private void activateSelectionIndicator(GameObject unit, bool vis)
    {
        unit.transform.GetChild(0).gameObject.SetActive(vis);
    }

}

