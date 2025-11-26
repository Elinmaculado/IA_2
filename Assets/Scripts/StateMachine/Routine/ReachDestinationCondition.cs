using UnityEngine;

[CreateAssetMenu(fileName = "ReachedDestinationCondition", menuName = "FSM/Conditions/ReachedDestination")]
public class ReachedDestinationHomemadeCondition : HomemadeCondition
{
    public float threshold = 0.2f;
    public string targetTag;

    public override bool Check(StateMachine sm)
    {
        var obj = GameObject.FindGameObjectWithTag(targetTag);
        if (obj == null) return false;
        return Vector3.Distance(sm.transform.position, obj.transform.position) < threshold;
    }
}