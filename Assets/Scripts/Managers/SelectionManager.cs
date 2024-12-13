using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Mathematics;

public class SelectionManager : MonoBehaviour {
	[SerializeField] private Camera mainCamera;
	[SerializeField] private LayerMask layer;

	[SerializeField] private int selectMouse = 0;
	[SerializeField] private int orderMouse = 1;

	[SerializeField] private KeyCode cherryPickKey = KeyCode.LeftShift;

	public List<Selectable> selectedObjects = new List<Selectable>();

	private void Update(){
		VerifyList();
		HandleSelection();
		HandleOrder();
	}

	private void VerifyList(){
		selectedObjects.RemoveAll(selectable => selectable == null);
	}

	private void HandleSelection(){
		if(!Input.GetMouseButtonDown(selectMouse)){ return; }

		RaycastHit2D hit = Raycast();

		Selectable selected = null;

		if(hit.collider != null){
			selected = hit.collider.GetComponent<Selectable>();
		}

		if(selected == null){
			if(!Input.GetKey(cherryPickKey)){
				DeselectAll();
			}

			return;
		}

		if(Input.GetKey(cherryPickKey)){
			CherryPick(selected);
		}
		else{
			SingleSelect(selected);
		}
	}

	private void SingleSelect(Selectable selectable){
		DeselectAll();
		Select(selectable);
	}

	private void CherryPick(Selectable selectable){
		if(selectedObjects.Contains(selectable)){
			Deselect(selectable);
		}
		else{
			Select(selectable);
		}
	}

	private RaycastHit2D Raycast(){
		Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
		Vector2 rayOrigin = new Vector2(mousePosition.x, mousePosition.y);

		RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero, Mathf.Infinity, layer);

		return hit;
	}

	private void HandleOrder(){
		if(!Input.GetMouseButtonDown(orderMouse)){ return; }

		RaycastHit2D hit = Raycast();

		if(hit.collider == null){ return; }

		Selectable selected = hit.collider.GetComponent<Selectable>();

		if(selected == null){ Formation(hit.point); }
	}

	private void SortList(){
		List<Selectable> innerList = new List<Selectable>();
		List<Selectable> outerList = new List<Selectable>();

		foreach(Selectable selectable in selectedObjects){
			if(selectable.GetComponent<RangedAttack>() != null){
				innerList.Add(selectable);
			}
			else{
				outerList.Add(selectable);
			}
		}

		selectedObjects.Clear();

		selectedObjects.AddRange(innerList);
		selectedObjects.AddRange(outerList);
	}

	private void Formation(Vector2 destination){
		SortList();

		int remaining = selectedObjects.Count;

		float pi2 = Mathf.PI * 2;

		for(int r = 0; true; r++){
			int ringSize = Mathf.Min(4 * r, remaining);

			for(float t = 0; t < pi2; t += pi2 / ringSize){
				if(remaining == 0){ return; }

				Vector2 offset = new Vector2(Mathf.Cos(t), Mathf.Sin(t)) * r * 1.25f;

				MoveOrder(selectedObjects[selectedObjects.Count - remaining], destination + offset);
				remaining--;

				if(r == 0){ break; }
			}
		}
	}

	private void MoveOrder(Selectable unit, Vector2 destination){
		Movement movable = unit.GetComponent<Movement>();
		if(movable == null){ return; }
		movable.MoveTo(destination);
	}

	public void Select(Selectable selectable){
		if(selectedObjects.Contains(selectable)){ return; }

		selectedObjects.Add(selectable);
		selectable.isSelected = true;
	}

	public void Deselect(Selectable selectable){
		if(!selectedObjects.Contains(selectable)){ return; }

		selectedObjects.Remove(selectable);
		selectable.isSelected = false;
	}

	public void DeselectAll(){
		List<Selectable> selectedObjectsCopy = new List<Selectable>(selectedObjects);

		foreach(Selectable selectable in selectedObjectsCopy){
			Deselect(selectable);
		}
	}

	public void SelectAll (List<Selectable> unitsToAdd)
	{
		foreach(var unit in unitsToAdd)
		{
			if (!selectedObjects.Contains(unit))
			{
				Select(unit);
			}
		}
	}
}