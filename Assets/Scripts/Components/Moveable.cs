using UnityEngine;
using UnityEngine.AI;

public class Movable : MonoBehaviour {
	[SerializeField] private float speed = 5f;

	private NavMeshAgent agent;

	private void Awake(){
		agent = GetComponent<NavMeshAgent>();
	}

	private void Update(){
		transform.rotation = Quaternion.Euler(0, 0, 0);
		agent.speed = speed;
	}

	public void MoveTo(Vector3 position){
		agent.SetDestination(position);
	}
}