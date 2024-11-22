using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float detectionRadius = 10f;
    public float followDistance = 1f;
    public float moveSpeed = 5f;
    private EnemyAttack enemyAttack; //references enemyAttack script

    private Transform target; //unit to be followed
    private UnityEngine.AI.NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = moveSpeed;

        enemyAttack = GetComponent<EnemyAttack>();
    }

    void FindNearbyUnits()
    {
        //overlap circle to get nearby units
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        float nearestDistance = Mathf.Infinity; //start with infinite range
        Transform nearestUnit = null;

        foreach(var collider in hitColliders)
        {
            if (collider.CompareTag("Unit"))
            {
                //find distance to units
                float distance = Vector2.Distance(transform.position, collider.transform.position);


                //find which unit is closest
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestUnit = collider.transform;
                }
            }
        }

        //track whatever unit is found from above
        target = nearestUnit;

        //sets target for the attack script
        enemyAttack.SetTarget(target);
    }

    void MoveToTarget()
    {
        //sets agent destination to target position
        Vector3 goal = new Vector3(target.position.x, target.position.y, target.position.z);
        agent.SetDestination(goal);
    }


    // Update is called once per frame
    void Update()
    {
        FindNearbyUnits();

        if (target != null)
        {
            MoveToTarget();
            
            //if target reached, stop moving
            if (Vector2.Distance(transform.position, target.position) <= followDistance)
            {
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
            }
        }
        else
        {
            //stop if no target
            agent.isStopped = true;
        }
    }
}
