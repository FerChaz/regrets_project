using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLanding : MonoBehaviour
{
    public BossController bossController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boss"))
        {
            bossController.animatorController.Landing();
        }
    }
}
