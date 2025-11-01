using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "FSM/Pet/States/GoHome")]
public class GoToHomeState : State
{
    public Transform home;

    public override void EnterState(StateMachine stateMachine)
    {
        home = GameObject.FindGameObjectWithTag("Home").transform;
    }
    public override void UpdateState(StateMachine stateMachine)
    {
        NavMeshAgent agent = stateMachine.GetComponent<NavMeshAgent>();
        if (home != null)
        {
            agent.SetDestination(home.position);
        }
    }

}
