using UnityEngine;
[CreateAssetMenu(fileName = "TeacherPlaying", menuName = "FSM/Limbus/Teacher/PlayRimworld")]
public class TeacherPlayingState : State
{
    public override void EnterState(StateMachine sm)
    {
        sm.teacherBlackBoard.isPlayig = true;
        sm.transform.position = sm.teacherBlackBoard.screenPlay.position;
        sm.transform.rotation = Quaternion.LookRotation(Vector3.back);
        sm.teacherBlackBoard.videoPlayer.Play();
        sm.teacherBlackBoard.audioSource.Play();
    }
    public override void UpdateState(StateMachine sm)
    {
        
    }


    public override void ExitState(StateMachine sm)
    {
        sm.teacherBlackBoard.isPlayig = false;
    }
}
