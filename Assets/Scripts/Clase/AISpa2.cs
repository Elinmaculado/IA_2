using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.Android;

[RequireComponent(typeof(NavMeshAgent))]
public class AISpa2 : MonoBehaviour
{
    public Transform player;
    public float fleeRange = 3f;
    private NavMeshAgent agent;
    
    public Transform[] patrolPoints = new Transform[3];
    private int patrolIndex = 0;

    private void Start()
    {
        gameObject.TryGetComponent(out agent); 
    }

    private void Update()
    {
        //SENSE
        float distance = Vector3.Distance(player.position, transform.position);
        Ray ray = new Ray(transform.position + (Vector3.up * 0.5f), player.position - transform.position);
        bool lineOfSight =false;

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.TryGetComponent(out PlayerMovement playah))
            {
                lineOfSight = true;
            }
        }
        float patrolPointDistance = Vector3.Distance(transform.position, patrolPoints[patrolIndex].position);
        if (!lineOfSight)
        {
            if (patrolPointDistance < 0.5f)
                patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }
        
        //PLAN
        if (lineOfSight)
        {
            if (distance > fleeRange)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                Vector3 dir = player.position - transform.position;
                Vector3 fleePos = transform.position + dir * 5;
                agent.SetDestination(fleePos);
            }
        }
        else
        {
            agent.SetDestination(patrolPoints[patrolIndex].position);
        }
    }
    
    
}

