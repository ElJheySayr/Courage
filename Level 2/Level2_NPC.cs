using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Level1_AI;
using UnityEngine.AI;

public class Level2_NPC : MonoBehaviour
{
    public static Level2_NPC instance;
    public NpcState npcState;
    private Animator animator;

    public enum NpcState
    {
        Idle,
        Walking,
        Running,
        Talking,
        SittingIdle,
        SittingTalking,
    }

    protected virtual void Awake()
    {
        instance = this;       
    }

    protected void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    protected virtual void Update()
    {
        animator.SetBool("isIdle", npcState == NpcState.Idle);
        animator.SetBool("isWalking", npcState == NpcState.Walking);
        animator.SetBool("isRunning", npcState == NpcState.Running);
        animator.SetBool("isTalking", npcState == NpcState.Talking);
        animator.SetBool("isSittingIdle", npcState == NpcState.SittingIdle);
        animator.SetBool("isSittingTalking", npcState == NpcState.SittingTalking);
    }
}