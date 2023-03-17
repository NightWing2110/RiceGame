using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamblerAnimation : MonoBehaviour
{
    private Animator animator;
    private GamblerCat gamblerCat;
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("Idle");
        gamblerCat = GameObject.FindGameObjectWithTag("Player").GetComponent<GamblerCat>();
    }

    private void Update()
    {
        if (gamblerCat.canMove)
        {
            animator.SetBool("Idle", false);
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Walk", false);
        }
    }
}