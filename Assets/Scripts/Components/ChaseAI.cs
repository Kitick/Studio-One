using UnityEngine;
using System.Collections.Generic;

public class ChaseAI : AttackBase {
	[SerializeField] private float range = 5f;
	[SerializeField] private string targetTag = "Friendly";

	private void Update(){
		List<GameObject> targets = FindTargets(range);

		targets.RemoveAll(target => !target.CompareTag(targetTag) || target.GetComponent<Defense>() == null);

		GameObject nearest = NearestTarget(targets);

		if(nearest != null){
			Movable movable = GetComponent<Movable>();
			movable.MoveTo(nearest.transform.position);
		}
	}
}