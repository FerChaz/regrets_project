using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class SceneController : MonoBehaviour
{
    [Header("DontDestroyOnLoad")]
    public GameObject player;
    public GameObject mainCamera;                                                           // Para establecer los límites

    [Header("Music List")]
    public AudioClip[] musicList;
    private AudioSource musicSource;

    private UnityAction _onTaskComplete;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        mainCamera = FindObjectOfType<MainCamera>().gameObject;

        musicSource = GetComponent<AudioSource>();
    }

    public void ChangePlayerPosition(Vector3 positionToGo)
    {
        player.transform.position = positionToGo;
    }

    public void LoadSceneInAdditive(string sceneToLoad, UnityAction callback)
    {
        _onTaskComplete = callback;
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        asyncOp.completed += OnAsyncOpCompleted;
    }


    public void UnloadSceneInAdditive(string sceneToUnload, UnityAction callback)
    {
        _onTaskComplete = callback;
        AsyncOperation asyncOp = SceneManager.UnloadSceneAsync(sceneToUnload);
        asyncOp.completed += OnAsyncOpCompleted;
    }



    private void OnAsyncOpCompleted(AsyncOperation obj)
    {
        _onTaskComplete?.Invoke();
    }


    //-- MUSIC CONTROLLER ----------------------------------------------------------------

    public void StartMusic(int musicIndex)
    {
        if (musicIndex > musicList.Length || musicIndex < 0)
        {
            return;
        }

        musicSource.clip = musicList[musicIndex];
        musicSource.Play();
    }

    public void StopMusic(bool needToStop)
    {
        if (needToStop)
        {
            musicSource.Stop();
        }
    }

}
