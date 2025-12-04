using System;
using Unity.Behavior;
using UnityEngine;
using Composite = Unity.Behavior.Composite;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ElSeqncio2", story: "Ejecuta y regresa la mayoría", category: "Flow", id: "149a13ce793232f2e6a9970ac33db7a8")]
public partial class ElSeqncio2Sequence : Composite
{
    private int currentChild = 0;
    private float succesCount = 0;
    protected override Status OnStart()
    {
        currentChild = 0;
        succesCount = 0;
        StartChild(currentChild);
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }

    protected Status StartChild(int childIndex)
    {
        if (childIndex >= Children.Count)
        {
            return succesCount >= (float)Children.Count / 2 ? Status.Success : Status.Failure;
        }
        var status = StartNode(Children[childIndex]);
        if (status == Status.Success)
        {
            succesCount++;
            if (childIndex + 1 >= Children.Count)
            {
                return succesCount >= (float)Children.Count / 2 ? Status.Success : Status.Failure;
            }
            return StartChild(childIndex + 1);
        }
        else if (status == Status.Running)
        {
            return Status.Running;
        }
        else
        {
            if (childIndex + 1 >= Children.Count)
            {
                return succesCount >= (float)Children.Count / 2 ? Status.Success : Status.Failure;
            }
            return StartChild(childIndex + 1);
        }
    }
    
}

