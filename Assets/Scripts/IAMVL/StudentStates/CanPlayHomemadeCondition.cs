using UnityEngine;
[CreateAssetMenu(fileName = "CanPlay", menuName = "FSM/Limbus/Student/Conditions/CanPlay")]
public class CanPlayHomemadeCondition : HomemadeCondition
{
    public override bool Check(StateMachine sm)
    {
        return sm.studentBlackBoard.isPlaying;
    }
}
