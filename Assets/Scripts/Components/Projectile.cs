using UnityEngine;
using System.Collections.Generic;

public class Projectile : AttackBase {
	[SerializeField] private int damage = 5;
	[SerializeField] private float aoe = 1f;
	[SerializeField] private float speed = 10f;
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

	public void Fire(Vector2 position){
		audioSource.Play();

		targetPosition = position;
		fired = true;
	}

	private void HitTarget(){
		List<GameObject> targets = FindTargets(aoe);

		targets.RemoveAll(target => target.GetComponent<Defense>() == null);

		foreach(GameObject target in targets){
			Defense defense = target.GetComponent<Defense>();

			defense.DamageWith(damageType, damage);
		}

		Destroy(gameObject);
	}
}