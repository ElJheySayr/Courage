using System.Collections;
using UnityEngine;

public class Level5_InteractItem : MonoBehaviour
{
    public virtual void Phone()
    {
        if(!Level5_SoundManager.soundManager5.dialoguePlayer.isPlaying && !Level5_Manager.instance.changedMusic && Level5_Manager.instance.objectiveList == Level5_Manager.ObjectiveList.ChangeMusic)
        {
            StartCoroutine(PhoneFunction());
        }
    }

    private IEnumerator PhoneFunction()
    {
        Level5_Manager.instance.changedMusic = true;
        Level5_Manager.instance.fadingWhite.SetActive(true);
        yield return new WaitForSeconds(2);
        Level5_Cutscenes.instance.PlayFirstCutscene();
    }
}
