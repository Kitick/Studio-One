using UnityEngine;
using System.Collections.Generic;

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

		if(selected == null){ MoveOrder(hit.point); }
	}

	private void MoveOrder(Vector3 destination){
		foreach(Selectable selectable in selectedObjects){
			Movement movable = selectable.GetComponent<Movement>();

			if(movable == null){ continue; }

			movable.MoveTo(destination);
		}
	}

	private void Select(Selectable selectable){
		if(selectedObjects.Contains(selectable)){ return; }

		selectedObjects.Add(selectable);
		selectable.isSelected = true;
	}

	private void Deselect(Selectable selectable){
		if(!selectedObjects.Contains(selectable)){ return; }

		selectedObjects.Remove(selectable);
		selectable.isSelected = false;
	}

	private void DeselectAll(){
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