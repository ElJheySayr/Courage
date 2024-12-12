using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level3_Triggers : MonoBehaviour
{
    public virtual void OnTriggerStay(Collider Actor)
    {
        if(Actor.CompareTag("Player") && !Level3_Manager.instance.warningTriggered && Level3_Manager.instance.time >= 360f && !Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
        {
            Level3_SoundManager.soundManager3.Say(11);
            Level3_Manager.instance.warningTriggered = true;
        }
    }
}
