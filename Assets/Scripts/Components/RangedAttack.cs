using UnityEngine;
using System.Collections.Generic;

public class RangedAttack : AttackBase {
	[SerializeField] private float multiplier = 1;
	[SerializeField] private float minRange = 2;
	[SerializeField] private float maxRange = 6;
	[SerializeField] private float cooldown = 1;

	[SerializeField] private GameObject projectilePrefab;

	[SerializeField] private TargetTag targetTag = TargetTag.Enemy;

	private float lastAttackTime;

	private void Update(){
		if(Time.time < lastAttackTime + cooldown){ return; }

		List<GameObject> targets = FindTargets(maxRange);
		List<GameObject> tooClose = FindTargets(minRange);

		targets.RemoveAll(target => !HasTag(target, targetTag) || target.GetComponent<Defense>() == null || tooClose.Contains(target));

		GameObject nearest = NearestTarget(targets);

		if(nearest != null){
			Fire(nearest.transform.position);
		}
	}

	public void Fire(Vector2 position){
		GameObject projectileObject = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

		Projectile projectile = projectileObject.GetComponent<Projectile>();
		projectile.Fire(position, multiplier);

		lastAttackTime = Time.time;
	}
}