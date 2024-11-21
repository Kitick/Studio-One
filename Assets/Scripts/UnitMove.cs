using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMove : MonoBehaviour
{
    private Vector3 target;
    NavMeshAgent agent;
    

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        setTargetPosition();
        setAgentPosition();
    }

    void setTargetPosition()
    {
        if (Input.GetMouseButtonDown(1))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    void setAgentPosition()
    {
        agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));
    }

}