using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Selections : MonoBehaviour
{

    public TMP_Text textbox;

    private GameObject unit;
    private GameObject selectedUnit;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		    Vector2 rayOrigin = new Vector2(mousePosition.x, mousePosition.y);

            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero, Mathf.Infinity, 3);

            if (hit.collider.gameObject != null) {
                unit = hit.collider.gameObject;

                if (unit != null && (unit.gameObject.tag == "Friendly" || unit.gameObject.tag == "Enemy")) {
                    selectedUnit = unit;
                } else {
                    selectedUnit = null;
                }
            } else {
                unit = null;
            }
        }

        UpdateStatsText();
    }

    void UpdateStatsText() {

        if (selectedUnit != null) {
            Defense unitDefense = selectedUnit.GetComponent<Defense>();
            Movement unitSpeed = selectedUnit.GetComponent<Movement>();

            textbox.text = $"Health: {unitDefense.currentValues[0]}\n" +
                           $"Armor: {unitDefense.currentValues[1]}\n" +
                           $"Shield: {unitDefense.currentValues[2]}\n" +
                           $"Speed: {unitSpeed.speed}";
        } else {
            textbox.text = "";
        }

    }

}
