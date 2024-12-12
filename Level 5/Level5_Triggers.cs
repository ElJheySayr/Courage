using System.Collections;
using NUnit.Framework.Interfaces;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Level5_Triggers : MonoBehaviour
{
    private float time;

    private bool playerInside;

    private void OnTriggerStay(Collider Actor)
    {
        if(gameObject.name == "Playground Trigger" && Actor.CompareTag("Player") && Level5_Manager.instance.objectiveList == Level5_Manager.ObjectiveList.Playground)
        {
            time += Time.deltaTime;

            if(time >= 3f && !Level5_Manager.instance.playgroundArrived)
            {
                StartCoroutine(PlaygroundScene());
            }    

            Level5_Manager.instance.playerIsSafe = true;
        }
        else if(gameObject.name == "Picnic Trigger" && Actor.CompareTag("Player"))
        {
            if(Level5_Manager.instance.objectiveList == Level5_Manager.ObjectiveList.Picnic || Level5_Manager.instance.objectiveList == Level5_Manager.ObjectiveList.FindYourFriends)
            {
                time += Time.deltaTime;

                if(time >= 3f)
                {
                    if(!Level5_Manager.instance.picnicArrived)
                    {
                        StartCoroutine(PicnicScene());
                    }    
                } 

                Level5_Manager.instance.playerIsSafe = true;
            }
        }
        else if(gameObject.name == "Food Stalls Trigger" && Actor.CompareTag("Player") && Level5_Manager.instance.objectiveList == Level5_Manager.ObjectiveList.FoodStalls)
        {
            time += Time.deltaTime;

            if(!Level5_Manager.instance.playerAtTheFoodStall)
            {
                Level5_Manager.instance.playerAtTheFoodStall = true;
            }    

            if(time >= 3f && !Level5_Manager.instance.foodStallsArrived)
            {
                StartCoroutine(FoodStallsScene());
            } 

            Level5_Manager.instance.playerIsSafe = true;
        }
        else if(gameObject.name == "Zumba Trigger" && Actor.CompareTag("Player") && Level5_Manager.instance.objectiveList == Level5_Manager.ObjectiveList.Zumba)
        {
            time += Time.deltaTime;

            if(time >= 3f && !Level5_Manager.instance.zumbaArrived)
            {
                StartCoroutine(ZumbaScene());
            } 

            Level5_Manager.instance.playerIsSafe = true;
        }
    }

    private void OnTriggerExit(Collider Actor)
    {
        if(Actor.CompareTag("Player"))
        {
            Level5_Manager.instance.playerIsSafe = false;
            Level5_Manager.instance.playerAtTheFoodStall = false;
            time = 0f;
        }
    }

    private IEnumerator PlaygroundScene()
    {
        Level5_Manager.instance.playerIsSafe = true;
        Level5_Manager.instance.playgroundArrived = true;
        Level5_Manager.instance.vignetteObj.SetActive(true);
        Level5_SoundManager.soundManager5.SayWithFreeze(3);

        while(Level5_SoundManager.soundManager5.dialoguePlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }
        
        Level5_Manager.instance.objectiveList = Level5_Manager.ObjectiveList.Picnic;
        HUDController.instance.objectiveListText.text = "Go to the Picnic Area.";
        time = 0f;
        HUDController.instance.ObjectiveUpdated.SetActive(true);
        gameObject.SetActive(false);  
        Level5_Manager.instance.playerIsSafe = false;
    }

    private IEnumerator PicnicScene()
    {
        Level5_Manager.instance.objectiveList = Level5_Manager.ObjectiveList.FindYourFriends;
        HUDController.instance.objectiveListText.text = "Find your Friends at the picnic area.";
        Level5_Manager.instance.signObjects[0].SetActive(true);
        time = 0f;
        HUDController.instance.ObjectiveUpdated.SetActive(true);
        Level5_Manager.instance.picnicArrived = true;
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator FoodStallsScene()
    {
        time = 0f;
        Level5_Manager.instance.foodStallsArrived = true;
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator ZumbaScene()
    {
        Level5_SoundManager.soundManager5.Say(29);
        Level5_Manager.instance.objectiveList = Level5_Manager.ObjectiveList.ChangeMusic;
        HUDController.instance.objectiveListText.text = "Find the speaker and change the music.";
        HUDController.instance.ObjectiveUpdated.SetActive(true);
        Level5_Manager.instance.zumbaArrived = true;
        yield return new WaitForEndOfFrame();
    }
}
