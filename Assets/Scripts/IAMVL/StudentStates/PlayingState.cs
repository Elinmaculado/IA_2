using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "PlayingState", menuName = "FSM/Limbus/Student/Playing")]
public class PlayingState : State
{
    public override void EnterState(StateMachine sm)
    {
        sm.studentBlackBoard.isPlaying = true;
        sm.studentBlackBoard.videoPlayer.clip = sm.studentBlackBoard.limbus;
        sm.studentBlackBoard.videoPlayer.Play();
    }
    public override void UpdateState(StateMachine sm)
    {
        sm.studentBlackBoard.isPlaying = false;
    }
}
