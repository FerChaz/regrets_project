using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject canvas;
    public Image canvasImage;

    public ObjectStatus eventHappened;

    public PlayerController playerController;

    public ParticleSystem effect;
    public GameObject model;

    public StartingScene startingSceneController;

    private void Awake()
    {
        if (eventHappened.eventAlreadyHappened)
        {
            this.gameObject.SetActive(false);
        }

        playerController = FindObjectOfType<PlayerController>();
        startingSceneController = FindObjectOfType<StartingScene>();
    }

    private void OnTriggerExit(Collider other)
    {
        UnShow();
    }

    private void OnTriggerEnter(Collider other)
    {
        Show();
    }

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

    public void PlayerMovement(bool enabled)
    {
        playerController.CanDoAnyMovement(enabled);
    }
}
