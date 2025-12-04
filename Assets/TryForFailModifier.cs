using System;
using Unity.Behavior;
using UnityEngine;
using Modifier = Unity.Behavior.Modifier;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Try for fail", story: "Try [Number] of Failures", category: "Flow", id: "e0f99752c08a9502ec7acaf38d73d698")]
public partial class TryForFailModifier : Modifier
{
    [SerializeReference] public BlackboardVariable<int> Number;
    internal int attemptAccount = 0;

    protected override Status OnStart()
    {
        if (Child == null)
            return Status.Failure;
        
        var status = StartNode(Child);
        if (status == Status.Failure || status == Status.Success)
        {
            return Status.Running;
        }
        return Status.Waiting;
    }

    protected override Status OnUpdate()
    {
        if (attemptAccount >= Number.Value)
        {
            attemptAccount = 0;
            Debug.Log("out");
            return Status.Failure;
        }

        Status status = Child.CurrentStatus;
        if (status == Status.Failure)
        {
            attemptAccount++;
            Debug.Log($"failed attempt {attemptAccount}");
            return Status.Running;
        }
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

