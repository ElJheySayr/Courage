using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2_InteractItem : MonoBehaviour
{
    public virtual void GradeBook()
    {
        Level2_Manager.instance.gradeBook = true;
        Level2_Manager.instance.objectiveList = Level2_Manager.ObjectiveList.SecondFacultyConsultation;
        HUDController.instance.ObjectiveUpdated.SetActive(true);
        HUDController.instance.objectiveListText.text = "Go back to faculty room, and hand the grade book to your Professor.";
        Level2_SoundManager.soundManager2.Say(34);
        Level2_SoundManager.soundManager2.PlaySFX(2);
        Level2_Manager.instance.signObjects[1].SetActive(true);
        Level2_Manager.instance.gradeBookObj.SetActive(false);
    }
}
