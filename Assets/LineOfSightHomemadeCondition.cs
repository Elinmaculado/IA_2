using System;
using Unity.Behavior;
using UnityEngine;
using Object = System.Object;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(name: "Line Of Sight", story: "[Agent] has line of sight of [Player]", category: "Conditions", id: "3d4668c839dbfb45716998247d031afd")]
public partial class LineOfSightHomemadeCondition : Condition
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Player;

    public override bool IsTrue()
    {
        Ray ray = new Ray(Agent.Value.transform.position, Player.Value.transform.position - Agent.Value.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == Player.Value)
            {
                return true;
            }
        }
    return false;
}

    public override void OnStart()
    {
    }

    public override void OnEnd()
    {
    }
}
