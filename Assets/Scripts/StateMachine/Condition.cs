using System;
using UnityEngine;

public class Condition : ScriptableObject
{
    public virtual bool Check(StateMachine stateMachine)
    {
        return false;
    }
}

[Serializable]
public class Transition
{
    public Condition Condition;
    public State state;
}
