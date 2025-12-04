using System;
using Unity.Behavior;
using UnityEngine;
using Modifier = Unity.Behavior.Modifier;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "El seqncio", story: "Ejecuta y regresa la mayoria", category: "Flow", id: "5c4db8379cffbe9668dc3e5c2132e674")]
public partial class ElSeqncioModifier : Modifier
{

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

