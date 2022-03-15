using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using scripts.sceneManager.sceneController;

namespace scripts.sceneManager.exitScene
{
    public class ExitScene : MonoBehaviour
    {
        #region Variables

        [Header("SceneToGoData")]
        public List<string> scenesToUnload;
        public Vector3 playerPositionToGo;

        public SceneController _sceneManager;
        public string _actualScene;
        public string sceneToGo;

        public GameObject transitionCanvas;
        public Animator canvasAnimator;

        private WaitForSeconds wait = new WaitForSeconds(.5f);

        public PlayerController player;
        public PlayerAnimatorController playerAnimator;
        public float topTime = 1;
        private Vector3 movement;
        public int direction;

        public bool needToStopMusic;

        #endregion

        #region Awake & Start

        private void Awake()
        {
            _sceneManager = FindObjectOfType<SceneController>();
            transitionCanvas = GameObject.Find("TransitionCanvas");
            canvasAnimator = transitionCanvas.GetComponentInChildren<Animator>();

            player = FindObjectOfType<PlayerController>();
            playerAnimator = FindObjectOfType<PlayerAnimatorController>();
        }

        #endregion

        #region ExitPlayer

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                StartCoroutine(Move());
                movement.Set(15f * direction, 0.0f, 0.0f);

                CanvasTransition();

                StartCoroutine(WaitForFade());

                _sceneManager.StopMusic(needToStopMusic);
            }
        }

        #endregion

        #region CanvasTransition

        private void CanvasTransition()
        {
            // De transparente a negro

            canvasAnimator.SetBool("ToBlack", true);
        }

        private IEnumerator WaitForFade()
        {
            yield return wait;

            foreach (string scene in scenesToUnload)
            {
                if (scene != sceneToGo)                                           // Mejorar la comparacion de strings
                {
                    _sceneManager.UnloadSceneInAdditive(scene, OnSceneComplete);
                }
            }

            _sceneManager.ChangePlayerPosition(playerPositionToGo);
            _sceneManager.UnloadSceneInAdditive(_actualScene, OnSceneComplete);
        }

        #endregion

        #region ExitMovePlayer

        IEnumerator Move()
        {
            player.CanDoAnyMovement(false);
            float timer = 0;
            while (timer < topTime)
            {
                playerAnimator.Run(direction);
                player.rigidBody.velocity = movement;
                timer += Time.deltaTime;
                yield return null;
            }
            player.CanDoAnyMovement(true);
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
