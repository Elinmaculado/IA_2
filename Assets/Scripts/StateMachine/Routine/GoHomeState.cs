using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "GoHomeState", menuName = "FSM/States/Routine/GoHome")]
public class GoHomeState : State
{
    public float moveSpeed = 3f;
    public float restTime = 3f;
    private Transform home;
    private bool arrived = false;

    public override void EnterState(StateMachine sm)
    {
        var obj = GameObject.FindGameObjectWithTag("House");
        home = obj != null ? obj.transform : null;
        arrived = false;
    }

    public override void UpdateState(StateMachine sm)
    {
        if (home == null) return;

        if (!arrived)
        {
            Vector3 target = new Vector3(home.position.x, sm.transform.position.y, home.position.z);
            Vector3 dir = (target - sm.transform.position).normalized;
            sm.transform.position += dir * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(sm.transform.position, target) < 0.2f)
            {
                arrived = true;
                sm.StartCoroutine(Rest(sm));
            }
        }
    }

    private IEnumerator Rest(StateMachine sm)
    {
        yield return new WaitForSeconds(restTime);
    }
}