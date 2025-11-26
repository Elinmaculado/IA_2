using UnityEngine;

[CreateAssetMenu(fileName = "NoChargeCondition", menuName = "FSM/Conditions/Roomba/NoCharge")]
public class NoChargeState : HomemadeCondition
{
    public override bool Check(StateMachine sm)
    {
        return sm.context.battery <= 0;
    }
}
