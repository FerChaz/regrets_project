using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using scripts.sceneManager.startingScene;

public class TutorialMove : MonoBehaviour
{
    public GameObject canvas;
    public Image canvasImage;

    public ObjectStatus eventHappened;

    public PlayerController playerController;

    public ParticleSystem effect;
    public GameObject model;
    public GameObject dialogue;

    public StartingScene startingSceneController;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        startingSceneController = FindObjectOfType<StartingScene>();
    }

    private void OnTriggerExit(Collider other)
    {
        UnShow();
    }

    public void Show()
    {
        eventHappened.eventAlreadyHappened = true;
        dialogue.SetActive(false);
        canvas.SetActive(true);

        effect.Play();
        StartCoroutine(FinishEffect());
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

    IEnumerator FinishEffect()
    {
        yield return new WaitForSeconds(0.5f);
        model.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        effect.Stop();
        startingSceneController.gameObject.SetActive(false);
    }
}
