using UnityEngine;

[CreateAssetMenu(fileName = "NoTrashCondition", menuName = "FSM/Conditions/Roomba/NoTrash")]
public class NoTrashCondition : Condition
{
    public override bool Check(StateMachine sm)
    {
        return GameObject.FindGameObjectsWithTag("Trash").Length == 0;
    }
}