using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using scripts.sceneManager.sceneController;

namespace scripts.limbo.limboController
{
    public class LimboController : MonoBehaviour
    {
        #region Variables

        public SceneController sceneController;

        public LimboInfo limboInfo;

        private int _random;

        private WaitForSeconds wait = new WaitForSeconds(1);

        public GameObject transitionCanvas;
        public Animator canvasAnimator;

        #endregion

        #region Awake & Start

        private void Awake()
        {
            sceneController = FindObjectOfType<SceneController>();
            transitionCanvas = GameObject.Find("TransitionCanvas");
            canvasAnimator = transitionCanvas.GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            _random = 1;
        }

        #endregion

        #region LoadLimboScene && UnloadLimboScene

        public void ChargeLimboScene(Vector3 position)
        {
            CanvasTransition();
            limboInfo.deathPosition = position;

            if (_random == 1)
            {
                limboInfo.limboScene = "Limbo1";
                sceneController.LoadSceneInAdditive("Limbo1", OnSceneComplete);
                StartCoroutine(WaitToChange(limboInfo.positionToGoInLimbo1));

            }
            else
            {
                limboInfo.limboScene = "Limbo2";
                sceneController.LoadSceneInAdditive("Limbo2", OnSceneComplete);
                StartCoroutine(WaitToChange(limboInfo.positionToGoInLimbo2));
            }
        }

        public void UnloadLimboScene()
        {
            sceneController.StopMusic(true);
            if (_random == 1)
            {
                StartCoroutine(WaitToUnload("Limbo1"));
            }
            else
            {
                StartCoroutine(WaitToUnload("Limbo2"));
            }

        }

        #endregion

        #region WaitToUnloadCO && WaitToChangeCO

        IEnumerator WaitToUnload(string limbo)
        {
            yield return wait;
            sceneController.UnloadSceneInAdditive(limbo, OnSceneComplete);
        }


        IEnumerator WaitToChange(Vector3 position)
        {
            yield return wait;
            sceneController.ChangePlayerPosition(position);
            canvasAnimator.SetBool("ToBlack", false);
        }

        #endregion

        #region CanvasTransition

        private void CanvasTransition()
        {
            // De transparente a negro
            canvasAnimator.SetBool("ToBlack", true);
        }

        #endregion

        #region OnSceneComplete

        private void OnSceneComplete()
        {
            Debug.Log("OnScene async complete");
        }

        #endregion

    }

}
