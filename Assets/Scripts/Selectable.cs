using UnityEngine;

public class Selectable : MonoBehaviour {
	public bool isSelected = false;

	[SerializeField] private GameObject selectionIndicator;

	private void Start(){
		Deselect();
	}

	public void Select(){
		isSelected = true;

		if(selectionIndicator == null){ return; }
		selectionIndicator.SetActive(true);
	}

	public void Deselect(){
		isSelected = false;

		if(selectionIndicator == null){ return; }
		selectionIndicator.SetActive(false);
	}
}