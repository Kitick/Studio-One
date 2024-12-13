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
	void Update(){
		if (!Input.GetMouseButtonDown(0)) { return; }

		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 rayOrigin = new Vector2(mousePosition.x, mousePosition.y);

		RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero, Mathf.Infinity, 3);

		if(hit.collider == null){ return; }

		unit = hit.collider.gameObject;
		if (unit == null) { return; }

		if (unit.gameObject.CompareTag("Friendly") || unit.gameObject.CompareTag("Enemy")) {
			selectedUnit = unit;
		} else {
			selectedUnit = null;
		}

		UpdateStatsText();
	}

	void UpdateStatsText() {

		if (selectedUnit != null) {
			Defense unitDefense = selectedUnit.GetComponent<Defense>();
			Movement unitSpeed = selectedUnit.GetComponent<Movement>();
			BasicAttack unitAttack = selectedUnit.GetComponent<BasicAttack>();
			RangedAttack unitRangedAttack = selectedUnit.GetComponent<RangedAttack>();

			textbox.text = "";
			textbox.text += $"Armor: {unitDefense.GetDefense(Defense.DefenseType.Armor)}\n";
			textbox.text += $"Shield: {unitDefense.GetDefense(Defense.DefenseType.Sheild)}\n";
			textbox.text += $"Health: {unitDefense.GetDefense(Defense.DefenseType.Health)}\n";
			textbox.text += "\n";
			textbox.text += $"Speed: {unitSpeed.speed}";

			if (unitAttack != null) {
				textbox.text += $"\nAttack Speed: {unitAttack.AttackSpeed}";
				textbox.text += $"\nDamage: {unitAttack.Damage}";
				textbox.text += $"\nRange: {unitAttack.Range}";
			}
			else if (unitRangedAttack != null) {
				textbox.text += $"\nAttack Speed: {unitRangedAttack.AttackSpeed}";
				textbox.text += $"\nDamage: {unitRangedAttack.Damage}";
				textbox.text += $"\nMin Range: {unitRangedAttack.MinRange}";
				textbox.text += $"\nMax Range: {unitRangedAttack.MaxRange}";
			}
		} else {
			textbox.text = "";
		}

	}

}
