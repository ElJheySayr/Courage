using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1_SuspenseTrigger : MonoBehaviour
{
    public virtual void OnTriggerEnter(Collider Actor)
    {
        if (gameObject.name != "Suspense Trigger Stop" && Actor.CompareTag("Player") && Level1_Manager.instance.objectiveList == Level1_Manager.ObjectiveList.FindTheTrophies)
        {
            Level1_SoundManager.soundManager1.suspenseSound.Stop();
        }
    }

    public virtual void OnTriggerStay(Collider Actor)
    {
        if (Actor.CompareTag("Player") && Level1_Manager.instance.objectiveList != Level1_Manager.ObjectiveList.FindTheTrophies)
        {
            Level1_SoundManager.soundManager1.suspenseSound.Stop();
        }
    }

    public virtual void OnTriggerExit(Collider Actor)
    {
        if (gameObject.name != "Suspense Trigger Stop" && Actor.CompareTag("Player") && Level1_Manager.instance.objectiveList == Level1_Manager.ObjectiveList.FindTheTrophies)
        {
            Level1_SoundManager.soundManager1.suspenseSound.Play();
        }
    }
}
