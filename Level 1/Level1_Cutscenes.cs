using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using static Level1_Manager;
using TMPro;
using UnityEngine.UI;

public class Level1_Cutscenes : MonoBehaviour
{
    public static Level1_Cutscenes instance;
    
    private VideoPlayer videoPlayer;
    public VideoClip[] clip;
    private Animator fadeAnimator;
    private GameObject skipButton;

    protected virtual void Awake()
    {
        instance = this;     

        fadeAnimator = GameObject.Find("Player UI").GetComponent<Animator>();
        videoPlayer = GameObject.Find("Cutscene Video Player").GetComponent<VideoPlayer>();
        skipButton = GameObject.Find("Skip Button");

        skipButton.SetActive(false);
    }

    protected virtual void Start()
    {
        videoPlayer.loopPointReached += DoSomethingWhenVideoFinish;    
    }

    public virtual void DoSomethingWhenVideoFinish(VideoPlayer vp)
    {
        Time.timeScale = 1f;
        videoPlayer.Stop();
        skipButton.SetActive(false);

        fadeAnimator.SetTrigger("isFadeOut");
        fadeAnimator.SetTrigger("isNormal");

        if (Level1_Manager.instance.objectiveList == Level1_Manager.ObjectiveList.Start)
        { 
            
            Level1_SoundManager.soundManager1.PlayMultipleSFXWithFreeze(2,3);
            Level1_SoundManager.soundManager1.ambienceSound.Play();

            StartCoroutine(AfterFirstCutscene());
        }
        else if (Level1_Manager.instance.objectiveList == Level1_Manager.ObjectiveList.FirstInteractionWithFamily)
        {    
            Level1_SoundManager.soundManager1.Say(10); 
            Level1_Manager.instance.objectiveList = Level1_Manager.ObjectiveList.FindTheTrophies;
            HUDController.instance.ObjectiveUpdated.SetActive(true);
            Level1_Manager.instance.trophiesObjects[0].SetActive(true); Level1_Manager.instance.trophiesObjects[1].SetActive(true); Level1_Manager.instance.trophiesObjects[2].SetActive(true);
            Level1_Manager.instance.AiFunction();     

            GameManager.instance.gameState = GameManager.GameState.Gameplay;
            SettingsMenu.instance.SetVolume();
            Level1_Manager.instance.SaveGame();
        }
        else if (Level1_Manager.instance.objectiveList == Level1_Manager.ObjectiveList.FindTheTrophies)
        {  
            Level1_SoundManager.soundManager1.Say(14);         
            Level1_Manager.instance.objectiveList = Level1_Manager.ObjectiveList.LastInteractionWithFamily;
            HUDController.instance.ObjectiveUpdated.SetActive(true);
            Level1_Manager.instance.signObjects[0].SetActive(true);

            GameManager.instance.gameState = GameManager.GameState.Gameplay;
            SettingsMenu.instance.SetVolume();
            Level1_Manager.instance.SaveGame();
        }
    }

    public virtual void PlayFirstCutscene()
    {
        StartCoroutine(FirstCutscene());
    }

    public virtual void PlaySecondCutsence()
    {       
        StartCoroutine(SecondCutscene());
    }

    public virtual void PlayThirdCutsence()
    {
        StartCoroutine(ThirdCutscene());
    }

    public virtual IEnumerator FirstCutscene()
    {
        GameManager.instance.gameState = GameManager.GameState.Cutscene;
        SettingsMenu.instance.SetVolume();
        HUDController.instance.ObjectiveUpdated.SetActive(false);
        Time.timeScale = 0.01f;
        videoPlayer.clip = clip[0];
        videoPlayer.Play();
        skipButton.SetActive(true);
        yield return new WaitForEndOfFrame();
    }

    public virtual IEnumerator AfterFirstCutscene()
    {
        GameManager.instance.gameState = GameManager.GameState.Interaction;

        while(Level1_SoundManager.soundManager1.dialoguePlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        Level1_Manager.instance.trophiesObjects[0].SetActive(false); Level1_Manager.instance.trophiesObjects[1].SetActive(false); Level1_Manager.instance.trophiesObjects[2].SetActive(false);
        Level1_Manager.instance.objectiveList = ObjectiveList.FirstInteractionWithFamily;
        HUDController.instance.objectiveListText.text = "Go to the dining hall and eat breakfast with your family.";
        HUDController.instance.ObjectiveUpdated.SetActive(true);
        Level1_SoundManager.soundManager1.PlayMultipleDialogueWithFreeze(0,1);

        while(Level1_SoundManager.soundManager1.dialoguePlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        Level1_SoundManager.soundManager1.momSFX.Play();

        GameManager.instance.gameState = GameManager.GameState.Interaction;
        PauseMenu.instance.clickSound.Play(); 
        Level1_Manager.instance.instructionUI.SetActive(true);
        Level1_Manager.instance.signObjects[0].SetActive(true);
        Level1_Manager.instance.SaveGame();
    }

    public virtual IEnumerator SecondCutscene() 
    {
        GameManager.instance.gameState = GameManager.GameState.Cutscene;
        SettingsMenu.instance.SetVolume();
        HUDController.instance.ObjectiveUpdated.SetActive(false);
        Time.timeScale = 0.01f;
        videoPlayer.clip = clip[1];
        videoPlayer.Play();
        skipButton.SetActive(true);
        yield return new WaitForEndOfFrame();
    }

    public virtual IEnumerator ThirdCutscene()
    {
        GameManager.instance.gameState = GameManager.GameState.Cutscene;
        SettingsMenu.instance.SetVolume();
        HUDController.instance.ObjectiveUpdated.SetActive(false);
        Time.timeScale = 0.01f;
        videoPlayer.clip = clip[2];
        videoPlayer.Play();
        skipButton.SetActive(true);
        yield return new WaitForEndOfFrame();
    }
}
