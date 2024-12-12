using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1_FamilyTrigger : MonoBehaviour
{
    private Transform playerTransform;

    public GameObject familyCamera;

    protected virtual void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) < 25f)
        {
            if (Level1_Manager.instance.objectiveList == Level1_Manager.ObjectiveList.FirstInteractionWithFamily && !Level1_Manager.instance.firstInteraction)
            {              
                StartCoroutine(FirstInteraction());
            }
            else if (Level1_Manager.instance.objectiveList == Level1_Manager.ObjectiveList.LastInteractionWithFamily && !Level1_Manager.instance.lastInteraction)
            {
                StartCoroutine(SecondInteraction());
            }
        }
    }

    public virtual IEnumerator FirstInteraction()
    {
        Level1_Manager.instance.firstInteraction = true;
        Level1_SoundManager.soundManager1.PlayMultipleDialogueWithFreeze(2, 9);
        Level1_Maria.instance.mariaState = Level1_Maria.MariaState.Talking;
        familyCamera.SetActive(true);  
        Level1_Manager.instance.signObjects[0].SetActive(false);

        while (Level1_SoundManager.soundManager1.dialoguePlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        Level1_Maria.instance.mariaState = Level1_Maria.MariaState.Sitting;
        familyCamera.SetActive(false);
        Level1_Cutscenes.instance.PlaySecondCutsence();
    }

    public virtual IEnumerator SecondInteraction()
    {
        Level1_Manager.instance.lastInteraction = true;
        Level1_SoundManager.soundManager1.PlayMultipleDialogueWithFreeze(15, 16);
        Level1_Maria.instance.mariaState = Level1_Maria.MariaState.Talking;
        familyCamera.SetActive(true);
        Level1_Manager.instance.signObjects[0].SetActive(false);

        while (Level1_SoundManager.soundManager1.dialoguePlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        HUDController.instance.objectiveListText.text = "Head to school now and go through the front door.";
        Level1_Maria.instance.mariaState = Level1_Maria.MariaState.Sitting;
        Level1_Manager.instance.objectiveList = Level1_Manager.ObjectiveList.End;
        HUDController.instance.ObjectiveUpdated.SetActive(true);
        familyCamera.SetActive(false);
    }
}
