using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1_LookAt : MonoBehaviour
{
    public static Level1_LookAt instance;
    public Transform target;
    private Transform cameraTransform;

    public float speed;
    private Coroutine LookCoroutine;

    protected virtual void Awake()
    {
        instance = this;  
    }

    protected virtual void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    public virtual void OnTriggerStay(Collider Actor)
    {
        if (Actor.CompareTag("Player") && Level1_Manager.instance.objectiveList == Level1_Manager.ObjectiveList.FindTheTrophies)
        {
            if (gameObject.name == "Trophy1Trigger" && !Level1_Manager.instance.Trophy1LookAt && !Level1_SoundManager.soundManager1.dialoguePlayer.isPlaying)
            {
                Level1_Manager.instance.Trophy1LookAt = true;       
                StartRotating();  
            }
            else if (gameObject.name == "Trophy2Trigger" && !Level1_Manager.instance.Trophy2LookAt && !Level1_SoundManager.soundManager1.dialoguePlayer.isPlaying)
            {
                Level1_Manager.instance.Trophy2LookAt = true;
                StartRotating();
            }
            else if (gameObject.name == "Trophy3Trigger" && !Level1_Manager.instance.Trophy3LookAt && !Level1_SoundManager.soundManager1.dialoguePlayer.isPlaying)
            {
                Level1_Manager.instance.Trophy3LookAt = true;
                StartRotating();
            }    
        }
    }

    public virtual void StartRotating()
    {
        Level1_SoundManager.soundManager1.Say(17);
         
        GameManager.instance.gameState = GameManager.GameState.Interaction;
        SettingsMenu.instance.SetVolume();

        if (LookCoroutine != null)
        {
            StopCoroutine(LookCoroutine);
        }

        LookCoroutine = StartCoroutine(LookAt());
    }

    public virtual IEnumerator LookAt()
    {
        Quaternion lookRotation = Quaternion.LookRotation(target.position - cameraTransform.position);
        float time = 0;

        while (time < 1)
        {
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, lookRotation, time);
            time += Time.deltaTime * speed;
            yield return null;
        }

        GameManager.instance.gameState = GameManager.GameState.Gameplay;
        SettingsMenu.instance.SetVolume();
    }
}
