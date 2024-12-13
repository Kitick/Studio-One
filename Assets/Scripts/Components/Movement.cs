using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour {
	[SerializeField] public float speed = 5f;

	//distance to see whether stuck
	[SerializeField] private float stuckThreshold = 1f;

	//time before considering unit stuck
	[SerializeField] private float stuckTimeThresh = 0.2f;

	private NavMeshAgent agent;
	private float timeStuck = 0f;

	private void Awake(){
		agent = GetComponent<NavMeshAgent>();
		agent.speed = speed;
	}

	private void Update(){
		if (agent == null){ return; }

		//stop unit if stuck
		if (IsStuck())
		{
			timeStuck += Time.deltaTime;
			if (timeStuck > stuckTimeThresh)
			{
				//Stop();
			}
		} else
		{
			timeStuck = 0f; //resetting stuck timer if unit unsticks
		}

		RotateWithVelocity();
    }

	private void RotateWithVelocity(){
		float currentAngle = transform.rotation.z;
		Vector3 velocity = agent.velocity;

		float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
		if (velocity.magnitude < 0.5f){ angle = currentAngle + 90; }

		transform.rotation = Quaternion.Euler(0, 0, angle - 90);
	}

	public void MoveTo(Vector2 position){
		agent.SetDestination(position);
	}

	public void Stop(){
		agent.ResetPath();

		//stopping velocity to try fixing glide issue
		agent.velocity = Vector3.zero;
	}

	//function determines whether unit is stuck
	private bool IsStuck()
	{
		//check if path is valid and if moving
		if (agent.pathPending || agent.isPathStale || agent.remainingDistance > stuckThreshold)
		{
			return false; //not stuck
		}

		//check if agent is close to destination but not moving
		if (agent.velocity.sqrMagnitude == 0f)
		{
			return true; //must be stuck. Probably.
		}

		return false; //shouldn't be stuck not doing anything
	}

}