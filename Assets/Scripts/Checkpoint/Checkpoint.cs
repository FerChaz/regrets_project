using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using scripts.sceneManager.sceneController;

namespace scripts.checkpoint.checkpoint
{
    public class Checkpoint : MonoBehaviour
    {
        #region Variables

        public List<StringValue> scenesToChargeInAdditive;
        public StringValue checkpointSceneName;

        public RespawnInfo respawnInfo;
        public AdditiveScenesInfo additiveScenesScriptableObject;
        public SaveController saveController;

        public GameObject objectsToActivate;
        public List<GameObject> entrancesToDisable;

        private LifeController _lifeController;
        private SceneController _sceneController;
        private ParticleSystem _particle;

        [Header("Canvas")]
        public GameObject canvas;

        [Header("Transition Canvas")]
        public GameObject transitionCanvas;
        public Animator canvasAnimator;

        private WaitForSeconds wait = new WaitForSeconds(.5f);

        private string sceneToLoad;


        #endregion

        #region Awake & Start

        private void Awake()
        {
            saveController = FindObjectOfType<SaveController>();
            _lifeController = FindObjectOfType<LifeController>();
            _sceneController = FindObjectOfType<SceneController>();

            _particle = GetComponentInChildren<ParticleSystem>();

            transitionCanvas = GameObject.Find("TransitionCanvas");
            canvasAnimator = transitionCanvas.GetComponentInChildren<Animator>();
        }

        #endregion

        #region OnTriggerEnter || OnTriggerExit

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (respawnInfo.isRespawning)
                {
                    respawnInfo.isRespawning = false;
                    Revive();
                }
                else
                {
                    canvas.SetActive(true);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                canvas.SetActive(false);
                _particle.Stop();
            }
        }

        #endregion

        #region Active Checkpoint

        public void Pray()                                                  // Active Checkpoint
        {
            canvas.SetActive(false);

            _lifeController.RestoreMaxLife();
            respawnInfo.respawnPosition = transform.position;
            respawnInfo.sceneToRespawn = checkpointSceneName;
            respawnInfo.additiveScenesToCharge = scenesToChargeInAdditive;
            respawnInfo.checkpointActivename = gameObject.name; // REVISAR, TENGO QUE ASIGNARLE UN STRING VALUE

            //saveController.SaveData();

            _particle.Play();
        }

        #endregion

        #region Revive

        public void Revive()
        {
            canvas.SetActive(false);
            _particle.Play();

            _lifeController.RestoreMaxLife();

            objectsToActivate.SetActive(true);                      // Enable enemies

            foreach (GameObject entrance in entrancesToDisable)
            {
                entrance.SetActive(false);                          // Disable other entrances
            }

            foreach (StringValue scene in scenesToChargeInAdditive)
            {
                sceneToLoad = scene.actualScene;
                _sceneController.LoadSceneInAdditive(sceneToLoad, OnSceneComplete);
            }

            additiveScenesScriptableObject.actualScene = checkpointSceneName;
            additiveScenesScriptableObject.additiveScenes = scenesToChargeInAdditive;

            //saveController.SaveData();

            //StartCoroutine(WaitForFade());
        }

        #endregion

        #region CanvasTransition

        private void CanvasTransition()
        {
            // De negro a transparente
            canvasAnimator.SetBool("ToBlack", false);
        }

        private IEnumerator WaitForFade()
        {
            yield return wait;
            CanvasTransition();
        }

        #endregion

        #region OnSceneComplete

        private void OnSceneComplete()
        {
            Debug.Log($"OnScene async complete {gameObject.name}");
        }

        #endregion

    }

}
