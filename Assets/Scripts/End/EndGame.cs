using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using scripts.sceneManager.sceneController;
using scripts.sceneManager.exitScene;
using scripts.sceneManager.enterScene;

namespace scripts.end.endgame
{
    public class EndGame : MonoBehaviour
    {
        //Reference To Player
        public PlayerController player;

        //Reference To VidePlayer
        public VIDEPlayer videPlayer;

        //Stored current VA when inside
        public VIDE_Assign dialogue;

        //Reference to our diagUI script for quick access
        public VIDEUIManager1 diagUI;

        public SceneController sceneController;

        public StringValue bossDecision;
        public string decisionGood;
        public string decisionBad;

        public GameObject door;
        private Animator doorAnimator;
        private BoxCollider doorCollider;
        private string sceneToLoad;


        private void Awake()
        {
            diagUI = FindObjectOfType<VIDEUIManager1>();
            sceneController = FindObjectOfType<SceneController>();
            player = FindObjectOfType<PlayerController>();
            videPlayer = player.GetComponentInChildren<VIDEPlayer>();

            doorCollider = door.GetComponent<BoxCollider>();
            doorAnimator = door.GetComponentInChildren<Animator>();
        }

        public void EndBossFigth()
        {
            doorAnimator.SetBool("Open", true);
            doorCollider.enabled = false;

            sceneToLoad = bossDecision.actualScene;
            sceneController.StartMusic(1);
            sceneController.LoadSceneInAdditive(sceneToLoad, OnSceneComplete);
        }


        public void GoodDecision()
        {
            bossDecision.actualScene = decisionGood;
            EndBossFigth();
        }

        public void BadDecision()
        {
            bossDecision.actualScene = decisionBad;
            EndBossFigth();
        }

        public void EnableDisablePlayerMovement()
        {
            player.ChangeCanDoAnyMovement();
        }


        #region OnSceneComplete

        private void OnSceneComplete()
        {
            Debug.Log($"OnScene async complete, {gameObject.name}");
        }

        #endregion



    }
}

