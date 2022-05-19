using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parca : MonoBehaviour
{
    public PlayerController target;
    public float heighToLook;
    private Vector3 rotateVector;


    private void Awake()
    {
        target = FindObjectOfType<PlayerController>();
        heighToLook = this.transform.position.y;
    }

    private void Update()
    {
        rotateVector.Set(target.transform.position.x, heighToLook, 0.0f);
        this.transform.LookAt(rotateVector);

    }
}
