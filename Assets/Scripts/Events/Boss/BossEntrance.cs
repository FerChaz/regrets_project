using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;
using scripts.sceneManager.sceneController;

public class BossEntrance : MonoBehaviour
{
    public BossController bossController;
    public SceneController sceneController;
    public int musicIndex;

    //Reference To Player
    public PlayerController player;

    //Reference To VidePlayer
    public VIDEPlayer videPlayer;

    //Stored current VA when inside
    public VIDE_Assign dialogue;

    //Reference to our diagUI script for quick access
    public VIDEUIManager1 diagUI;

    public ObjectStatus eventHappened;

    private void Awake()
    {
        diagUI = FindObjectOfType<VIDEUIManager1>();
        sceneController = FindObjectOfType<SceneController>();
        player = FindObjectOfType<PlayerController>();
        videPlayer = player.GetComponentInChildren<VIDEPlayer>();
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            /*if (eventHappened.eventAlreadyHappened)
            {
                return;
            }
            //else
            //{
                diagUI.Interact(dialogue);
            //}
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
            videPlayer.inTrigger = dialogue;
            diagUI.Interact(dialogue);

            bossController.animatorController.Landing();
        }
    }

    public void StartFight()
    {
        videPlayer.inTrigger = null;
        bossController.isEntranceStateRunning = false;
        sceneController.StartMusic(musicIndex);
        this.gameObject.SetActive(false);
    }
}
