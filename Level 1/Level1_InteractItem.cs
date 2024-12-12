using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Level1_InteractItem : MonoBehaviour
{
    public virtual void firstTrophy()
    {
        StartCoroutine(Trophy1Function());
    }

    public virtual void secondTrophy()
    {
        StartCoroutine(Trophy2Function());
    }

    public virtual void thirdTrophy()
    {
        StartCoroutine(Trophy3Function());
    }

    public virtual IEnumerator Trophy1Function()
    {
        if (!Level1_Manager.instance.Trophy1 && Level1_Manager.instance.objectiveList == Level1_Manager.ObjectiveList.FindTheTrophies && !Level1_SoundManager.soundManager1.dialoguePlayer.isPlaying)
        {
            Level1_Manager.instance.Trophy1 = true;
            Level1_SoundManager.soundManager1.Say(11);

            while (Level1_SoundManager.soundManager1.dialoguePlayer.isPlaying)
            {
                yield return new WaitForEndOfFrame();
            }

            Level1_Manager.instance.AiFunction();
        }
        else
        {
            if(!Level1_SoundManager.soundManager1.dialoguePlayer.isPlaying)
            {
                Level1_SoundManager.soundManager1.Say(19);
            }      
        }

        yield return new WaitForEndOfFrame();
    }

    public virtual IEnumerator Trophy2Function()
    {
        if (!Level1_Manager.instance.Trophy2 && Level1_Manager.instance.objectiveList == Level1_Manager.ObjectiveList.FindTheTrophies && !Level1_SoundManager.soundManager1.dialoguePlayer.isPlaying)
        {
            Level1_Manager.instance.Trophy2 = true;
            Level1_SoundManager.soundManager1.Say(12);

            while (Level1_SoundManager.soundManager1.dialoguePlayer.isPlaying)
            {
                yield return new WaitForEndOfFrame();
            }

            Level1_Manager.instance.AiFunction();
        }
        else
        {
            if(!Level1_SoundManager.soundManager1.dialoguePlayer.isPlaying)
            {
                Level1_SoundManager.soundManager1.Say(19);
            }      
        }

        yield return new WaitForEndOfFrame();
    }

    public virtual IEnumerator Trophy3Function()
    {
        if (!Level1_Manager.instance.Trophy3 && Level1_Manager.instance.objectiveList == Level1_Manager.ObjectiveList.FindTheTrophies && !Level1_SoundManager.soundManager1.dialoguePlayer.isPlaying)
        {
            Level1_Manager.instance.Trophy3 = true;
            Level1_SoundManager.soundManager1.Say(13);

            while (Level1_SoundManager.soundManager1.dialoguePlayer.isPlaying)
            {
                yield return new WaitForEndOfFrame();
            }

            Level1_Manager.instance.AiFunction();
        }
        else
        {
            if(!Level1_SoundManager.soundManager1.dialoguePlayer.isPlaying)
            {
                Level1_SoundManager.soundManager1.Say(19);
            }      
        }

        yield return new WaitForEndOfFrame();
    }

    public virtual void WrongAchievement()
    {
        if(!Level1_SoundManager.soundManager1.dialoguePlayer.isPlaying && Level1_Manager.instance.objectiveList == Level1_Manager.ObjectiveList.FindTheTrophies)
        {
            Level1_SoundManager.soundManager1.Say(20);
        }
        else
        {
            if(!Level1_SoundManager.soundManager1.dialoguePlayer.isPlaying)
            {
                Level1_SoundManager.soundManager1.Say(19);
            }
        }
    }
}
