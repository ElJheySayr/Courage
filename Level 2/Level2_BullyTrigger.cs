using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level2_BullyTrigger : MonoBehaviour
{
    public static Level2_BullyTrigger instance;
    public GameObject bullyCamera;
    private Transform playerTransform;

    protected virtual void Awake()
    {
        instance = this;       
    }

    protected virtual void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) < 25f && Level2_Manager.instance.objectiveList == Level2_Manager.ObjectiveList.FirstFacultyConsultation && !Level2_Manager.instance.bullyInteraction)
        {
            StartCoroutine(FirstInteraction());
        }
    }

    public virtual IEnumerator FirstInteraction()
    {
        Level2_Manager.instance.bullyInteraction = true;
        bullyCamera.SetActive(true);
        gameObject.GetComponent<Level2_Enemy>().npcState = Level2_Enemy.NpcState.Talking;
        Level2_SoundManager.soundManager2.PlayMultipleDialogueWithFreeze(21, 23);

        while (Level2_SoundManager.soundManager2.dialoguePlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        bullyCamera.SetActive(false);
        gameObject.GetComponent<Level2_Enemy>().npcState = Level2_Enemy.NpcState.Idle;
        Level2_Manager.instance.bully1Enable = true;
        gameObject.GetComponent<Level2_Enemy>().ChasePlayer();
    }
}
