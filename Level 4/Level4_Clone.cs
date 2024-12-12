using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using NUnit.Framework.Constraints;

public class Level4_Clone : MonoBehaviour
{
    public static Level4_Clone instance;
    public CloneState cloneState;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    
    private Transform playerTransform;
    public float walkSpeed = 10f;
    private AudioSource footstepSound;

    public enum CloneState
    {
        Idle,
        Walking,
        Running,
        Jumping,
    }

    protected virtual void Awake()
    {
        instance = this;   
    }

    protected virtual void Start()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        footstepSound = GetComponent<AudioSource>();
        
        navMeshAgent.speed = walkSpeed;
    }

    protected virtual void Update()
    {
        UpdateAnimations();

        if (Level4_Manager.instance.objectiveList == Level4_Manager.ObjectiveList.OldParentRoom || Level4_Manager.instance.objectiveList == Level4_Manager.ObjectiveList.GrandpaOffice)
        {
            if(Vector3.Distance(transform.position, playerTransform.position) <= 15f)
            {
                IdleMode();
            }
            else
            {
                WalkTowardsPlayer();
            }
        }  
    }


    public virtual void WalkTowardsPlayer()
    {
        navMeshAgent.enabled = true;
        cloneState = CloneState.Walking;
        footstepSound.enabled = true;
        navMeshAgent.destination = playerTransform.position;
        transform.LookAt(playerTransform);
    }

    public virtual void IdleMode()
    {
        navMeshAgent.enabled = false;
        cloneState = CloneState.Idle;
        footstepSound.enabled = false;    
    }

    private void UpdateAnimations()
    {
        animator.SetBool("Idle", cloneState == CloneState.Idle);
        animator.SetBool("Walking", cloneState == CloneState.Walking);
        animator.SetBool("Running", cloneState == CloneState.Running);
        animator.SetBool("Jumping", cloneState == CloneState.Jumping);
    }
}
