using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using scripts.sceneManager.sceneController;

namespace scripts.sceneManager.startingScene
{
    public class StartingScene : MonoBehaviour
    {
        #region Variables

        public SceneController sceneManager;

        public GameObject loadingCanvas;

        public RespawnInfo respawnInfo;
        public AdditiveScenesInfo sceneInfo;

        public Vector3 initialPosition;
        public StringValue initialScene;
        public List<StringValue> additiveScenes;

        public Animator playerAnimator;

        //Stored current VA when inside
        public VIDE_Assign dialogue;

        //Reference to our diagUI script for quick access
        public VIDEUIManager1 diagUI;

        public ObjectStatus eventHappened;

        private string sceneToGo;

        #endregion

        #region Awake & Start

        private void Start()
        {
            if (respawnInfo.isRespawning)                                                       // Se cargo una partida 
            {
                sceneToGo = respawnInfo.sceneToRespawn.actualScene;
                sceneManager.LoadSceneInAdditive(sceneToGo, OnSceneComplete);
                StartCoroutine(WaitToChange(respawnInfo.respawnPosition));
            }
            else                                                                                // Empezamos desde 0
            {
                sceneManager.LoadSceneInAdditive(initialScene.actualScene, OnSceneComplete);
                if (additiveScenes.Count > 0)
                {
                    sceneToGo = additiveScenes[0].actualScene;
                    sceneManager.LoadSceneInAdditive(sceneToGo, OnSceneComplete);
                }
                

                sceneInfo.additiveScenes = additiveScenes;
                sceneInfo.actualScene = initialScene;

                StartCoroutine(WaitToChange(initialPosition));
            }


            loadingCanvas.SetActive(false);
        }

        #endregion

        #region Update

        /*private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //diagUI.Interact(dialogue);
                if (eventHappened.eventAlreadyHappened)
                {
                    this.gameObject.SetActive(false);
                    return;
                }
                else
                {

                }
            }
        }*/

        #endregion

        #region CO WaitToChange

        private IEnumerator WaitToChange(Vector3 position)
        {
            yield return new WaitForSeconds(1.5f);
            sceneManager.ChangePlayerPosition(position);
            //diagUI.Interact(dialogue);
        }

        #endregion

        #region Stand

        public void Stand()                                 //Se lo llama desde el dialogo
        {
            //playerAnimator.SetBool("Stand", true);
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
