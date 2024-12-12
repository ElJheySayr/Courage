using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4_NPC : MonoBehaviour
{
    public static Level4_NPC instance;
    public NpcState npcState;
    private Animator animator;

    public enum NpcState
    {
        Idle,
        SittingIdle,
        SittingTalking,
        Talking,
        Walking,
        Clapping
    }

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
        UpdateAnimations();
    }

    public virtual void UpdateAnimations()
    {
        animator.SetBool("isIdle", npcState == NpcState.Idle);
        animator.SetBool("isSittingIdle", npcState == NpcState.SittingIdle);
        animator.SetBool("isSittingTalking", npcState == NpcState.SittingTalking);
        animator.SetBool("isTalking", npcState == NpcState.Talking);
        animator.SetBool("isWalking", npcState == NpcState.Walking);
        animator.SetBool("isClapping", npcState == NpcState.Clapping);
    }
}
