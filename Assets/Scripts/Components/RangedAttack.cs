using UnityEngine;
using System.Collections.Generic;

public class RangedAttack : AttackBase {
	[SerializeField] private float minRange = 2f;
	[SerializeField] private float maxRange = 6f;
	[SerializeField] private float attackSpeed = 1f;

	[SerializeField] private GameObject projectilePrefab;

	[SerializeField] private string targetTag = "Enemy";

	private float lastAttackTime;

	private void Update(){
		if(Time.time < lastAttackTime + attackSpeed){ return; }

		List<GameObject> targets = FindTargets(maxRange);
		List<GameObject> tooClose = FindTargets(minRange);

		targets.RemoveAll(target => !target.CompareTag(targetTag) || target.GetComponent<Defense>() == null || tooClose.Contains(target));

		GameObject nearest = NearestTarget(targets);

		if(nearest != null){
			Fire(nearest.transform.position);
		}
	}

	public void Fire(Vector2 position){
		GameObject projectileObject = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

		Projectile projectile = projectileObject.GetComponent<Projectile>();
		projectile.Fire(position);

		lastAttackTime = Time.time;
	}
}