using UnityEngine;
using System.Collections.Generic;

public class BasicAttack : AttackBase {
	[SerializeField] private int damage = 5;
	[SerializeField] private float attackSpeed = 1f;
	[SerializeField] private float attackRange = 1.5f;

	[SerializeField] private string targetTag = "Enemy";

	private float lastAttackTime;

	private void Update(){
		if(Time.time < lastAttackTime + attackSpeed){ return; }

		List<GameObject> targets = FindTargets(targetTag, attackRange);

		targets.RemoveAll(target => target.GetComponent<Health>() == null);

		GameObject nearest = NearestTarget(targets);

		if(nearest != null){
			Attack(nearest);
		}
	}

	public void Attack(GameObject target){
		Health health = target.GetComponent<Health>();

		health.Damage(damage);

		lastAttackTime = Time.time;
	}
}