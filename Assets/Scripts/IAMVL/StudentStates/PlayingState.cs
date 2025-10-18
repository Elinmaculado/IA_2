using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "PlayingState", menuName = "FSM/Limbus/Student/Playing")]
public class PlayingState : State
{
    public override void EnterState(StateMachine sm)
    {
        sm.studentBlackBoard.isPlaying = true;
        sm.studentBlackBoard.screen = GameObject.FindGameObjectWithTag("Screen");
        //if (sm.studentBlackBoard.screen != null)
        //    Debug.Log($"Se encontró" + sm.studentBlackBoard.screen.name);
        sm.studentBlackBoard.videoPlayer = sm.studentBlackBoard.screen.GetComponent<VideoPlayer>();
        sm.studentBlackBoard.videoPlayer.Play();
    }
    public override void UpdateState(StateMachine sm)
    {
        
    }
}
