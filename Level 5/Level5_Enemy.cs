using UnityEngine;
using UnityEngine.AI;

public class Level5_Enemy : MonoBehaviour
{
    public static Level5_Enemy instance;
    public CloneState cloneState;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private Transform playerTransform;
    private bool cautionPlayed;
    public float walkSpeed = 12f;
    private AudioSource footstepSound;
    public AudioSource cautionSound;

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

        if(Level5_Manager.instance.enemyEnabled)
        {
            if(Vector3.Distance(transform.position, playerTransform.position) >= 25f && !Level5_Manager.instance.playerIsSafe && GameManager.instance.gameState == GameManager.GameState.Gameplay)
            {
                WalkTowardsPlayer();
            }
            else
            {
                IdleMode();
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
        Level5_Manager.instance.cautionObj.SetActive(true);

        if(!cautionSound.isPlaying && !cautionPlayed)
        {
            cautionSound.Play();
            cautionPlayed = true;
        }
        
        if(Vector3.Distance(transform.position, playerTransform.position) <= 27.5f && GameManager.instance.gameState == GameManager.GameState.Gameplay)
        {
            GameOver.instance.PlayGameOver();
        }
    }

    public virtual void IdleMode()
    {
        navMeshAgent.enabled = false;
        cloneState = CloneState.Idle;
        footstepSound.enabled = false;    
        cautionPlayed = false;
        Level5_Manager.instance.cautionObj.SetActive(false);
    }

    private void UpdateAnimations()
    {
        animator.SetBool("Idle", cloneState == CloneState.Idle);
        animator.SetBool("Walking", cloneState == CloneState.Walking);
        animator.SetBool("Running", cloneState == CloneState.Running);
        animator.SetBool("Jumping", cloneState == CloneState.Jumping);
    }
}
