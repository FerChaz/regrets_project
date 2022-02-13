using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimatorController : MonoBehaviour
{
    private Animator animator;

    public GameObject originalModel;
    public GameObject rollModel;

    private const string JUMP = "Jump";
    private const string ATTACK = "Attack";
    private const string RUN = "Run";
    private const string VELOCITY = "Velocity";
    private const string DEFEATED = "Defeat";
    private const string ROAR = "Roar";


    public bool endAttackAnimation;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        endAttackAnimation = false;
    }

    public void Jump()
    {
        animator.SetTrigger(JUMP);
    }

    public void FinishJump()
    {
        //animator.SetBool(JUMP, false);
    }

    public void Attack()
    {
        animator.SetTrigger(ATTACK);
    }

    public void Run()
    {
        animator.SetBool(RUN, true);
    }

    public void Idle()
    {
        animator.SetBool(RUN, false);
    }

    public void Defeated()
    {
        animator.SetTrigger(DEFEATED);
    }

    public void Roar()
    {
        animator.SetTrigger(ROAR);
    }

    private void ChangeVel()
    {
        animator.SetFloat(VELOCITY, 0.5f);
    }

    public void EndAnimation()
    {
        endAttackAnimation = true;
    }

    public void ChangeModel()
    {
        if (originalModel.activeInHierarchy)
        {
            originalModel.SetActive(false);
            rollModel.SetActive(true);
        }
        else
        {
            originalModel.SetActive(true);
            rollModel.SetActive(false);
        }
    }
}
