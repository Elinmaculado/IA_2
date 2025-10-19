using UnityEngine;
[CreateAssetMenu(fileName = "VigilantCondition", menuName = "FSM/Limbus/Student/Conditions/Surveil")]
public class IsVigilantCondition : Condition
{
    public override bool Check(StateMachine sm)
    {
        return sm.studentBlackBoard.isVigilant;
    }
}
