using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using scripts.sceneManager.sceneController;
using scripts.checkpoint.checkpoint;

namespace scripts.respawn.respawnController
{
    public class RespawnController : MonoBehaviour
    {
        #region Variables

        public SceneController sceneController;

        public Checkpoint checkpoint;

        public RespawnInfo respawnInfo;
        public AdditiveScenesInfo additiveScenesInfo;
        public GameObject checkpointObject;

        public GameObject transitionCanvas;
        public Animator canvasAnimator;

        private WaitForSeconds waitFade = new WaitForSeconds(.5f);
        private WaitForSeconds wait = new WaitForSeconds(1);

        #endregion

        #region Awake & Start

        private void Awake()
        {
            sceneController = FindObjectOfType<SceneController>();
            transitionCanvas = GameObject.Find("TransitionCanvas");
            canvasAnimator = transitionCanvas.GetComponentInChildren<Animator>();
        }

        #endregion

        #region Respawn

        public void Respawn()
        {
            CanvasTransition();

            StartCoroutine(WaitForFade());
        }

        private IEnumerator WaitForFade()
        {
            yield return waitFade;

            sceneController.ChangePlayerPosition(Vector3.zero);

            sceneController.UnloadSceneInAdditive(additiveScenesInfo.actualScene.actualScene, OnSceneComplete);

            foreach (StringValue scene in additiveScenesInfo.additiveScenes)
            {
                if (scene.actualScene != additiveScenesInfo.actualScene.actualScene)
                {
                    sceneController.UnloadSceneInAdditive(scene.actualScene, OnSceneComplete);
                }
            }

            sceneController.LoadSceneInAdditive(respawnInfo.sceneToRespawn.actualScene, OnSceneComplete);
            respawnInfo.isRespawning = true;

            StartCoroutine(WaitToChange());

        }

        IEnumerator WaitToChange()
        {
            yield return wait;
            sceneController.ChangePlayerPosition(respawnInfo.respawnPosition);
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
            Debug.Log($"OnScene async complete, {gameObject.name}");
        }

        #endregion

    }

}
