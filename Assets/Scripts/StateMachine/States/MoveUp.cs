using UnityEngine;

[CreateAssetMenu(fileName = "Move Up", menuName = "FSM/States/Move Up")]
public class MoveUp : State
{
    public float speed = 1.5f;
    public override void UpdateState(StateMachine stateMachine)
    {
        stateMachine.transform.Translate(stateMachine.transform.up * speed * Time.deltaTime, Space.Self);
    }
}
