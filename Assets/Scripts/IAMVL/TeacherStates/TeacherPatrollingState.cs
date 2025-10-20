using UnityEngine;
[CreateAssetMenu(fileName = "TeacherPatrolling", menuName = "FSM/Limbus/Teacher/Patrol")]
public class TeacherPatrollingState : State
{
    [Header("Field of View")]
    public float viewDistance = 10f;
    public float viewAngle = 90f;
    
    public float moveSpeed = 3f;
    public float reachThreshold = 0.5f;
    private int currentPatrolIndex;

    public override void EnterState(StateMachine sm)
    {
        sm.teacherBlackBoard.isPatrolling = true;
        currentPatrolIndex = 0;
    }

    public override void UpdateState(StateMachine sm)
    {
        Patrol(sm);
        
        if (StudentInFOV(sm))
        {
            Debug.Log("El maestro detectó al alumno jugando.");
            sm.teacherBlackBoard.catchedStudent = true;
            // Cambio de estado
        }
        //Debug.Log(StudentInFOV(sm));
    }

    public override void ExitState(StateMachine sm)
    {
        sm.teacherBlackBoard.isPatrolling = false;
    }

    public void Patrol(StateMachine sm)
    {
        var points = sm.teacherBlackBoard.patrolPoints;
        if (points == null || points.Length == 0) return;

        Transform target = points[currentPatrolIndex];
        Vector3 dir = (target.position - sm.transform.position).normalized;

        sm.transform.position += dir * moveSpeed * Time.deltaTime;
        sm.transform.LookAt(target.position);

        float distance = Vector3.Distance(sm.transform.position, target.position);
        if (distance <= reachThreshold)
        {
            currentPatrolIndex++;
            if (currentPatrolIndex >= points.Length)
                currentPatrolIndex = 0;
        }
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

        if (!studentSM.studentBlackBoard.isPlaying)
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
