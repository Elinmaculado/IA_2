using UnityEngine;

[CreateAssetMenu(fileName = "TimerCondition", menuName = "FSM/Conditions/Timer")]
public class TimerHomemadeCondition : HomemadeCondition
{
    public float waitTime = 3f;
    private float timer;

    public override bool Check(StateMachine sm)
    {
        timer += Time.deltaTime;
        if (timer >= waitTime)
        {
            timer = 0f;
            return true;
        }
        return false;
    }
}