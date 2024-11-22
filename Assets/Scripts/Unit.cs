
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour {
	private Vector3 target;
	NavMeshAgent agent;

	private void Start(){
		agent = GetComponent<NavMeshAgent>();
		agent.updateRotation = false;
		agent.updateUpAxis = false;

		UnitSelectionManager.Instance.AddToUnitList(this, gameObject);
	}

	public GameObject GetGameObject(){
		return gameObject;
	}

	private void OnDestroy(){
		UnitSelectionManager.Instance.RemoveFromUnitList(gameObject);
	}

	private void Update(){
		SetTargetPosition();
		SetAgentPosition();
	}

	public void SetTargetPosition(){
		if (Input.GetMouseButtonDown(1)){
			target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
	}

	public void SetAgentPosition(){
		agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));
	}
}