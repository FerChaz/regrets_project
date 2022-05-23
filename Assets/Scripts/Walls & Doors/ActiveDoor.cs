using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveDoor : MonoBehaviour
{
    private Animator doorAnimator;

    private void Awake()
    {
        doorAnimator = GetComponent<Animator>();
        doorAnimator.SetBool("Open", true);
    }
}
