using UnityEngine;

[CreateAssetMenu(fileName = "CanPlayRimworld", menuName = "FSM/Limbus/Teacher/Conditions/CanPlay")]
public class TeacherCanPlayCondition : Condition
{
    public override bool Check(StateMachine sm)
    {
        return sm.teacherBlackBoard.isPlayig;
    }
}
