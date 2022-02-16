using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiveKeyCanvas : MonoBehaviour
{
    public PlayerInventoryController playerInventory;
    public int keyIdentifier = 0;

    public PlayerController player;
    public LifeController playerLife;

    public GameObject canvas;
    public GameObject panelTalk;
    public GameObject panelOptions;
    public GameObject panelExtraLife;
    public GameObject panelHasntKey;

    private BoxCollider gaveKeyCollider;

    public ObjectStatus extraLife;

    private void Awake()
    {
        playerInventory = FindObjectOfType<PlayerInventoryController>();
        player = FindObjectOfType<PlayerController>();
        playerLife = FindObjectOfType<LifeController>();
        gaveKeyCollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        if (extraLife.eventAlreadyHappened)
        {
            gaveKeyCollider.enabled = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canvas.SetActive(false);
        panelOptions.SetActive(false);
        panelTalk.SetActive(true);
    }

    public void ResponseYes()
    {
        if (playerInventory.HasKey(keyIdentifier))
        {
            extraLife.eventAlreadyHappened = true;
            panelOptions.SetActive(false);
            panelExtraLife.SetActive(true);
            playerLife.AddMaxLife(1);
            gaveKeyCollider.enabled = false;
        }
        else
        {
            panelOptions.SetActive(false);
            panelHasntKey.SetActive(true);
        }
    }


    //-- ENABLE/DISABLE PLAYER MOVEMENT

    public void EnableDisablePlayerMovement()
    {
        player.ChangeCanDoAnyMovement();
    }
}
