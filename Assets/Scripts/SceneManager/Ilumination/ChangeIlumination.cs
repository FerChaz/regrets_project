using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using scripts.etiquetas.mainlight;

namespace scripts.sceneManager.ilumination.changeilumination {
    public class ChangeIlumination : MonoBehaviour
    {
        private GameObject player;
        private Light directionalLight;

        public float duration;
        public Color colorToChange;
        private Color initialColor;
        public float intensity;

        private void Awake()
        {
            directionalLight = FindObjectOfType<MainLight>().GetComponent<Light>();
            player = FindObjectOfType<PlayerController>().gameObject;
            initialColor = directionalLight.color;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                directionalLight.color = colorToChange;
                directionalLight.intensity = intensity;
            }
        }

        private IEnumerator TimeToLerp()
        {
            for (int t = 0; t < duration; t++)
            {
                yield return null;
            }
            
        }


    }
}

