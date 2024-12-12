using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level4_Door : MonoBehaviour
{
    private float openAngle;
    private float closeAngle = 0f;
    private float smooth = 0.5f;

    private Quaternion openRotation;
    private Quaternion closeRotation;
    private bool isOpen;
    private bool canUse = true;

    protected virtual void Start()
    {
        if (gameObject.name == "MainDoor" || gameObject.name == "CRDoor")
        {
            openAngle = 90f;
        }
        else
        {
            openAngle = -90f;
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
        if (Level4_Manager.instance.objectiveList == Level4_Manager.ObjectiveList.End) 
        {
            DoorFunction();
        }
        else
        {
            if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying)
            {
                Level4_SoundManager.soundManager4.Say(45);
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
            Level4_SoundManager.soundManager4.PlaySFX(1);
        }
        else
        {
            Level4_SoundManager.soundManager4.PlaySFX(0);
        }
    }
}
