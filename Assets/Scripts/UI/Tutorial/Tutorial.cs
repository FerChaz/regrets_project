using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using scripts.sceneManager.startingScene;

namespace scripts.ui.tutorial.tutorial
{
    public class Tutorial : MonoBehaviour
    {
        #region Variables

        public GameObject canvas;
        public Image canvasImage;

        public ObjectStatus eventHappened;

        public PlayerController playerController;

        public ParticleSystem effect;
        public GameObject model;

        public StartingScene startingSceneController;

        #endregion

        #region Awake & Start

        private void Awake()
        {
            if (eventHappened.eventAlreadyHappened)
            {
                this.gameObject.SetActive(false);
            }

            playerController = FindObjectOfType<PlayerController>();
            startingSceneController = FindObjectOfType<StartingScene>();
        }

        #endregion

        #region OnTriggerEnter && OnTriggerExit

        private void OnTriggerExit(Collider other)
        {
            UnShow();
        }

        private void OnTriggerEnter(Collider other)
        {
            Show();
        }

        #endregion

        #region Show && UnShow

        public void Show()
        {
            eventHappened.eventAlreadyHappened = true;
            canvas.SetActive(true);

        }

        public void UnShow()
        {
            // FADE OUT Y DESPUES DESACTIVAR
            canvas.SetActive(false);
            this.gameObject.SetActive(false);
        }

        #endregion

        #region PlayerMovement

        public void PlayerMovement(bool enabled)
        {
            playerController.CanDoAnyMovement(enabled);
        }

        #endregion
    }

}
