using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatedBoss : MonoBehaviour
{
    public GameObject canvasDefeated;

    private void OnEnable()
    {
        canvasDefeated.SetActive(true);
    }
}
