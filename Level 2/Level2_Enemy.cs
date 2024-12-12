using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class Level2_Enemy : MonoBehaviour
{
    public static Level2_Enemy instance;
    public NpcState npcState;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    
    public Transform[] patrolPoints;
    public float patrolSpeed = 10f;
    public float chaseSpeed = 12f;
    public float detectionRange = 50f;
    public float fieldOfViewAngle = 90f;
    public LayerMask playerLayer;
    public float patrolCooldown = 2f;
    public AudioSource footstepSound;
    public AudioSource heySound;

    private Transform player;
    private int currentPatrolIndex = 0;
    private bool isChasing = false;
    private bool playerInSight = false;
    private bool isOnCooldown = false;
    private float cooldownTimer = 0f;

    public enum NpcState
    {
        Idle,
        Walking,
        Running,
        Talking,
        SittingIdle,
        SittingTalking,
        Pointing
    }

    protected virtual void Awake()
    {
        instance = this;  
    }

    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>(); 
        navMeshAgent = GetComponent<NavMeshAgent>();    
          
        navMeshAgent.speed = patrolSpeed;      
    }

    protected virtual void Update()
    {
        UpdateAnimations();

        if (Level2_Manager.instance.objectiveList == Level2_Manager.ObjectiveList.FirstFacultyConsultation || Level2_Manager.instance.objectiveList == Level2_Manager.ObjectiveList.GetGradeBookAtClassroom || Level2_Manager.instance.objectiveList == Level2_Manager.ObjectiveList.GetGradeBookAtCafeteria || Level2_Manager.instance.objectiveList == Level2_Manager.ObjectiveList.SecondFacultyConsultation)
        {
            if (Level2_Manager.instance.bully1Enable && gameObject.name == "Bully 1")
            {
                if (isChasing)
                {
                    ChasePlayer();
                }
                else
                {
                    if (isOnCooldown)
                    {
                        Cooldown();
                    }
                    else
                    {
                        Patrol();
                    }
                }

                DetectPlayer();
            }
            else if (Level2_Manager.instance.bully2Enable && gameObject.name == "Bully 2")
            {
                if (isChasing)
                {
                    ChasePlayer();
                }
                else
                {
                    if (isOnCooldown)
                    {
                        Cooldown();
                    }
                    else
                    {
                        Patrol();
                    }
                }

                DetectPlayer();
            }      
        }  
    }

    private void Patrol()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.2f)
        {
            StartCooldown();
        }
        else
        {
            npcState = NpcState.Walking;
            heySound.enabled = false;
            footstepSound.enabled = true;
            footstepSound.pitch = 1f;
            navMeshAgent.speed = patrolSpeed;
            navMeshAgent.destination = patrolPoints[currentPatrolIndex].position;
        }
    }

    private void StartCooldown()
    {
        isOnCooldown = true;
        cooldownTimer = patrolCooldown;
        navMeshAgent.isStopped = true;
        npcState = NpcState.Idle;
    }

    private void Cooldown()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f)
        {
            isOnCooldown = false;
            navMeshAgent.isStopped = false;
            GoToNextPatrolPoint();
        }
    }

    private void GoToNextPatrolPoint()
    {
        npcState = NpcState.Walking;
        footstepSound.enabled = true;
        footstepSound.pitch = 1f;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        navMeshAgent.destination = patrolPoints[currentPatrolIndex].position;
    }

    private void DetectPlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (directionToPlayer.magnitude <= detectionRange && angleToPlayer <= fieldOfViewAngle)
        {
            RaycastHit hit; 
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionRange, playerLayer))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    isChasing = true;
                    playerInSight = true;
                    isOnCooldown = false;
                    return;
                }
            }
        }

        if (playerInSight && directionToPlayer.magnitude > detectionRange)
        {
            isChasing = false;
            playerInSight = false;
        }
    }

    public virtual void ChasePlayer()
    {
        npcState = NpcState.Running;
        footstepSound.enabled = true;
        footstepSound.pitch = 1.25f;
        navMeshAgent.speed = chaseSpeed;
        navMeshAgent.destination = player.position;
        heySound.enabled = true;

        if(Vector3.Distance(player.transform.position, transform.position) < 5f && GameManager.instance.gameState == GameManager.GameState.Gameplay)
        {
            GameOver.instance.PlayGameOver();
        }
    }

    private void UpdateAnimations()
    {
        animator.SetBool("isIdle", npcState == NpcState.Idle);
        animator.SetBool("isWalking", npcState == NpcState.Walking);
        animator.SetBool("isRunning", npcState == NpcState.Running);
        animator.SetBool("isTalking", npcState == NpcState.Talking);
        animator.SetBool("isSittingIdle", npcState == NpcState.SittingIdle);
        animator.SetBool("isSittingTalking", npcState == NpcState.SittingTalking);
        animator.SetBool("isPointing", npcState == NpcState.Pointing);
    }
}