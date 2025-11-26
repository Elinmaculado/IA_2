using UnityEngine;
[CreateAssetMenu(fileName = "Catched", menuName = "FSM/Limbus/Teacher/Conditions/Catched")]
public class CatchedStudentHomemadeCondition : HomemadeCondition
{
    public override bool Check(StateMachine sm)
    {
        return sm.teacherBlackBoard.catchedStudent;
    }
}
