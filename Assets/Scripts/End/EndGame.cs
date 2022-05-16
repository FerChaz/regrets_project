using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        

        private void Awake()
        {
            diagUI = FindObjectOfType<VIDEUIManager1>();
            sceneController = FindObjectOfType<SceneController>();
            player = FindObjectOfType<PlayerController>();
            videPlayer = player.GetComponentInChildren<VIDEPlayer>();
        }

        public void EndBossFigth()
        {
            videPlayer.inTrigger = dialogue;
            diagUI.Interact(dialogue);
        }

        public void EndDialogue()
        {
            videPlayer.inTrigger = null;
            EndBossFigth();
        }


        public void GoodDecision()
        {
            bossDecision.actualScene = decisionGood;
            EndBossFigth();
        }

        public void BadDecision()
        {
            bossDecision.actualScene = decisionBad;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                ChangeScene();
            }
        }

        public void ChangeScene()
        {
            SceneManager.LoadScene("MainMenu");
        }

    }
}

