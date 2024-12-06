using UnityEngine;
using System.Collections.Generic;

public class Projectile : AttackBase {
	[SerializeField] private int damage = 5;
	[SerializeField] private float speed = 10f;
	[SerializeField] private float effectArea = 1f;
	[SerializeField] private Defense.DamageType damageType = Defense.DamageType.Physical;

	private AudioSource audioSource;

	private Vector2 targetPosition;
	private bool fired = false;

	private void Awake(){
		audioSource = GetComponent<AudioSource>();
	}

	private void Update(){
		if(!fired){ return; }

		Vector2 difference = targetPosition - (Vector2)transform.position;

		Vector2 position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
		Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 90);

		transform.SetPositionAndRotation(position, rotation);

		if(Vector2.Distance(transform.position, targetPosition) < 0.1f){
			HitTarget();
		}
	}

	public void Fire(Vector2 position, float multiplier = 1){
		targetPosition = position;
		damage = (int)(damage * multiplier);

		audioSource.Play();
		fired = true;
	}

	private void HitTarget(){
		List<GameObject> targets = FindTargets(effectArea);

		targets.RemoveAll(target => target.GetComponent<Defense>() == null);

		foreach(GameObject target in targets){
			Defense defense = target.GetComponent<Defense>();

			defense.DamageWith(damageType, damage);
		}

		Destroy(gameObject);
	}
}