using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3_Exit : MonoBehaviour
{
    public virtual void ExitFunction()
    {
        if(Level3_Manager.instance.objectiveList == Level3_Manager.ObjectiveList.End)
        {
            Level3_Cutscenes.instance.PlaySecondCutscene();
        }
        else
        {
            if(!Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
            {
                Level3_SoundManager.soundManager3.Say(20);
            }           
        }
    }
}
