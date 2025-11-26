using UnityEngine;
[CreateAssetMenu(fileName = "VigilantCondition", menuName = "FSM/Limbus/Student/Conditions/Surveil")]
public class IsVigilantHomemadeCondition : HomemadeCondition
{
    public override bool Check(StateMachine sm)
    {
        return sm.studentBlackBoard.isVigilant;
    }
}
