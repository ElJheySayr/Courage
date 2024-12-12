using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2_ProfessorTrigger : MonoBehaviour
{
    public GameObject professorCamera;
    private Transform playerTransform;
    private bool firstPosition;
    private Vector3 secondPos;
    private Quaternion secondRot;

    protected virtual void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        firstPosition = true;
        secondPos = new Vector3(-119.27f, 67.26f, -116.97f);
        secondRot = Quaternion.Euler(0f, -180f, 0f);
    }

    protected virtual void Update()
    {
        if(Vector3.Distance(transform.position, playerTransform.position) < 12.5f && Level2_Manager.instance.objectiveList == Level2_Manager.ObjectiveList.AfterExamConsulation && !Level2_Manager.instance.professorFirstInteraction)
        {
            StartCoroutine(FirstInteraction());
        }
        else if (Vector3.Distance(transform.position, playerTransform.position) > 25f && Level2_Manager.instance.objectiveList == Level2_Manager.ObjectiveList.FirstFacultyConsultation && firstPosition)
        {
            firstPosition = false;
            transform.position = secondPos;
            transform.rotation = secondRot;          
        }
        else if (Vector3.Distance(transform.position, playerTransform.position) < 12.5f && Level2_Manager.instance.objectiveList == Level2_Manager.ObjectiveList.FirstFacultyConsultation && !Level2_Manager.instance.professorSecondInteraction)
        {
            StartCoroutine(SecondInteraction());
        }
        else if (Vector3.Distance(transform.position, playerTransform.position) < 12.5f && Level2_Manager.instance.objectiveList == Level2_Manager.ObjectiveList.SecondFacultyConsultation && !Level2_Manager.instance.professorThirdInteraction)
        {
            StartCoroutine(ThirdInteraction());
        }
    }

    public virtual IEnumerator FirstInteraction()
    {
        Level2_Manager.instance.professorFirstInteraction = true;
        professorCamera.SetActive(true);
        gameObject.GetComponent<Level2_NPC>().npcState = Level2_NPC.NpcState.SittingTalking;
        Level2_SoundManager.soundManager2.PlayMultipleDialogueWithFreeze(9,11);
        Level2_Manager.instance.signObjects[1].SetActive(false);

        while (Level2_SoundManager.soundManager2.dialoguePlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        professorCamera.SetActive(false);
        gameObject.GetComponent<Level2_NPC>().npcState = Level2_NPC.NpcState.SittingIdle;
        Level2_Manager.instance.objectiveList = Level2_Manager.ObjectiveList.GoToCafeteria;
        HUDController.instance.ObjectiveUpdated.SetActive(true);
        HUDController.instance.objectiveListText.text = "Meet your Friends at the Cafeteria";
        Level2_Manager.instance.signObjects[0].SetActive(true);
    }

    public virtual IEnumerator SecondInteraction()
    {
        Level2_Manager.instance.professorSecondInteraction = true;
        professorCamera.SetActive(true);
        gameObject.GetComponent<Level2_NPC>().npcState = Level2_NPC.NpcState.SittingTalking;
        Level2_SoundManager.soundManager2.PlayMultipleDialogueWithFreeze(24, 27);
        Level2_Manager.instance.signObjects[1].SetActive(false);

        while (Level2_SoundManager.soundManager2.dialoguePlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        professorCamera.SetActive(false);
        gameObject.GetComponent<Level2_NPC>().npcState = Level2_NPC.NpcState.SittingIdle;
        Level2_Manager.instance.objectiveList = Level2_Manager.ObjectiveList.GetGradeBookAtClassroom;
        HUDController.instance.objectiveListText.text = "Get your grade book at the classroom 1.";
    }

    public virtual IEnumerator ThirdInteraction()
    {
        Level2_Manager.instance.professorThirdInteraction = true;
        professorCamera.SetActive(true);
        gameObject.GetComponent<Level2_NPC>().npcState = Level2_NPC.NpcState.SittingTalking;
        Level2_SoundManager.soundManager2.PlayMultipleDialogueWithFreeze(28, 32);
        Level2_Manager.instance.signObjects[1].SetActive(false);

        while (Level2_SoundManager.soundManager2.dialoguePlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        professorCamera.SetActive(false);
        gameObject.GetComponent<Level2_NPC>().npcState = Level2_NPC.NpcState.SittingIdle; 
        Level2_Manager.instance.objectiveList = Level2_Manager.ObjectiveList.End;
        HUDController.instance.ObjectiveUpdated.SetActive(true);
        HUDController.instance.objectiveListText.text = "Leave the School.";
        Level2_Manager.instance.bulliesObj.SetActive(false);
    }
}
