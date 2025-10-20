using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayingState", menuName = "FSM/Limbus/Student/Playing")]
public class PlayingState : State
{
    public float timeBetweenChecks = 5f;
    public override void EnterState(StateMachine sm)
    {
        sm.studentBlackBoard.ClearBools();
        sm.studentBlackBoard.isPlaying = true;
        sm.studentBlackBoard.videoPlayer.clip = sm.studentBlackBoard.limbus;
        sm.studentBlackBoard.videoPlayer.Play();
        sm.StartCoroutine(checkForTeacher(sm, timeBetweenChecks));

    }
    public override void UpdateState(StateMachine sm)
    {
        
    }

    public override void ExitState(StateMachine sm)
    {
        sm.studentBlackBoard.isPlaying = false;
    }
    
    private IEnumerator checkForTeacher(StateMachine sm, float delay)
    {
        yield return new WaitForSeconds(delay);
        sm.studentBlackBoard.isVigilant = true;
    }
}
