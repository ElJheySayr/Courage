using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level4_GrandpaTrigger : MonoBehaviour
{
    public Camera grandpaCamera;
    private Transform playerTransform;

    protected virtual void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        if(Vector3.Distance(transform.position, playerTransform.position) <= 20f &&Level4_Manager.instance.objectiveList == Level4_Manager.ObjectiveList.InteractWithGrandfather && !Level4_Manager.instance.interactedWithGrandpa && !Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying)
        {
            StartCoroutine(FirstInteraction());
        }
    }

    private IEnumerator FirstInteraction()
    {
        Level4_Manager.instance.interactedWithGrandpa = true;
        gameObject.GetComponent<Level4_NPC>().npcState = Level4_NPC.NpcState.SittingTalking;
        Level4_SoundManager.soundManager4.PlayMultipleDialogueWithFreeze(25,34);
        grandpaCamera.gameObject.SetActive(true);
        Level4_Manager.instance.signObjects[2].SetActive(false);
        
        while(Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        gameObject.GetComponent<Level4_NPC>().npcState = Level4_NPC.NpcState.SittingIdle;
        grandpaCamera.gameObject.SetActive(false);
        Level4_Manager.instance.objectiveList = Level4_Manager.ObjectiveList.End;
        HUDController.instance.objectiveListText.text = "Leave the house.";
        HUDController.instance.ObjectiveUpdated.SetActive(true);
        Level4_Cutscenes.instance.PlayThirdCutscene();
    }
}
