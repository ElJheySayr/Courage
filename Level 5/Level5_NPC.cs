using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Level5_NPC : MonoBehaviour
{
    public static Level5_NPC instance;
    public NpcState npcState;
    public bool movingNpc;
    public GameObject cameraObj;
    private Animator animator;

    private NavMeshAgent agent;
    private AudioSource footstepSound;
    private Transform playerTransform;

    public enum NpcState
    {
        Idle,
        SittingIdle,
        SittingTalking,
        Talking,
        Walking,
        DancingOrGroundSit
    }

    protected virtual void Awake()
    {
        instance = this;     
    }

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        footstepSound = GetComponent<AudioSource>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        UpdateAnimations();

        if(movingNpc)
        {
            if(Level5_Manager.instance.objectiveList == Level5_Manager.ObjectiveList.FoodStalls && Level5_Manager.instance.playerIsSafe && Level5_Manager.instance.playerAtTheFoodStall && !Level5_Manager.instance.strangerInteracted)
            {
                agent.enabled = true;
                agent.SetDestination(playerTransform.position);
                npcState = NpcState.Walking;
                footstepSound.enabled = true;

                if(Vector3.Distance(transform.position, playerTransform.position) < 17.5f)
                {
                    StartCoroutine(StrangerInteractionScene());
                }
            }
            else
            {
                if(!Level5_Manager.instance.playerAtTheFoodStall)
                {
                    npcState = NpcState.Idle;
                    footstepSound.enabled = false;
                    agent.enabled = false;
                }   
            }
        }
    }

    public virtual void UpdateAnimations()
    {
        animator.SetBool("isIdle", npcState == NpcState.Idle);
        animator.SetBool("isSittingIdle", npcState == NpcState.SittingIdle);
        animator.SetBool("isSittingTalking", npcState == NpcState.SittingTalking);
        animator.SetBool("isTalking", npcState == NpcState.Talking);
        animator.SetBool("isWalking", npcState == NpcState.Walking);
        animator.SetBool("isDancing", npcState == NpcState.DancingOrGroundSit);
    }

    private IEnumerator StrangerInteractionScene()
    {
        Level5_Manager.instance.playerIsSafe = true;
        Level5_Manager.instance.strangerInteracted = true;
        npcState = NpcState.Idle;
        footstepSound.enabled = false;
        agent.enabled = false;
        cameraObj.SetActive(true);

        Level5_SoundManager.soundManager5.PlayMultipleDialogueWithFreeze(4,8);

        while(Level5_SoundManager.soundManager5.dialoguePlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        npcState = NpcState.Talking;
        Level5_SoundManager.soundManager5.PlayMultipleDialogueWithFreeze(9,17);

        while(Level5_SoundManager.soundManager5.dialoguePlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        npcState = NpcState.Idle;           
        Level5_Manager.instance.objectiveList = Level5_Manager.ObjectiveList.Zumba;
        HUDController.instance.objectiveListText.text = "Go to the Zumba Area.";
        HUDController.instance.ObjectiveUpdated.SetActive(true);
        Level5_Manager.instance.FoodStallsCollider.SetActive(false);  
        Level5_Manager.instance.playerIsSafe = false;
        cameraObj.SetActive(false); 
    }
}
