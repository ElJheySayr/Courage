using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Level3_NPC : MonoBehaviour
{
    public static Level3_NPC instance;
    public NpcState npcState;
    public SaleLadyState saleLadyState;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private Transform playerTransform;
    private BoxCollider boxCollider;

    public bool movingNpc;
    public bool salesLadyNpc;
    public float playerDistance;
    public GameObject cameraObj;
    private bool entertained;
    private bool inPlace = true;
    private bool talking;
    public Transform npcPatrolPoint;
    private Vector3 startPos;
    
    private AudioSource footstepSound;

    public enum NpcState
    {
        Idle,
        Walking,
        Talking,
    }

    public enum SaleLadyState
    {
        VideoGames,
        Stationary,
        MenClothes,
        WomenClothes
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
        boxCollider = GetComponent<BoxCollider>();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        footstepSound.enabled = false;

        startPos = transform.position;
    }
    
    protected virtual void Update()
    {
        animator.SetBool("isIdle", npcState == NpcState.Idle);
        animator.SetBool("isWalking", npcState == NpcState.Walking);
        animator.SetBool("isTalking", npcState == NpcState.Talking);

        if (movingNpc)
        {
            if(!Level3_Manager.instance.npcArrived && Level3_Manager.instance.npcOnTheGo)
            {
                WalkToPoint();
            }
            else if(Level3_Manager.instance.npcArrived)
            {
                navMeshAgent.enabled = false;
                npcState = NpcState.Idle;
                navMeshAgent.enabled = false;
                boxCollider.enabled = false;
            }     
        }
        else if(salesLadyNpc)
        {
            WalkTowardsPlayerFunction();
        }
    }

    public virtual void WalkToPoint()
    {
        if(Vector3.Distance(transform.position , npcPatrolPoint.position) >= 2.5f)
        {
            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(npcPatrolPoint.position);
            npcState = NpcState.Walking;
            footstepSound.enabled = true;
            boxCollider.enabled = false;

            if(Vector3.Distance(transform.position , npcPatrolPoint.position) <= 3f)
            {
                Level3_Manager.instance.npcArrived = true;
            }       
        }  
    }

    public virtual void WalkTowardsPlayerFunction()
    {
        if(Vector3.Distance(transform.position, playerTransform.position) <= playerDistance && !entertained)
        {
            if(Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
            {
                navMeshAgent.enabled = false;

                if(!talking)
                {
                    npcState = NpcState.Idle;
                }            

                footstepSound.enabled = false;
            }
            else
            {
                navMeshAgent.enabled = true;
                npcState = NpcState.Walking;
                navMeshAgent.SetDestination(playerTransform.position);
                footstepSound.enabled = true;
                inPlace = false;      

                if(Vector3.Distance(transform.position, playerTransform.position) <= 12.5f)
                {
                    StartCoroutine(WalkTowardsPlayer());
                }
            }             
        }
        else
        {
            if(entertained || !inPlace)
            {
                if(!Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
                {
                    if(Vector3.Distance(transform.position, startPos) >= 0.2f)
                    {
                        navMeshAgent.enabled = true;
                        navMeshAgent.SetDestination(startPos);
                        transform.LookAt(startPos);
                        npcState = NpcState.Walking; 
                        footstepSound.enabled = true;
                    }
                    else
                    {
                        npcState = NpcState.Idle;
                        inPlace = true;
                        footstepSound.enabled = false;
                        navMeshAgent.enabled = false;
                    }  
                }       
            }
        }   
    }

    public virtual IEnumerator WalkTowardsPlayer()
    {
        talking = true;
        npcState = NpcState.Talking;
        footstepSound.enabled = false;
        navMeshAgent.enabled = false;

        if(!Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
        {
            if(saleLadyState == SaleLadyState.VideoGames)
            {
                Level3_SoundManager.soundManager3.SayWithFreeze(17);
            }
            else if(saleLadyState == SaleLadyState.Stationary)
            {
                Level3_SoundManager.soundManager3.SayWithFreeze(15);
            }
            else if(saleLadyState == SaleLadyState.MenClothes)
            {
                Level3_SoundManager.soundManager3.SayWithFreeze(16);
            }
            else if(saleLadyState == SaleLadyState.WomenClothes)
            {
                Level3_SoundManager.soundManager3.SayWithFreeze(18);
            }        
        }

        cameraObj.SetActive(true);   

        while(Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
        

        talking = false;
        navMeshAgent.enabled = true;  
        entertained = true;
        cameraObj.SetActive(false); 
    }
}
