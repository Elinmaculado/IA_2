using System;
using UnityEngine;


public class HomemadeCondition : ScriptableObject
{
    public virtual bool Check(StateMachine stateMachine)
    {
        return false;
    }
}

[Serializable]
public class Transition
{
    public HomemadeCondition homemadeCondition;
    public State state;
}
