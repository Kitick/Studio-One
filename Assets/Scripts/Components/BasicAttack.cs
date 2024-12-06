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

		List<GameObject> targets = FindTargets(attackRange);

		targets.RemoveAll(target => !target.CompareTag(targetTag) || target.GetComponent<Defense>() == null);

		GameObject nearest = NearestTarget(targets);

		if(nearest != null){
			Attack(nearest);
		}
	}

	public void Attack(GameObject target){
		Defense defense = target.GetComponent<Defense>();

		defense.Damage(Defense.DefenseType.Armor, damage);

		lastAttackTime = Time.time;
	}
}