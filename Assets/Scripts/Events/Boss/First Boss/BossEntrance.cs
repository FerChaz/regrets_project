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
    public VIDE_Assign dialogueIntro;
    public VIDE_Assign dialogueEnd;

    //Reference to our diagUI script for quick access
    public VIDEUIManager1 diagUI;

    public ObjectStatus eventHappened;

    public GameObject canvasDefeated;

    private BoxCollider collider;

    private void Awake()
    {
        diagUI = FindObjectOfType<VIDEUIManager1>();
        sceneController = FindObjectOfType<SceneController>();
        player = FindObjectOfType<PlayerController>();
        videPlayer = player.GetComponentInChildren<VIDEPlayer>();
        collider = GetComponent<BoxCollider>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
            videPlayer.inTrigger = dialogueIntro;
            diagUI.Interact(dialogueIntro);

            bossController.animatorController.Landing();
        }
    }

    public void StartFight()
    {
        videPlayer.inTrigger = null;
        bossController.isEntranceStateRunning = false;
        sceneController.StartMusic(musicIndex);
        collider.enabled = false;
    }

    public void Defeated()
    {
        videPlayer.inTrigger = dialogueEnd;
        diagUI.Interact(dialogueEnd);
    }

    public void EndDialogue()
    {
        videPlayer.inTrigger = null;
        canvasDefeated.SetActive(true);
    }
}
