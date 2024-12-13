using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UnitSelectionBox : MonoBehaviour
{
    Camera myCam;

    //forces unity to serialize variable in script. So it's in inspector
    [SerializeField]
    RectTransform boxVisual;

    Rect SelectionBox;

    Vector2 startPosition;
    Vector2 endPosition;
    public string unitTag = "Friendly";

    //referencing unit selection manager
    public SelectionManager manager;

    private void Start()
    {
        myCam = Camera.main;
        startPosition = Vector2.zero;
        endPosition = Vector2.zero;
        DrawVisual();
    }

    private void Update()
    {
        //on click
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;

            //for selecting units
            SelectionBox = new Rect();
        }

        //when dragging
        if (Input.GetMouseButton(0))
        {
            //to draw the square
            endPosition = Input.mousePosition;
            DrawVisual();
            DrawSelection();
        }

        //when mouse is released
        if (Input.GetMouseButtonUp(0))
        {
            SelectUnits();

            startPosition = Vector2.zero;
            endPosition = Vector2.zero;
            DrawVisual();
        }
    }

    void DrawVisual()
    {
        //calculating start and end positions of the selection box
        Vector2 boxStart = startPosition;
        Vector2 boxEnd = endPosition;

        //calculating the center of the selection box
        Vector2 boxCenter = (boxStart + boxEnd) / 2;

        //set the position of box based on center
        boxVisual.position = boxCenter;

        Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));

        //set the size of the box based on calculation
        boxVisual.sizeDelta = boxSize;
    }

    void DrawSelection()
    {
        if (Input.mousePosition.x < startPosition.x)
        {
            SelectionBox.xMin = Input.mousePosition.x;
            SelectionBox.xMax = startPosition.x;
        }
        else
        {
            SelectionBox.xMin = startPosition.x;
            SelectionBox.xMax = Input.mousePosition.x;
        }

        if (Input.mousePosition.y < startPosition.y)
        {
            SelectionBox.yMin = Input.mousePosition.y;
            SelectionBox.yMax = startPosition.y;
        }
        else
        {
            SelectionBox.yMin = startPosition.y;
            SelectionBox.yMax = Input.mousePosition.y;
        }

    }

    void SelectUnits()
    {

		if (Time.timeScale == 0){ return; }

        GameObject[] units = GameObject.FindGameObjectsWithTag(unitTag);
        List<Selectable> selectedUnits = new List<Selectable>();

        foreach (GameObject unit in units)
        {
            //get the Selectable component from the units in the box
            Selectable selected = unit.GetComponent<Selectable>();
            if (selected != null)
            {
                //check if in box
                Vector3 unitScreenPosition = myCam.WorldToScreenPoint(unit.transform.position);
                Vector2 unitScreenPosition2D = new Vector2(unitScreenPosition.x, unitScreenPosition.y);

                    if (SelectionBox.Contains(unitScreenPosition2D))
                    {
                        selectedUnits.Add(selected);
                    }


            }

            if (manager != null)
            {
                manager.SelectAll(selectedUnits);
            }
            else if (manager == null)
            {
                Debug.Log("no manager");
            }

        }
    }
}

