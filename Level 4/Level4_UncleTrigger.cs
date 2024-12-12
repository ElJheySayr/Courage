using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level4_UncleTrigger : MonoBehaviour
{
    public Camera uncleCamera;
    private Transform playerTransform;

    protected virtual void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        if(Vector3.Distance(transform.position, playerTransform.position) <= 20f &&Level4_Manager.instance.objectiveList == Level4_Manager.ObjectiveList.InteractWithUncle && !Level4_Manager.instance.interactedWithUncle && Level4_Manager.instance.cerificates && !Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying)
        {
            StartCoroutine(FirstInteraction());
        }
    }

    private IEnumerator FirstInteraction()
    {
        Level4_Manager.instance.interactedWithUncle = true;
        gameObject.GetComponent<Level4_NPC>().npcState = Level4_NPC.NpcState.SittingTalking;
        Level4_SoundManager.soundManager4.PlayMultipleDialogueWithFreeze(14,19);
        uncleCamera.gameObject.SetActive(true);
        Level4_Manager.instance.signObjects[1].SetActive(false);
        
        while(Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        gameObject.GetComponent<Level4_NPC>().npcState = Level4_NPC.NpcState.SittingIdle;
        uncleCamera.gameObject.SetActive(false);
        Level4_Manager.instance.objectiveList = Level4_Manager.ObjectiveList.GrandpaOffice;
        HUDController.instance.objectiveListText.text = "Go to Grandfather's Office.";
        HUDController.instance.ObjectiveUpdated.SetActive(true);
        Level4_Manager.instance.timerText.gameObject.SetActive(true);
        Level4_Manager.instance.time = 360f;
        Level4_Manager.instance.cloneObj.SetActive(true);
        Level4_SoundManager.soundManager4.PlayMultipleDialogue(23,24);
    }
}
