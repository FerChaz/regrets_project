using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using scripts.sceneManager.sceneController;

namespace scripts.ui.tutorial.introfirstdialogue
{
    public class IntroFirstDialogue : MonoBehaviour
    {
        //Reference To Player
        public PlayerController player;

        //Stored current VA when inside
        public VIDE_Assign dialogue;

        //Reference to our diagUI script for quick access
        public VIDEUIManager1 diagUI;

        public SceneController sceneController;

        public Animator playerAnimator;

        private void Awake()
        {
            diagUI = FindObjectOfType<VIDEUIManager1>();
            sceneController = FindObjectOfType<SceneController>();
            player = FindObjectOfType<PlayerController>();
            playerAnimator = player.GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                diagUI.Interact(dialogue);
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                diagUI.Interact(dialogue);
            }
        }

        #region Stand

        public void Stand()                                 //Se lo llama desde el dialogo
        {
            playerAnimator.SetBool("Stand", true);
        }

        #endregion

    }
}

