using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class IAE1 : MonoBehaviour
{
    public GameObject player;
    public Transform playerTransform;

    public MeshRenderer playeMeshRenderer;
    
    private NavMeshAgent agent;
    
    public float viewDistance = 10;
    private float distanceToPlayer;
    public float viewAngle = 90f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        playeMeshRenderer = player.GetComponent<MeshRenderer>();
        gameObject.TryGetComponent(out agent);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInFOW())
            Debug.Log("Player in FOW");
        else
            Debug.Log("Player not in FOW");
        
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        // Sense
        if (playeMeshRenderer.material.color == Color.white)
        {
            // Implementar patrullaje normal
        }
        else if (playeMeshRenderer.material.color == Color.black)
        {
            agent.SetDestination(playerTransform.position);
        }
        else if (playeMeshRenderer.material.color == Color.yellow)
        {
            agent.SetDestination(transform.position);
            transform.LookAt(playerTransform);
        }

    }
    
    private bool PlayerInFOW()
    {
        Vector3 dirToPlayer = (player.transform.position - transform.position).normalized;
        if (distanceToPlayer > viewDistance)
            return false;

        float angleToPlayer = Vector3.Angle(transform.forward, dirToPlayer);
        if (angleToPlayer > viewAngle / 2)
            return false;

        if (Physics.Raycast(transform.position, dirToPlayer, out RaycastHit hit, distanceToPlayer))
        {
            if (hit.collider.gameObject.TryGetComponent(out PlayerMovement _))
            {
                return true;
            }

            return false;
        }

        return false;
    }
}
