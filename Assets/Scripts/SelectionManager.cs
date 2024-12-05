using UnityEngine;
using System.Collections.Generic;

public class SelectionManager : MonoBehaviour {
	[SerializeField] private Camera mainCamera;
	[SerializeField] private LayerMask selectableLayer;
	[SerializeField] private LayerMask groundLayer;

	[SerializeField] private int selectMouse = 0;
	[SerializeField] private int orderMouse = 1;

	[SerializeField] private KeyCode cherryPickKey = KeyCode.LeftControl;

	private List<Selectable> selectedObjects = new List<Selectable>();

	private void Update(){
		HandleSelection();
		HandleMovement();
	}

	private void HandleSelection(){
		if(!Input.GetMouseButtonDown(selectMouse)){ return; }

		Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if(!Physics.Raycast(ray, out hit, Mathf.Infinity, selectableLayer)){
			if(!Input.GetKey(cherryPickKey)){
				DeselectAll();
			}
			return;
		}

		Selectable selectable = hit.collider.GetComponent<Selectable>();

		if(selectable != null){
			if(Input.GetKey(cherryPickKey)){
				CherryPick(selectable);
			}
			else{
				SingleSelect(selectable);
			}
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

	private void HandleMovement(){
		if(!Input.GetMouseButtonDown(orderMouse)){ return; }

		Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
		Vector2 rayOrigin = new Vector2(mousePosition.x, mousePosition.y);

		RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.zero, Mathf.Infinity, groundLayer);

		if(!hit.collider){ return; }

		foreach(Selectable selectable in selectedObjects){
			if(selectable.TryGetComponent<Movable>(out Movable movable)){
				movable.MoveTo(hit.point);
			}
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
}