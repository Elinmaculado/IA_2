using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AISpa : MonoBehaviour
{
    public Transform player;
    public float fleeRange = 3f;
    private NavMeshAgent agent;

    private void Start()
    {
        gameObject.TryGetComponent(out agent);
    }

    private void Update()
    {
        // Empieza perecepción (sense)
        float distance = Vector3.Distance(player.position, transform.position);
        // Empieza planeación (plan)
        if (distance < fleeRange)
        {
            // Emíeza actuar (Act)
            Vector3 dir = player.position - transform.position;
            Vector3 fleePos = transform.position + dir * 5;
            agent.SetDestination(fleePos);
        }
        else
        {
            agent.SetDestination(player.position);
        }
    }
}
