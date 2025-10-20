using UnityEngine;
[CreateAssetMenu(fileName = "Catched", menuName = "FSM/Limbus/Teacher/Conditions/Catched")]
public class CatchedStudentCondition : Condition
{
    public override bool Check(StateMachine sm)
    {
        return sm.teacherBlackBoard.catchedStudent;
    }
}
