using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "SWorkingState", menuName = "FSM/Limbus/Student/Working")]
public class Workingstate : State
{
    public override void EnterState(StateMachine sm)
    {
        sm.studentBlackBoard.ClearBools();
        sm.studentBlackBoard.isWorking = true;
        sm.transform.rotation = Quaternion.LookRotation(Vector3.forward);
        sm.studentBlackBoard.videoPlayer.clip = sm.studentBlackBoard.working;
        sm.studentBlackBoard.videoPlayer.Play();
    }
    public override void UpdateState(StateMachine sm)
    {
    }

    public override void ExitState(StateMachine sm)
    {
        sm.studentBlackBoard.isWorking = false;
    }
}
