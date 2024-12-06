using UnityEngine;
using System.Collections.Generic;

public abstract class AttackBase : MonoBehaviour {
	public List<GameObject> FindTargets(float range){
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, range);

		List<GameObject> targets = new List<GameObject>();

		foreach(Collider2D collider in hitColliders){
			targets.Add(collider.gameObject);
		}

		return targets;
	}

	public GameObject NearestTarget(List<GameObject> targets){
		float nearestDistance = Mathf.Infinity;
		GameObject nearest = null;

		foreach(GameObject target in targets){
			float distance = Vector2.Distance(transform.position, target.transform.position);

			if(distance < nearestDistance){
				nearestDistance = distance;
				nearest = target;
			}
		}

		return nearest;
	}
}