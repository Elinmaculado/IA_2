using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Pet/States/Wait")]
public class WaitState : State
{
    public float waitTime = 3f;
    private float timer;

    public override void EnterState(StateMachine stateMachine)
    {
        timer = 0f;
    }

    public override void UpdateState(StateMachine stateMachine)
    {
        timer += Time.deltaTime;
    }

    public override void ExitState(StateMachine stateMachine)
    {
        timer = 0f;
    }

    public bool IsDoneWaiting() => timer >= waitTime;
}
