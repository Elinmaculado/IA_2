using UnityEngine;
[CreateAssetMenu(fileName = "Caught", menuName = "FSM/Limbus/Student/Conditions/Caught")]
public class CaughtCondition : Condition
{
    public override bool Check(StateMachine sm)
    {
        return sm.studentBlackBoard.isCaught;
    }
}
