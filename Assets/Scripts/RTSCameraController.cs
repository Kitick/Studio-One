using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class RTSCameraController : MonoBehaviour {
	public static RTSCameraController instance;

	[Header("General")]
	[SerializeField] Transform cameraTransform;
	public Transform followTransform;
	Vector3 newPosition;
	Vector3 dragStartPosition;
	Vector3 dragCurrentPosition;

	[Header("Optional Functionality")]
	[SerializeField] private bool moveWithKeyboad = true;
	[SerializeField] private bool moveWithEdgeScrolling = true;
	[SerializeField] private bool moveWithMouseDrag = true;


	[Header("Zoom Settings")]
	[SerializeField] private int zoomSpeed = 5;
	[SerializeField] private int minZoom = 2;
	[SerializeField] private int maxZoom = 8;


	[Header("Keyboard Movement")]
	[SerializeField] private float movementSpeed = 0.015f;
	[SerializeField] private float fastSpeed = 0.04f;
	[SerializeField] private int movementSensitivity = 10; // Hardcoded Sensitivity

	[SerializeField] private int minMapX = -25;
	[SerializeField] private int maxMapX = 25;
	[SerializeField] private int minMapY = -10;
	[SerializeField] private int maxMapY = 10;


	[Header("Edge Scrolling Movement")]
	[SerializeField] int edgeSize = 50;
	bool isCursorSet = false;
	public Texture2D cursorArrowUp;
	public Texture2D cursorArrowDown;
	public Texture2D cursorArrowLeft;
	public Texture2D cursorArrowRight;

	CursorArrow currentCursor = CursorArrow.DEFAULT;
	enum CursorArrow { UP, DOWN, LEFT, RIGHT, DEFAULT }

	private void Start(){
		instance = this;
		newPosition = transform.position;
		//Cursor.lockState = CursorLockMode.Confined; // If we have an extra monitor we don't want to exit screen bounds
	}

	private void Update(){
		// Allow Camera to follow Target
		if (followTransform != null){
			transform.position = new Vector3(followTransform.position.x, followTransform.position.y, transform.position.z);
		}
		// Let us control Camera
		else{
			HandleCameraMovement();
		}

		if (Input.GetKeyDown(KeyCode.Escape)){
			followTransform = null;
		}
	}

	private Vector3 ClampPosition(Vector3 position){
		float x = Mathf.Clamp(position.x, minMapX, maxMapX);
		float y = Mathf.Clamp(position.y, minMapY, maxMapY);

		return new Vector3(x, y, position.z);
	}

	void HandleCameraMovement(){
		// Mouse Drag
		if (moveWithMouseDrag){
			HandleMouseDragInput();
		}

		// Keyboard Control
		if (moveWithKeyboad){
			HandleKeyboardInput();
		}

		// Edge Scrolling
		if (moveWithEdgeScrolling){
			HandleEdgeScroll();
		}

		HandleZoom();

		newPosition = ClampPosition(newPosition);

		transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementSensitivity);
	}

	private void HandleZoom(){
		float scrollInput = Input.GetAxis("Mouse ScrollWheel"); // Get scroll input

		if (scrollInput == 0){ return; }

		float newSize = Camera.main.orthographicSize - scrollInput * zoomSpeed;
		Camera.main.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
	}


	private void HandleEdgeScroll(){
		float speed = movementSpeed;

		// Move Right
		if (Input.mousePosition.x > Screen.width - edgeSize){
			newPosition += Vector3.right * speed;
			ChangeCursor(CursorArrow.RIGHT);
			isCursorSet = true;
		}
		// Move Left
		else if (Input.mousePosition.x < edgeSize){
			newPosition += Vector3.left * speed;
			ChangeCursor(CursorArrow.LEFT);
			isCursorSet = true;
		}
		// Move Up
		else if (Input.mousePosition.y > Screen.height - edgeSize){
			newPosition += Vector3.up * speed;
			ChangeCursor(CursorArrow.UP);
			isCursorSet = true;
		}
		// Move Down
		else if (Input.mousePosition.y < edgeSize){
			newPosition += Vector3.down * speed;
			ChangeCursor(CursorArrow.DOWN);
			isCursorSet = true;
		}
		else {
			if (isCursorSet){
				ChangeCursor(CursorArrow.DEFAULT);
				isCursorSet = false;
			}
		}
	}

	private void HandleKeyboardInput(){
		float speed = movementSpeed;

		if (Input.GetKey(KeyCode.LeftShift)){
			speed = fastSpeed;
		}

		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
			newPosition += Vector3.up * speed;
		}
		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
			newPosition += Vector3.down * speed;
		}
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
			newPosition += Vector3.right * speed;
		}
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
			newPosition += Vector3.left * speed;
		}
	}

	private void HandleMouseDragInput(){
		if (Input.GetMouseButtonDown(2) && !EventSystem.current.IsPointerOverGameObject()){
			Plane plane = new Plane(Vector3.forward, Vector3.zero);  // 2D plane (Z=0)
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			float entry;

			if (plane.Raycast(ray, out entry)){
				dragStartPosition = ray.GetPoint(entry);
			}
		}

		if (Input.GetMouseButton(2) && !EventSystem.current.IsPointerOverGameObject()){
			Plane plane = new Plane(Vector3.forward, Vector3.zero);  // 2D plane (Z=0)
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			float entry;

			if (plane.Raycast(ray, out entry)){
				dragCurrentPosition = ray.GetPoint(entry);
				newPosition = transform.position + (dragStartPosition - dragCurrentPosition);
			}
		}
	}

	private void ChangeCursor(CursorArrow newCursor){
		// Only change cursor if its not the same cursor
		if (currentCursor == newCursor){return;}

		switch (newCursor){
			case CursorArrow.UP:
				Cursor.SetCursor(cursorArrowUp, Vector2.zero, CursorMode.Auto);
				break;
			case CursorArrow.DOWN:
				Cursor.SetCursor(cursorArrowDown, new Vector2(cursorArrowDown.width, cursorArrowDown.height), CursorMode.Auto); // So the Cursor will stay inside view
				break;
			case CursorArrow.LEFT:
				Cursor.SetCursor(cursorArrowLeft, Vector2.zero, CursorMode.Auto);
				break;
			case CursorArrow.RIGHT:
				Cursor.SetCursor(cursorArrowRight, new Vector2(cursorArrowRight.width, cursorArrowRight.height), CursorMode.Auto); // So the Cursor will stay inside view
				break;
			case CursorArrow.DEFAULT:
				Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
				break;
		}

		currentCursor = newCursor;
	}
}