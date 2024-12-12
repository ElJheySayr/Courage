using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.AI;

public class Level2_ClassroomTrigger : MonoBehaviour
{
    private Transform bully2NewPos;

    protected virtual void Start()
    {
        bully2NewPos = GameObject.Find("Start").transform;
    }

    public virtual void OnTriggerEnter(Collider Actor)
    {
        if(Actor.CompareTag("Player") && Level2_Manager.instance.objectiveList == Level2_Manager.ObjectiveList.TakeTheExam && gameObject.name == "RoomTrigger 1")
        {
            Level2_Cutscenes.instance.PlaySecondCutsence();
        }
        else if (Actor.CompareTag("Player") && Level2_Manager.instance.objectiveList == Level2_Manager.ObjectiveList.GetGradeBookAtClassroom && gameObject.name == "RoomTrigger 1")
        {
            Level2_SoundManager.soundManager2.Say(33);
            Level2_Manager.instance.gradeBookObj.SetActive(true);
            Level2_Manager.instance.objectiveList = Level2_Manager.ObjectiveList.GetGradeBookAtCafeteria;
            HUDController.instance.objectiveListText.text = "Get your grade book at the Cafeteria.";
            HUDController.instance.ObjectiveUpdated.SetActive(true);
        }
        else if (Actor.CompareTag("Player") && Level2_Manager.instance.objectiveList == Level2_Manager.ObjectiveList.GetGradeBookAtClassroom && gameObject.name == "RoomTrigger 2" && !Level2_Manager.instance.bully2Enable)
        {
            StartCoroutine(Bully2());
        }
    }

    private IEnumerator Bully2()
    {
        Vector3 SecondBully2Pos = bully2NewPos.position;

        Level2_Manager.instance.bully2Enable = true;
        Level2_Manager.instance.bully2Obj.transform.position = SecondBully2Pos;
        Level2_Manager.instance.bully2Obj.GetComponent<NavMeshAgent>().enabled = true;
        Level2_BullyTrigger.instance.bullyCamera.SetActive(true);
        GameManager.instance.gameState = GameManager.GameState.Interaction;
        SettingsMenu.instance.SetVolume();
        yield return new WaitForSeconds(1.5f);
        Level2_BullyTrigger.instance.bullyCamera.SetActive(false);
        GameManager.instance.gameState = GameManager.GameState.Gameplay;
        SettingsMenu.instance.SetVolume();
    }
}
