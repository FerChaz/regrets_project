using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace scripts.parallax.parallax
{
    public class Parallax : MonoBehaviour
    {

        #region Variables

        public Transform mainCamera;
        public float speedCoeficient;

        private Vector3 lastpos;

        #endregion

        #region Start, Awake & Update

        private void Awake()
        {
            mainCamera = FindObjectOfType<MainCamera>().gameObject.transform;
        }

        private void Start()
        {
            lastpos = mainCamera.position;
        }

        private void Update()
        {
            transform.position -= ((lastpos - mainCamera.position) * speedCoeficient);
            lastpos = mainCamera.position;
        }


        #endregion

    }
}

