using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Pet/Conditions/IsDoneWaiting")]
public class IsDoneWaitingHomemadeCondition : HomemadeCondition
{
    public override bool Check(StateMachine stateMachine)
    {
        var waitState = stateMachine.currentState as WaitState;
        return waitState != null && waitState.IsDoneWaiting();
    }
}
