using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Level5_Cutscenes : MonoBehaviour
{
    public static Level5_Cutscenes instance;

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
        PauseMenu.instance.LoadMenu();
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
        Level5_Manager.instance.fadingWhite.SetActive(false);
        Level5_Manager.instance.vignetteObj.SetActive(false);
    }
}
