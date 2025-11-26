using UnityEngine;

[CreateAssetMenu(fileName = "NoTrashCondition", menuName = "FSM/Conditions/Roomba/NoTrash")]
public class NoTrashHomemadeCondition : HomemadeCondition
{
    public override bool Check(StateMachine sm)
    {
        return GameObject.FindGameObjectsWithTag("Trash").Length == 0;
    }
}