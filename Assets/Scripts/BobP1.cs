using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;
using TMPro;

public class BobP1 : MonoBehaviour
{
    private float health = 50;
    private float maxHealth = 100;

    public Transform player;

    private float distanceToPlayer;
    public float fleeDistance = 4f;
    private bool lineOfSight = false;

    private Dictionary<string, float> actionScores;

    public Transform[] patrolPoints = new Transform[4];
    private int patrolIndex = 0;
    public float distanceCheck = 1;

    private NavMeshAgent agentSmith;

    public TextMeshProUGUI fleeText;
    public TextMeshProUGUI chaseText;

    public float criticalHealthLimit = 0.3f;

    public float viewDistance = 5;
    public float viewAngle = 45f;
    private void Start()
    {
        actionScores = new Dictionary<string, float>()
        {
            {"Flee", 0f },
            {"Chase", 0f },
            {"Patrol" ,0f }
        };

        health = maxHealth;

        gameObject.TryGetComponent(out agentSmith);
    }

    private void Update()
    {
        //SENSE
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        lineOfSight = PlayerInFOW();
        Ray ray = new Ray(transform.position + (Vector3.up * 0.5f), player.position - transform.position);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            lineOfSight = hit.collider.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement _);
        }
        if (Vector3.Distance(patrolPoints[patrolIndex].position, transform.position) < distanceCheck)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }

        //PLAN 

        updatePrediction();
        float healthRatio = Mathf.Clamp01(health / maxHealth);
        float distanceRatio = Mathf.Clamp01(distanceToPlayer / fleeDistance);

        if (healthRatio <= criticalHealthLimit) { distanceRatio = 0; }

        float riskFactor = (1 - healthRatio) * (1 - distanceRatio);

        float aggroFactor = healthRatio * distanceRatio;

        float total = riskFactor + aggroFactor;

        riskFactor /= total;
        aggroFactor /= total;
        aggroFactor *= healthRatio > criticalHealthLimit ? 1 : 0;

        actionScores["Flee"] = riskFactor * 10 * (lineOfSight == true ? 1 : 0);
        actionScores["Chase"] = aggroFactor * 10 * (lineOfSight == true ? 1 : 0);
        actionScores["Patrol"] = 3f;

        fleeText.text = "FLEE = " + actionScores["Flee"];
        chaseText.text = "CHASE = " + actionScores["Chase"];

        string chosenAction = actionScores.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
        switch (chosenAction)
        {
            //ACT
            case "Flee":
                Flee();
                break;
            case "Chase":
                Chase();
                break;
            case "Patrol":
                Patrol();
                break;
            default:
                break;
        }
    }

    private bool PlayerInFOW()
    {
        Vector3 dirToPlayer = (player.position - transform.position).normalized;
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
    public void GetHit()
    {
        health -= 9;
        Debug.Log("Was Hit! Only have " +  health + " hp left.");
    }

    private void Flee()
    {
        Vector3 fleeDir = transform.position + (transform.position - player.position).normalized * 2;
        if (NavMesh.SamplePosition(fleeDir, out NavMeshHit hit, 1, NavMesh.AllAreas))
        {
            agentSmith.SetDestination(fleeDir);
        }
        else
        {
            agentSmith.SetDestination(FindFleeAlternative(fleeDir));
        }

    }

    private void Chase()
    {
        agentSmith.SetDestination(predictedPlayerPos);
    }

    private void Patrol()
    {
        agentSmith.SetDestination(patrolPoints[patrolIndex].position);
    }

    #region FleeAlternative

    public float maxDistanceFromDirection = 100;
    public float step = 10f;
    public float fleeLength = 3f;

    private Vector3 FindFleeAlternative(Vector3 fleeDirection)
    {
        float maxDistanceFromPlayer = 0;
        Vector3 bestPosition = transform.position;

        for (float angle = -maxDistanceFromPlayer; angle <= maxDistanceFromDirection; angle += step)
        {
            Vector3 dir = Quaternion.Euler(0, angle, 0) * fleeDirection;
            Vector3 candidate = transform.position + dir * fleeLength;
            if (NavMesh.SamplePosition(candidate, out NavMeshHit hit, 1f, NavMesh.AllAreas))
            {
                float distToPlayer = Vector3.Distance(candidate, player.position);
                if (distToPlayer > maxDistanceFromPlayer)
                {
                    maxDistanceFromPlayer = distToPlayer;
                    bestPosition = transform.position;
                }
            }
        }
        return bestPosition;
    }
    #endregion

    #region predict

    Vector3 lastPlayerPosition = new Vector3();
    Vector3 predictedPlayerPos = new Vector3();

    private void updatePrediction()
    {
        Vector3 currentPlayerPosition = player.position;
        Vector3 moveDirection = (currentPlayerPosition - lastPlayerPosition).normalized;

        float predictionDistance = distanceToPlayer * 0.5f;
        
        predictedPlayerPos = currentPlayerPosition + moveDirection * predictionDistance;
        lastPlayerPosition = currentPlayerPosition;
    }

    #endregion

}