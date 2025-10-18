using System;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State initialState;
    public State currentState;
    public FSMContext context = new FSMContext();
    
    public BlackBoard blackBoard = new BlackBoard();
    public StudentBlackBoard studentBlackBoard;
    public TeacherBlackBoard teacherBlackBoard = new TeacherBlackBoard();

    private void Start()
    {
        blackBoard.Set("Player", GameObject.FindGameObjectWithTag("Player"));
        changeState(initialState);
    }

    public void changeState(State state)
    {
        if(currentState == state || state == null)
            return;
        if (currentState != null)
        {
            currentState.ExitState(this);
        }
        
        currentState = state;
        currentState.EnterState(this);
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(this);
            currentState.CheckTransitions(this);
        }
    }
    
}

// Opcional por si se quieren pasar info entre states
[Serializable]
public class FSMContext
{
    public int battery = 3;
    public GameObject playah;
    public LayerMask layer;
}
