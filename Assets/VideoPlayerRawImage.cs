using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public float delayBeforeSceneTransition = 1f;
    private bool videoFinished = false;

    void Start()
    {
        videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        videoFinished = true;
        StartCoroutine(DelayedSceneTransition());
    }

    IEnumerator DelayedSceneTransition()
    {
        yield return new WaitForSeconds(delayBeforeSceneTransition);
        // Transition to the next scene
        SceneManager.LoadScene("Play");
    }
}
