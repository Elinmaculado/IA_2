using UnityEngine;
[CreateAssetMenu(fileName = "PatrollingState", menuName = "FSM/States/Teacher/Patrol")]
public class TeacherPatrollingState : State
{
    public GameObject teacher;
    public int patrolingIndex = 0;
    public Transform[] patrolPoints;
    public override void EnterState(StateMachine sm)
    {
        teacher = sm.gameObject;
    }

    public override void UpdateState(StateMachine sm)
    {
        
    }
    public override void ExitState(StateMachine sm)
    {
        
    }
}
