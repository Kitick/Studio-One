using UnityEngine;
using UnityEngine.Timeline;

public class Selectable : MonoBehaviour {
	[HideInInspector] public bool isSelected = false;

	[SerializeField] private GameObject selectionIndicator;

	private void Start(){
		isSelected = false;
	}

	private void Update(){
		if(selectionIndicator == null){ return; }
		selectionIndicator.SetActive(isSelected);
	}
}