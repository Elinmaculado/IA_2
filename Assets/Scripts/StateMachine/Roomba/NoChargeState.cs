using UnityEngine;

[CreateAssetMenu(fileName = "NoChargeCondition", menuName = "FSM/Conditions/Roomba/NoCharge")]
public class NoChargeState : Condition
{
    public override bool Check(StateMachine sm)
    {
        return sm.context.battery <= 0;
    }
}
