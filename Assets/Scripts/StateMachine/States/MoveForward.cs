using UnityEngine;
[CreateAssetMenu(fileName = "State Patito", menuName = "FSM/States/StatePatito")]
public class MoveForward : State
{
    public float speed = 2f;

    public override void UpdateState(StateMachine stateMachine)
    {
        stateMachine.transform.Translate(stateMachine.transform.forward * speed * Time.deltaTime, Space.Self);
    }
}
