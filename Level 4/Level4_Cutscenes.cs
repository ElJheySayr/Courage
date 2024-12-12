using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Level4_Cutscenes : MonoBehaviour
{
    public static Level4_Cutscenes instance;

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

        if (Level4_Manager.instance.objectiveList == Level4_Manager.ObjectiveList.Intro)
        {
            Level4_SoundManager.soundManager4.ambienceSound.Play();
            Level4_Manager.instance.objectiveList = Level4_Manager.ObjectiveList.InteractWithAuntie;
            HUDController.instance.objectiveListText.text = "Talk to your Auntie at the Dining Hall.";
            HUDController.instance.ObjectiveUpdated.SetActive(true);
            Level4_Manager.instance.cloneObj.SetActive(false);
            Level4_Manager.instance.signObjects[0].SetActive(true);

            GameManager.instance.gameState = GameManager.GameState.Gameplay;
            SettingsMenu.instance.SetVolume();
        }
        else if (Level4_Manager.instance.objectiveList == Level4_Manager.ObjectiveList.GoToBathroom)
        {
            Level4_Manager.instance.objectiveList = Level4_Manager.ObjectiveList.OldParentRoom;
            HUDController.instance.objectiveListText.text = "Find the Certificates at your Parent Old Room.";
            HUDController.instance.ObjectiveUpdated.SetActive(true);
            Level4_Manager.instance.cloneObj.SetActive(true);
            Level4_SoundManager.soundManager4.PlayMultipleDialogueWithFreeze(12,13);
            Level4_Manager.instance.JoshuaSize();
            Level4_Manager.instance.timerText.gameObject.SetActive(true);
            Level4_SoundManager.soundManager4.anxietySound.Play();
        }
        else if (Level4_Manager.instance.objectiveList == Level4_Manager.ObjectiveList.End)
        {
            HUDController.instance.NewLevel();
        }
        
        Level4_Manager.instance.SaveGame();
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
    
    public virtual void PlayThirdCutscene()
    {
        StartCoroutine(ThirdCutscene());
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
