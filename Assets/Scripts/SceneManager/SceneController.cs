using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace scripts.sceneManager.sceneController
{
    public class SceneController : MonoBehaviour
    {
        #region Variables

        [Header("Player")]
        public GameObject player;

        [Header("Music List")]
        public AudioClip[] musicList;
        private AudioSource musicSource;

        private UnityAction _onTaskComplete;

        #endregion

        #region Awake & Start

        private void Awake()
        {
            player = FindObjectOfType<PlayerController>().gameObject;
            musicSource = GetComponent<AudioSource>();
        }

        #endregion

        #region ChangePlayerPosition

        public void ChangePlayerPosition(Vector3 positionToGo)
        {
            player.transform.position = positionToGo;
        }

        #endregion

        #region Load & Unload Scenes

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

        #endregion

        #region MusicController

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

        #endregion

        #region UnityAction

        private void OnAsyncOpCompleted(AsyncOperation obj)
        {
            _onTaskComplete?.Invoke();
        }

        #endregion

    }

}
