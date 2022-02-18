using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstParca : MonoBehaviour
{
    public ParticleSystem effect;
    public GameObject model;


    public void Disapear()
    {
        effect.Play();

        StartCoroutine(FinishEffect());
    }

    IEnumerator FinishEffect()
    {
        yield return new WaitForSeconds(0.5f);
        model.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        effect.Stop();
    }

}
