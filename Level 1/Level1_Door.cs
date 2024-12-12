using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level1_Door : MonoBehaviour
{
    private float openAngle;
    private float closeAngle = 0f;
    public float smooth = 0.5f;

    private Quaternion openRotation;
    private Quaternion closeRotation;
    private bool isOpen;
    private bool canUse = true;

    protected virtual void Start()
    {
        if(gameObject.name == "Joshua's Door")
        {
            openAngle = -90f;
        }
        else
        {
            openAngle = 90f;
        }

        openRotation = Quaternion.Euler(0, openAngle, 0);
        closeRotation = Quaternion.Euler(0, closeAngle, 0);
    }

    public virtual void DoorFunction()
    {
        if (canUse)
        {           
            ToggleDoor();
        }
    }

    public virtual void ExitDoorFunction()
    {
        if (Level1_Manager.instance.objectiveList == Level1_Manager.ObjectiveList.End)
        {
            HUDController.instance.NewLevel();
        }
        else
        {
            if(!Level1_SoundManager.soundManager1.dialoguePlayer.isPlaying)
            {
                Level1_SoundManager.soundManager1.Say(19);
            }
        }
    }

    public virtual void ToggleDoor()
    {
        canUse = false;
        isOpen = !isOpen;
        StartCoroutine(AnimateDoor(isOpen ? openRotation : closeRotation));
    }

    public virtual IEnumerator AnimateDoor(Quaternion targetRotation)
    {
        Quaternion initialRotation = transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < smooth)
        {
            transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, elapsedTime / smooth);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
        PlaySound(isOpen);
        canUse = true;
    }

    public virtual void PlaySound(bool open)
    {
        if (open)
        {
            Level1_SoundManager.soundManager1.PlaySFX(1);
        }
        else
        {
            Level1_SoundManager.soundManager1.PlaySFX(0);
        }
    }
}
