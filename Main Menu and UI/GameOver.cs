using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using static Level1_Manager;

public class GameOver : MonoBehaviour
{
    public static GameOver instance;
    public VideoPlayer videoPlayer;
    private Animator fadeAnimator;

    protected virtual void Awake()
    {
        instance = this;  

        fadeAnimator = GameObject.Find("Player UI").GetComponent<Animator>();
        videoPlayer = GameObject.Find("GameOver Video Player").GetComponent<VideoPlayer>();
    }

    protected virtual void Start()
    {
        videoPlayer.loopPointReached += DoSomethingWhenVideoFinish;
    }

    public virtual void DoSomethingWhenVideoFinish(VideoPlayer vp)
    {
        HUDController.instance.GameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public virtual void PlayGameOver()
    {
        if(GameManager.instance.levelState == GameManager.LevelState.Stage3)
        {
            Level3_Manager.instance.timerText.gameObject.SetActive(false);
        }
        else if(GameManager.instance.levelState == GameManager.LevelState.Stage4)
        {
            Level4_Manager.instance.timerText.gameObject.SetActive(false);
        }
        else if(GameManager.instance.levelState == GameManager.LevelState.Stage5)
        {
            Level5_Manager.instance.cautionObj.gameObject.SetActive(false);
            Level5_Manager.instance.vignetteObj.gameObject.SetActive(false);
        }

        HUDController.instance.dialogueText.gameObject.SetActive(false);
        HUDController.instance.interactionText.gameObject.SetActive(false);
        HUDController.instance.ObjectiveUpdated.gameObject.SetActive(false);
        HUDController.instance.SavedGameImage.gameObject.SetActive(false);
        
        StartCoroutine(GameOverCutscene());
    }

    public virtual IEnumerator GameOverCutscene()
    {
        GameManager.instance.gameState = GameManager.GameState.GameOver;
        SettingsMenu.instance.SetVolume();
        Time.timeScale = 0.01f;
        videoPlayer.Play();
        yield return new WaitForEndOfFrame();
    }
}
