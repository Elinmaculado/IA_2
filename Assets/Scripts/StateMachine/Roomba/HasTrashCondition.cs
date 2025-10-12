using UnityEngine;

[CreateAssetMenu(fileName = "HasTrashCondition", menuName = "FSM/Conditions/Roomba/HasTrash")]
public class HasTrashCondition : Condition
{
    public override bool Check(StateMachine sm)
    {
        return GameObject.FindGameObjectsWithTag("Trash").Length > 0;
    }
}