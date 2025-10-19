using UnityEngine;
[CreateAssetMenu(fileName = "CanPlay", menuName = "FSM/Limbus/Student/Conditions/CanPlay")]
public class CanPlayCondition : Condition
{
    public override bool Check(StateMachine sm)
    {
        return sm.studentBlackBoard.isPlaying;
    }
}
