using UnityEngine;
using System.Collections.Generic;

public class BasicAttack : MonoBehaviour {
	[SerializeField] private int damage = 5;
	[SerializeField] private float attackSpeed = 1f;
	[SerializeField] private float attackRange = 1.5f;

	[SerializeField] private string targetTag = "Enemy";

	private float lastAttackTime;

	private void Update(){
		if(Time.time < lastAttackTime + attackSpeed){ return; }

		List<GameObject> targets = FindTargets();

		GameObject nearest = NearestTarget(targets);

		if(nearest != null){
			Attack(nearest);
		}
	}

	public List<GameObject> FindTargets(){
		Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRange);

		List<GameObject> targets = new List<GameObject>();

		foreach(Collider2D collider in hitColliders){
			if(collider.CompareTag(targetTag) && collider.TryGetComponent(out Health health)){
				targets.Add(collider.gameObject);
			}
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

	public void Attack(GameObject target){
		Health health = target.GetComponent<Health>();

		health.Damage(damage);

		lastAttackTime = Time.time;
	}
}