using UnityEngine;

[CreateAssetMenu(fileName = "CanPlayRimworld", menuName = "FSM/Limbus/Teacher/Conditions/CanPlay")]
public class TeacherCanPlayHomemadeCondition : HomemadeCondition
{
    public override bool Check(StateMachine sm)
    {
        return sm.teacherBlackBoard.isPlayig;
    }
}
