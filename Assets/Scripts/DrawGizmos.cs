using UnityEngine;

public class DrawGizmos : MonoBehaviour
{
    public StateMachine sm;

    void Awake()
    {
        sm = GetComponent<StateMachine>();
    }

    void OnDrawGizmos()
    {
        if (sm == null || sm.currentState == null)
            return;

        // solo dibujar si el estado actual tiene FOV
        if (sm.currentState is VigilantState fov)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, fov.viewDistance);

            Vector3 leftBoundary = Quaternion.Euler(0, -fov.viewAngle / 2f, 0) * transform.forward;
            Vector3 rightBoundary = Quaternion.Euler(0, fov.viewAngle / 2f, 0) * transform.forward;

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + leftBoundary * fov.viewDistance);
            Gizmos.DrawLine(transform.position, transform.position + rightBoundary * fov.viewDistance);
        }
    }
}