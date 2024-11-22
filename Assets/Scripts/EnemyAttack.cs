using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //range at which the attack can trigger
    public float Range = 1.5f;
    public float attackCooldown = 1.5f;

    private Transform target;
    private bool isAttacking = false;
    private float lastAttack = 0f; //time of last attack

    //This is where edits to attack behavior like damage should go
    void AttackTarget()
    {
        if (!isAttacking && Vector2.Distance(transform.position, target.position) <= Range)
        {
            isAttacking = true; //begin attack
            lastAttack = Time.time; //set time of last attack

            //HERE IS WHERE ATTACK SHIT GOES
            Debug.Log("Attacking: " + target.name);


            //reset attack status after cooldown
            StartCoroutine(ResetAttackCooldown());
        }
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }



    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            //will attack if it can
            if (Vector2.Distance(transform.position, target.position) <= Range)
            {
                AttackTarget();
            }
        }
    }
}
