using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Level2_FriendsTrigger : MonoBehaviour
{
    public GameObject friendsCamera;

    private Transform playerTransform;
    private bool secondPos;

    private Vector3 friend1SecondPos;
    private Vector3 friend2SecondPos;
    private Quaternion friend1SecondRot;
    private quaternion friend2SecondRot;

    protected virtual void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        friend1SecondPos = new Vector3(97.55f, -0.2f, -66.43f);
        friend1SecondRot = Quaternion.Euler(0f, -90f, 0f);
        friend2SecondPos = new Vector3(82.64f, -0.2f, -66.43f);
        friend2SecondRot = Quaternion.Euler(0f, 90f, 0f);
    }

    protected virtual void Update()
    {
        if(Vector3.Distance(transform.position, playerTransform.position) < 25f && Level2_Manager.instance.objectiveList == Level2_Manager.ObjectiveList.GoToLibrary && !Level2_Manager.instance.friendsFirstInteraction)
        {
            StartCoroutine(FirstInteraction());
        }  
        else if (Level2_Manager.instance.objectiveList == Level2_Manager.ObjectiveList.AfterExamConsulation && !secondPos)
        {
            secondPos = true;
            Level2_Manager.instance.friend1Obj.transform.position = friend1SecondPos;
            Level2_Manager.instance.friend2Obj.transform.position = friend2SecondPos;
            Level2_Manager.instance.friend1Obj.transform.rotation = friend1SecondRot;
            Level2_Manager.instance.friend2Obj.transform.rotation = friend2SecondRot;         
        }   
        else if (Vector3.Distance(transform.position, playerTransform.position) < 25f && Level2_Manager.instance.objectiveList == Level2_Manager.ObjectiveList.GoToCafeteria && !Level2_Manager.instance.friendsSecondInteraction)
        {
            StartCoroutine(SecondInteraction());
        }  
    }

    public virtual IEnumerator FirstInteraction()
    {
        Level2_Manager.instance.friendsFirstInteraction = true;
        friendsCamera.SetActive(true);
        Level2_SoundManager.soundManager2.PlayMultipleDialogueWithFreeze(1, 5);
        Level2_Manager.instance.friend1Obj.GetComponent<Level2_NPC>().npcState = Level2_NPC.NpcState.SittingTalking;
        Level2_Manager.instance.signObjects[0].SetActive(false);

        while (Level2_SoundManager.soundManager2.dialoguePlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        friendsCamera.SetActive(false);
        Level2_Manager.instance.friend1Obj.gameObject.GetComponent<Level2_NPC>().npcState = Level2_NPC.NpcState.SittingIdle;
        Level2_Manager.instance.objectiveList = Level2_Manager.ObjectiveList.TakeTheExam;
        HUDController.instance.ObjectiveUpdated.SetActive(true);
        HUDController.instance.objectiveListText.text = "Go to Classroom 1 and take the exam.";
    }

    public virtual IEnumerator SecondInteraction()
    {
        Level2_Manager.instance.friendsSecondInteraction = true;
        friendsCamera.SetActive(true);
        Level2_SoundManager.soundManager2.PlayMultipleDialogueWithFreeze(12, 20);
        Level2_Manager.instance.friend1Obj.GetComponent<Level2_NPC>().npcState = Level2_NPC.NpcState.SittingTalking;
        Level2_Manager.instance.signObjects[0].SetActive(false);

        while (Level2_SoundManager.soundManager2.dialoguePlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        friendsCamera.SetActive(false);
        Level2_Manager.instance.friend1Obj.gameObject.GetComponent<Level2_NPC>().npcState = Level2_NPC.NpcState.SittingIdle;
        Level2_Manager.instance.objectiveList = Level2_Manager.ObjectiveList.FirstFacultyConsultation;
        HUDController.instance.ObjectiveUpdated.SetActive(true);
        HUDController.instance.objectiveListText.text = "Go to the Faculty, and talk to your professor about your concern.";
        Level2_Manager.instance.bulliesObj.SetActive(true);
        Level2_Manager.instance.signObjects[1].SetActive(true);
    }
}
