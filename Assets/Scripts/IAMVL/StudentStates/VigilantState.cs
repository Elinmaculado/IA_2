using UnityEngine;

[CreateAssetMenu(fileName = "VigilantState", menuName = "FSM/Limbus/Student/Vigilant")]
public class VigilantState : State
{
    [Header("Rotation")]
    public float rotationSpeed = 90f;

    [Header("Field of View")]
    public float viewDistance = 8f;
    public float viewAngle = 90f;
    private float rotatedAngle;

    public override void EnterState(StateMachine sm)
    {
        sm.studentBlackBoard.ClearBools();
        sm.studentBlackBoard.isVigilant = true;
        rotatedAngle = 0f;
        sm.studentBlackBoard.videoPlayer.clip = sm.studentBlackBoard.working;
        sm.studentBlackBoard.videoPlayer.Play();
        sm.studentBlackBoard.audioSource.Play();
    }

    public override void UpdateState(StateMachine sm)
    {
        float rotationStep = rotationSpeed * Time.deltaTime;
        sm.transform.Rotate(Vector3.up, rotationStep);
        rotatedAngle += Mathf.Abs(rotationStep);
        
        if (rotatedAngle >= 360f)
        {
            Debug.Log("Vuelta completa");
            rotatedAngle = 0f;
            
            sm.studentBlackBoard.isPlaying = true;
        }
        
        if (TeacherInFOV(sm))
        {
            sm.studentBlackBoard.isCaught = true;
        }
    }

    public override void ExitState(StateMachine sm)
    {
        
    }

    private bool TeacherInFOV(StateMachine sm)
    {
        var teacher = GameObject.FindGameObjectWithTag("Teacher");
        if (teacher == null) return false;

        Transform t = sm.transform;
        Vector3 dirToTeacher = (teacher.transform.position - t.position).normalized;
        float distanceToTeacher = Vector3.Distance(t.position, teacher.transform.position);

        if (distanceToTeacher > viewDistance)
            return false;

        float angleToTeacher = Vector3.Angle(t.forward, dirToTeacher);
        if (angleToTeacher > viewAngle / 2f)
            return false;

        if (Physics.Raycast(t.position, dirToTeacher, out RaycastHit hit, distanceToTeacher))
            return hit.collider.CompareTag("Teacher");

        return false;
    }
}
