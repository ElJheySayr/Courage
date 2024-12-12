using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level4_AuntieTrigger : MonoBehaviour
{
    public Camera auntCamera;
    private Transform playerTransform;

    protected virtual void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        if(Vector3.Distance(transform.position, playerTransform.position) <= 20f &&Level4_Manager.instance.objectiveList == Level4_Manager.ObjectiveList.InteractWithAuntie && !Level4_Manager.instance.interactedWithAuntie && !Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying)
        {
            StartCoroutine(FirstInteraction());
        }
    }

    private IEnumerator FirstInteraction()
    {
        Level4_Manager.instance.interactedWithAuntie = true;
        gameObject.GetComponent<Level4_NPC>().npcState = Level4_NPC.NpcState.SittingTalking;
        Level4_SoundManager.soundManager4.PlayMultipleDialogueWithFreeze(5,11);
        auntCamera.gameObject.SetActive(true);
        Level4_Manager.instance.signObjects[0].SetActive(false);
        
        while(Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        gameObject.GetComponent<Level4_NPC>().npcState = Level4_NPC.NpcState.SittingIdle;
        auntCamera.gameObject.SetActive(false);
        Level4_Manager.instance.objectiveList = Level4_Manager.ObjectiveList.GoToBathroom;
        HUDController.instance.objectiveListText.text = "Go to the bathroom.";
        HUDController.instance.ObjectiveUpdated.SetActive(true);
    }
}
