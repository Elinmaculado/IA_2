using UnityEngine;
[CreateAssetMenu(fileName = "TeacherJudging", menuName = "FSM/Limbus/Teacher/Judge")]
public class JudgingStudentState : State
{
    [Header("Field of View")]
    public float viewDistance = 10f;
    public float viewAngle = 90f;
    public override void EnterState(StateMachine sm)
    {
        sm.teacherBlackBoard.catchedStudent = true;
        sm.transform.position = sm.teacherBlackBoard.student.transform.position;
    }

    public override void UpdateState(StateMachine sm)
    {
        if (StudentInFOV(sm))
        {
            //Debug.Log("El maestro detectó al alumno trabajando.");
            // Cambio de estado
        }
    }

    public override void ExitState(StateMachine sm)
    {
        sm.teacherBlackBoard.catchedStudent = true;
    }
    
    private bool StudentInFOV(StateMachine sm)
    {
        GameObject student = GameObject.FindGameObjectWithTag("Student");
        if (student == null)
        {
            //Debug.Log("No se encontró objeto con tag Student");
            return false;
        }

        StateMachine studentSM = student.GetComponent<StateMachine>();
        if (studentSM == null || studentSM.studentBlackBoard == null)
        {
            //Debug.Log("El Student no tiene StateMachine o Blackboard");
            return false;
        }

        if (!studentSM.studentBlackBoard.isWorking)
        {
            //Debug.Log("El Student no está en modo jugando");
            return false;
        }

        Transform t = sm.transform;
        Vector3 dirToStudent = (student.transform.position - t.position).normalized;
        float distanceToStudent = Vector3.Distance(t.position, student.transform.position);
        float angleToStudent = Vector3.Angle(t.forward, dirToStudent);

        if (distanceToStudent > viewDistance)
        {
            //Debug.Log("Fuera de rango de distancia");
            return false;
        }

        if (angleToStudent > viewAngle / 2f)
        {
            //Debug.Log("Fuera del ángulo de visión");
            return false;
        }

        if (Physics.Raycast(t.position, dirToStudent, out RaycastHit hit, distanceToStudent))
        {
            //Debug.Log($"Raycast golpeó: {hit.collider.name}");
            return hit.collider.gameObject.CompareTag("Student") || hit.collider.transform.root.CompareTag("Student");

        }

        //Debug.Log("Raycast no golpeó nada");
        return false;
    }
}
