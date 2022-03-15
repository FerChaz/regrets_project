using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using scripts.sceneManager.sceneController;

namespace scripts.limbo.ExitLimbo
{
    public class ExitLimbo : MonoBehaviour
    {
        #region Variables

        public LimboInfo limboInfo;

        public SceneController sceneController;

        // CAMBIAR POSICION A LA QUE ESTABA, HAY QUE VER CON LOS PINCHES
        // DESCARGAR LA ESCENA DE LIMBO
        // ACTIVAR LOS ENEMIGOS
        // DAR UNOS SEGUNDOS DE INVULNERABILIDAD Y ALGUNA ANIMACION

        public GameObject transitionCanvas;
        public Animator canvasAnimator;

        private WaitForSeconds wait = new WaitForSeconds(.5f);

        #endregion

        #region Awake & Start

        private void Awake()
        {
            sceneController = FindObjectOfType<SceneController>();
            transitionCanvas = GameObject.Find("TransitionCanvas");
            canvasAnimator = transitionCanvas.GetComponentInChildren<Animator>();
        }

        #endregion

        #region OnTriggerEnter

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                // FADE O ANIMACION
                CanvasTransition();

                StartCoroutine(WaitForFade());
            }
        }

        private IEnumerator WaitForFade()
        {
            yield return wait;

            limboInfo.isPlayerInLimbo = false;
            sceneController.ChangePlayerPosition(limboInfo.deathPosition);
            sceneController.UnloadSceneInAdditive(limboInfo.limboScene, OnSceneComplete);
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
