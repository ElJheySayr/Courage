using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2_Exit : MonoBehaviour
{
    public virtual void ExitFunction()
    {
        if(Level2_Manager.instance.objectiveList == Level2_Manager.ObjectiveList.End)
        {
            HUDController.instance.NewLevel();
        }
        else
        {
            if(!Level2_SoundManager.soundManager2.dialoguePlayer.isPlaying)
            {
                Level2_SoundManager.soundManager2.Say(35);
            }
        }
    }
}
