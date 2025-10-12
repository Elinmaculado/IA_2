using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "WorkState", menuName = "FSM/States/Routine/Work")]
public class WorkState : State
{
    public float workDuration = 3f;

    public override void EnterState(StateMachine sm)
    {
        sm.StartCoroutine(WaitAndEnd(sm));
    }

    private IEnumerator WaitAndEnd(StateMachine sm)
    {
        yield return new WaitForSeconds(workDuration);
    }
}