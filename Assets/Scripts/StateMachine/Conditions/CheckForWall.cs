using UnityEngine;

[CreateAssetMenu(fileName = "ConditionWall", menuName = "FSM/Conditions/ConditionWall")]
public class CheckForWall : Condition
{
    public float checkDistance = 1.5f;
    public LayerMask wallMask;
    public override bool Check(StateMachine stateMachine)
    {
        Ray ray = new Ray(stateMachine.transform.position, Vector3.forward);
        return Physics.Raycast(ray, checkDistance, wallMask);
    }
}
