using UnityEngine;
using UnityEngine.Video;

public class StudentBlackBoard : MonoBehaviour
{
    public bool isWorking;
    public bool isPlaying;
    public bool isVigilant;
    public bool isCaught;

    [Header("Components")] 
    //public GameObject player;
    public VideoPlayer videoPlayer;
    public VideoClip limbus;
    public VideoClip working;

    public void ClearBools()
    {
        isWorking = false;
        isPlaying = false;
        isVigilant = false;
        isCaught = false;
    }
}
