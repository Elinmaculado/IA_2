using UnityEngine;

[CreateAssetMenu(fileName = "OpenShopState", menuName = "FSM/States/Routine/OpenShop")]
public class OpenShopState : State
{
    public float moveSpeed = 3f;
    private Transform shop;

    public override void EnterState(StateMachine sm)
    {
        var obj = GameObject.FindGameObjectWithTag("Shop");
        shop = obj.transform;
    }

    public override void UpdateState(StateMachine sm)
    {
        if (shop == null) 
            return;

        Vector3 target = new Vector3(shop.position.x, sm.transform.position.y, shop.position.z);
        Vector3 dir = (target - sm.transform.position).normalized;
        sm.transform.position += dir * moveSpeed * Time.deltaTime;
    }
}