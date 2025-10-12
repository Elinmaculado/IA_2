using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleState", menuName = "FSM/States/Roomba/IdleState")]
public class IdleState : State
{
    public int consuptionAmount = 1;
    public float consumptionRate = 1;
    public override void EnterState(StateMachine stateMachine)
    {
        stateMachine.StartCoroutine(DepletCharge(stateMachine));
    }
    public override void UpdateState(StateMachine stateMachine)
    {
        
    }

    public IEnumerator DepletCharge(StateMachine sm)
    {
        while (sm.context.battery > 0)
        {
            sm.context.battery -= consuptionAmount;
            yield return new WaitForSeconds(consumptionRate);
        }
    }
}
