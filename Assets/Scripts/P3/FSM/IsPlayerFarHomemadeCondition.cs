using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Pet/Conditions/IsPlayerFar")]
public class IsPlayerFarHomemadeCondition : HomemadeCondition
{
    public float chaseDistance = 5f;

    public override bool Check(StateMachine stateMachine)
    {
        GameObject player = stateMachine.blackBoard.Get<GameObject>("Player");
        if (player == null) return false;

        float distance = Vector3.Distance(stateMachine.transform.position, player.transform.position);
        return distance > chaseDistance;
    }
}
