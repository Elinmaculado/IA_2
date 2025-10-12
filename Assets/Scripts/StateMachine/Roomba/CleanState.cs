using UnityEngine;

[CreateAssetMenu(fileName = "CleanState", menuName = "FSM/States/Roomba/Clean")]
public class CleanState : State
{
    public float moveSpeed = 2f;
    private GameObject target;

    public override void EnterState(StateMachine sm)
    {
        FindNearestTrash(sm);
    }

    public override void UpdateState(StateMachine sm)
    {
        if (target == null)
        {
            FindNearestTrash(sm);
            return;
        }

        Vector3 dir = (target.transform.position - sm.transform.position).normalized;
        sm.transform.position += dir * moveSpeed * Time.deltaTime;

        if (Vector3.Distance(sm.transform.position, target.transform.position) < 0.5f)
        {
            Object.Destroy(target);
            target = null;
        }
    }

    private void FindNearestTrash(StateMachine sm)
    {
        var trash = GameObject.FindGameObjectsWithTag("Trash");
        float minDist = float.MaxValue;
        foreach (var t in trash)
        {
            float d = Vector3.Distance(sm.transform.position, t.transform.position);
            if (d < minDist)
            {
                minDist = d;
                target = t;
            }
        }
    }
}