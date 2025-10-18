using UnityEngine;

[CreateAssetMenu(fileName = "SWorkingState", menuName = "FSM/Limbus/Student/Working")]
public class Workingstate : State
{
    public override void EnterState(StateMachine sm)
    {
        sm.studentBlackBoard.isWorking = true;
        sm.studentBlackBoard.videoPlayer.clip = sm.studentBlackBoard.working;
        sm.studentBlackBoard.videoPlayer.Play();
    }
    public override void UpdateState(StateMachine sm)
    {
        sm.studentBlackBoard.isWorking = false;
    }
}
