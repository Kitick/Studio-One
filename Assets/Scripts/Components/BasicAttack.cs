using UnityEngine;
using System.Collections.Generic;

public class BasicAttack : AttackBase {
	[SerializeField] private float damage = 5f;
	[SerializeField] private float cooldown = 1f;
	[SerializeField] private float range = 1f;
	[SerializeField] private TargetTag targetTag = TargetTag.Enemy;

	[SerializeField] private AudioClip attackSound;
	private AudioSource audioSource;

	private float lastAttackTime;

	public float Damage { get { return damage; } }
	public float Range { get { return range; } }
	public float AttackSpeed { get { return 1 / cooldown; } }


	private void Awake(){
		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.clip = attackSound;
	}

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

		audioSource.Play();
		defense.TakeDamage(Defense.DefenseType.Armor, damage);

		lastAttackTime = Time.time;
	}
}