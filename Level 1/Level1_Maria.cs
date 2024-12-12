using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1_Maria : MonoBehaviour
{
    public static Level1_Maria instance;
    private Animator animator;
    public MariaState mariaState;

    protected virtual void Awake()
    {
        instance = this;
    }

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        animator.SetBool("isSitting", mariaState == MariaState.Sitting);
        animator.SetBool("isTalking", mariaState == MariaState.Talking);
        animator.SetBool("isAngry", mariaState == MariaState.Angry);
    }

    public enum MariaState
    {
        Sitting,
        Talking,
        Angry
    }
}
