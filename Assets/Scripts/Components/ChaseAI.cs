using UnityEngine;
using System.Collections.Generic;

public class ChaseAI : AttackBase {
	[SerializeField] private float range = 5;
	[SerializeField] private TargetTag targetTag = TargetTag.Friendly;

	private void Update(){
		List<GameObject> targets = FindTargets(range);

		targets.RemoveAll(target => !HasTag(target, targetTag) || target.GetComponent<Defense>() == null);

		GameObject nearest = NearestTarget(targets);

		if(nearest != null){
			Movement movable = GetComponent<Movement>();
			movable.MoveTo(nearest.transform.position);
		}
	}
}