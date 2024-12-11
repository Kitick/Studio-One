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
	}

	private void Update(){
		
		//stop unit if stuck
		if (IsStuck())
		{
			timeStuck += Time.deltaTime;
			if (timeStuck > stuckTimeThresh)
			{
				Stop();
			}
		} else
		{
			timeStuck = 0f; //resetting stuck timer if unit unsticks
		}

		agent.speed = speed;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

	public void MoveTo(Vector3 position){
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