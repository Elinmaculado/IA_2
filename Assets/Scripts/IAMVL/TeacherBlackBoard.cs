using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Video;

public class TeacherBlackBoard : MonoBehaviour
{
    public bool isPatrolling;
    public bool isPlayig;
    public bool catchedStudent;
    public Transform student;
    public Transform screenPlay;
    public Transform[] patrolPoints;

    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
	
}
