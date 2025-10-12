using UnityEngine;

[CreateAssetMenu(fileName = "RechargeState", menuName = "FSM/States/Roomba/Recharge")]
public class RechargeState : State
{
    public float moveSpeed = 3f;
    private Transform charger;

    public override void EnterState(StateMachine sm)
    {
        var chargerObj = GameObject.FindGameObjectWithTag("Charger");
        if (chargerObj != null)
            charger = chargerObj.transform;
    }

    public override void UpdateState(StateMachine sm)
    {
        if (charger == null) 
            return;
        Vector3 dir = (charger.position - sm.transform.position).normalized;
        sm.transform.position += dir * moveSpeed * Time.deltaTime;
    }
}