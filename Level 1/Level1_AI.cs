using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Level1_AI : MonoBehaviour
{
    public static Level1_AI instance;
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    public List<Transform> destinations;
    public int currentDestinationIndex = 0;
    public bool isCooldown = false;
    public float cooldownDuration = 2f;

    private AudioSource footstepSound;
    private Transform playerTransform;
    private AiState aiState;

    public enum AiState
    {
        Idle,
        Sitting,
        Walking
    }

    protected virtual void Awake()
    {
        instance = this;       
    }

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        footstepSound = GetComponent<AudioSource>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        if (Level1_Manager.instance.objectiveList == Level1_Manager.ObjectiveList.FindTheTrophies && GameManager.instance.gameState != GameManager.GameState.GameOver)
        {
            if (!navMeshAgent.enabled)
            {
                navMeshAgent.enabled = true;
                StartCoroutine(NextDestinationCooldown());
            }
            Walk();
        }
        else
        {
            navMeshAgent.enabled = false;
            footstepSound.enabled = false;
            StopAllCoroutines();
            aiState = AiState.Sitting;          
        }

        animator.SetBool("isSitting", aiState == AiState.Sitting);
        animator.SetBool("isIdle", aiState == AiState.Idle);
        animator.SetBool("isWalking", aiState == AiState.Walking);
    }

    public virtual void Walk()
    {     
        DetectPlayer();

        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.2f && !isCooldown)
        {
            StartCoroutine(NextDestinationCooldown());
        }

        SmoothRotate(navMeshAgent.destination);
    }

    public virtual void SmoothRotate(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * navMeshAgent.angularSpeed);
    }

    public virtual void SetNextDestination()
    {
        navMeshAgent.SetDestination(destinations[currentDestinationIndex].position);
        currentDestinationIndex = (currentDestinationIndex + 1) % destinations.Count;
        aiState = AiState.Walking;
        footstepSound.enabled = true;
    }

    public virtual void DetectPlayer()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) < 5f && Level1_Manager.instance.objectiveList == Level1_Manager.ObjectiveList.FindTheTrophies && GameManager.instance.gameState == GameManager.GameState.Gameplay)
        {
            GameOver.instance.PlayGameOver();
        }
    }

    public virtual IEnumerator NextDestinationCooldown()
    {
        footstepSound.enabled = false;
        isCooldown = true;
        aiState = AiState.Idle;
        yield return new WaitForSeconds(cooldownDuration);
        isCooldown = false;
        SetNextDestination();
    }
}
