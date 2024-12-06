using UnityEngine;
using System.Collections.Generic;

public class BasicAttack : AttackBase {
	[SerializeField] private int damage = 5;
	[SerializeField] private float cooldown = 1f;
	[SerializeField] private float range = 1f;

	[SerializeField] private TargetTag targetTag = TargetTag.Enemy;

	private float lastAttackTime;

	private void Update(){
		if(Time.time < lastAttackTime + cooldown){ return; }

		List<GameObject> targets = FindTargets(range);

		targets.RemoveAll(target => !HasTag(target, targetTag) || target.GetComponent<Defense>() == null);

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