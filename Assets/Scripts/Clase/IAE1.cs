using UnityEngine;
using UnityEngine.AI;

public class IAE1 : MonoBehaviour
{
    [Header("Info jugador")]
    private GameObject player;
    private Transform playerTransform;
    private MeshRenderer playerMeshRenderer;

    private NavMeshAgent agent;

    [Header("Vision")]
    public float viewDistance = 10f;
    public float viewAngle = 90f;
    private float distanceToPlayer;

    [Header("Patrol info")]
    public Transform[] patrolPoints = new Transform[4];
    private int patrolIndex = 0;
    public float distanceCheck = 1;
    
    
    private enum State {Patrol, Chase, Watch }
    private State currentState = State.Patrol;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            playerMeshRenderer = player.GetComponent<MeshRenderer>();
        }
        TryGetComponent(out agent);
    }

    void Update()
    {
        Sense();
        Plan();
        Act();
    }
    
    void Sense()
    {
        distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        
        if (Vector3.Distance(patrolPoints[patrolIndex].position, transform.position) < distanceCheck && currentState == State.Patrol)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }
    }
    
    void Plan()
    {

        if (!PlayerInFOV())
        {
            currentState = State.Patrol;
            return;
        }

        if (playerMeshRenderer.material.color == Color.white)
            currentState = State.Patrol;
        else if (playerMeshRenderer.material.color == Color.black)
            currentState = State.Chase;
        else if (playerMeshRenderer.material.color == Color.yellow)
            currentState = State.Watch;
    }
    
    void Act()
    {
        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Chase:
                Chase();
                break;
            case State.Watch:
                Watch();
                break;
        }
    }
    
    private void Patrol()
    {
        agent.SetDestination(patrolPoints[patrolIndex].position);
    }

    private void Chase()
    {
        agent.SetDestination(playerTransform.position);
    }

    private void Watch()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(playerTransform);
    }
    
    private bool PlayerInFOV()
    {
        Vector3 dirToPlayer = (playerTransform.position - transform.position).normalized;

        if (distanceToPlayer > viewDistance)
            return false;

        float angleToPlayer = Vector3.Angle(transform.forward, dirToPlayer);
        if (angleToPlayer > viewAngle / 2f)
            return false;

        if (Physics.Raycast(transform.position, dirToPlayer, out RaycastHit hit, distanceToPlayer))
        {
            if (hit.collider.gameObject == player)
                return true;

            return false;
        }

        return false;
    }

    // Esto se lo pedí al GPTo para poder visualizar las cosas xd
    void OnDrawGizmosSelected()
    {
        // Radio de visión
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        // Límites del ángulo de visión
        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2f, 0) * transform.forward;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2f, 0) * transform.forward;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * viewDistance);

        // Línea hacia el jugador
        if (playerTransform != null)
        {
            Gizmos.color = PlayerInFOV() ? Color.red : Color.gray;
            Gizmos.DrawLine(transform.position, playerTransform.position);
        }
    }
}
