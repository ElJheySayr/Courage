using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class Level3_Cutscenes : MonoBehaviour
{
    public static Level3_Cutscenes instance;
    public VideoPlayer videoPlayer;
    public VideoClip[] clip;
    public Animator fadeAnimator;
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

        if (Level3_Manager.instance.objectiveList == Level3_Manager.ObjectiveList.Intro)
        {
            StartCoroutine(FirstCutsceneFunction());
        }
        else if(Level3_Manager.instance.objectiveList == Level3_Manager.ObjectiveList.End)
        {
            HUDController.instance.NewLevel();
        }
    }

    public virtual void PlayFirstCutscene()
    {
        StartCoroutine(FirstCutscene());
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

    public virtual void PlaySecondCutscene()
    {
        StartCoroutine(SecondCutscene());
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

    public virtual IEnumerator FirstCutsceneFunction()
    {
        Level3_SoundManager.soundManager3.ambienceSound.Play();
        Level3_SoundManager.soundManager3.PlayMultipleDialogueWithFreeze(9,10);
        Level3_Manager.instance.UpdateObjectiveText();
        
        while(Level3_SoundManager.soundManager3.dialoguePlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        Level3_Manager.instance.objectiveList = Level3_Manager.ObjectiveList.GetItems;
        HUDController.instance.ObjectiveUpdated.SetActive(true);
        Level3_Manager.instance.timerText.gameObject.SetActive(true);
        Level3_Manager.instance.SaveGame();
    }
}
