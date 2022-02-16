using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoController : MonoBehaviour
{

    public VideoPlayer video;
    public string sceneToGo;


    private void Awake()
    {
        video = GetComponent<VideoPlayer>();
        video.Play();
        video.loopPointReached += CheckOver;
    }
    

    void CheckOver(VideoPlayer vp)
    {
        SceneManager.LoadScene(sceneToGo);
    }
}
