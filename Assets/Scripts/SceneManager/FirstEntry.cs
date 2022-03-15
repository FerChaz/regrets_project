using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstEntry : MonoBehaviour
{
    #region Variables

    public GameObject transitionCanvas;
    public Animator canvasAnimator;

    private WaitForSeconds wait = new WaitForSeconds(.5f);

    #endregion

    #region Awake & Start

    private void Awake()
    {
        transitionCanvas = GameObject.Find("TransitionCanvas");
        canvasAnimator = transitionCanvas.GetComponentInChildren<Animator>();
    }

    #endregion

    #region Init Canvas Transition

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(WaitForFade());
        }
    }

    #endregion

    #region CanvasTransition

    private void CanvasTransition()
    {
        // De negro a transparente

        canvasAnimator.SetBool("ToBlack", false);
    }

    private IEnumerator WaitForFade()
    {
        yield return wait;

        CanvasTransition();

        this.gameObject.SetActive(false);
    }

    #endregion

}
