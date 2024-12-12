using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Level2_Cutscenes : MonoBehaviour
{
    public static Level2_Cutscenes instance;

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

        if (Level2_Manager.instance.objectiveList == Level2_Manager.ObjectiveList.Intro)
        {   
            Level2_SoundManager.soundManager2.ambienceSound.Play();
            Level2_SoundManager.soundManager2.studentAmbienceSound.Play();
            Level2_Manager.instance.objectiveList = Level2_Manager.ObjectiveList.GoToLibrary;
            HUDController.instance.objectiveListText.text = "Meet your friends at the library.";
            HUDController.instance.ObjectiveUpdated.SetActive(true);
            Level2_SoundManager.soundManager2.SayWithFreeze(0);
            Level2_Manager.instance.gradeBookObj.SetActive(false);
            Level2_Manager.instance.signObjects[0].SetActive(true);
        }
        else if (Level2_Manager.instance.objectiveList == Level2_Manager.ObjectiveList.TakeTheExam)
        {
            Level2_Manager.instance.TeleportPlayer(-210f, 64.91f, -58f, 90f);
            Level2_Manager.instance.objectiveList = Level2_Manager.ObjectiveList.AfterExamConsulation;
            HUDController.instance.ObjectiveUpdated.SetActive(true);
            HUDController.instance.objectiveListText.text = "Talk to your Professor about your grades.";
            Level2_Manager.instance.signObjects[1].SetActive(true);
            GameManager.instance.gameState = GameManager.GameState.Gameplay;
            SettingsMenu.instance.SetVolume();
        }       

        Level2_Manager.instance.SaveGame();
    }

    public virtual void PlayFirstCutscene()
    {
        StartCoroutine(FirstCutscene());
    }

    public virtual void PlaySecondCutsence()
    {
        StartCoroutine(SecondCutscene());
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
}

