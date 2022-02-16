using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCanvas : MonoBehaviour
{
    public GameObject canvasToDisable;

    public void UnShowCanvas()
    {
        canvasToDisable.SetActive(false);
    }

    public void ShowCanvas()
    {
        canvasToDisable.SetActive(true);
    }

}
