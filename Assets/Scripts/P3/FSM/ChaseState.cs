using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "FSM/Pet/States/Chase")]
public class ChaseState : State
{
    public float chaseDistance = 5f;
    
    public override void UpdateState(StateMachine stateMachine)
    {
        GameObject player = stateMachine.blackBoard.Get<GameObject>("Player");
        NavMeshAgent agent = stateMachine.GetComponent<NavMeshAgent>();

        if (player != null)
        {
            agent.SetDestination(player.transform.position);
        }
    }
}
