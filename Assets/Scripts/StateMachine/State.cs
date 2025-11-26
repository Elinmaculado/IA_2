using UnityEngine;

public abstract class State : ScriptableObject
{
    public Transition[]  transitions;

    public virtual void EnterState(StateMachine stateMachine)
    {
        
    }

    public virtual void ExitState(StateMachine stateMachine)
    {
        
    }

    public virtual void UpdateState(StateMachine stateMachine)
    {
        
    }

    public void CheckTransitions(StateMachine stateMachine)
    {
        foreach (var t in transitions)
        {
            if (t.homemadeCondition != null && t.homemadeCondition.Check(stateMachine))
            {
                stateMachine.changeState(t.state);
                break;
            }
        }
    }
    
    
}
