using UnityEngine;
using UnityEngine.AI;

public class Movable : MonoBehaviour {
	private NavMeshAgent agent;

	private void Awake(){
		agent = GetComponent<NavMeshAgent>();
	}

	private void Update(){
		transform.rotation = Quaternion.Euler(0, 0, 0);
	}

	public void MoveTo(Vector3 position){
		agent.SetDestination(position);
	}
}