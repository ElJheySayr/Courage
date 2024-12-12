using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    [Header("GameObjects")]
    public MenuState menuState;
    public GameObject menuPanel;  
    public GameObject settingsPanel;
    public GameObject displaySettingsPanel;  
    public GameObject audioSettingsPanel;  
    public GameObject controlsSettingsPanel;  
    public GameObject guideDisplay, menuDisplay;

    [Header("Sounds")]
    public AudioSource clickSound;
    public AudioSource backSound;
    private bool clickSoundEnable = true;
    private bool backSoundEnable = true;
    private bool InSettings;

    public enum MenuState
    {
        PauseMenu,
        Settings,
        DisplaySettings,
        AudioSettings,
        ControlsSettings
    }

    protected virtual void Awake()
    {
        instance = this;
    }

    protected virtual void Update()
    {   
        // Dev Mode 
        if(Input.GetKeyDown(KeyCode.F3))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!InSettings)
            {
                if(GameManager.instance.gameState == GameManager.GameState.Gameplay)
                {
                    Pause();
                }
                else if(GameManager.instance.gameState == GameManager.GameState.PausedGame)
                {
                    Resume();
                }
            }
            else
            {
                if(menuState == MenuState.Settings)
                {
                    menuState = MenuState.PauseMenu;
                    settingsPanel.SetActive(false);
                    menuPanel.SetActive(true);

                    if(backSoundEnable)
                    {
                        StartCoroutine(delaySoundBackSound());
                    }

                    InSettings = false;

                }
                else if(menuState == MenuState.DisplaySettings)
                {
                    menuState = MenuState.PauseMenu;
                    menuPanel.SetActive(true);
                    settingsPanel.SetActive(false);
                    displaySettingsPanel.SetActive(false);
                    audioSettingsPanel.SetActive(false);
                    controlsSettingsPanel.SetActive(false);

                    if(backSoundEnable)
                    {
                        StartCoroutine(delaySoundBackSound());
                    }

                    InSettings = false;

                }
                else if(menuState == MenuState.AudioSettings)
                {
                    menuState = MenuState.PauseMenu;
                    menuPanel.SetActive(true);
                    settingsPanel.SetActive(false);
                    displaySettingsPanel.SetActive(false);
                    audioSettingsPanel.SetActive(false);
                    controlsSettingsPanel.SetActive(false);

                    if(backSoundEnable)
                    {
                        StartCoroutine(delaySoundBackSound());
                    }

                    InSettings = false;

                }
                else if(menuState == MenuState.ControlsSettings)
                {
                    menuState = MenuState.PauseMenu;
                    menuPanel.SetActive(true);
                    settingsPanel.SetActive(false);
                    displaySettingsPanel.SetActive(false);
                    audioSettingsPanel.SetActive(false);
                    controlsSettingsPanel.SetActive(false);

                    if(backSoundEnable)
                    {
                        StartCoroutine(delaySoundBackSound());
                    }

                    InSettings = false;

                }
            }
        }   

        if (GameManager.instance.gameState == GameManager.GameState.Gameplay && Input.GetKey(KeyCode.Tab))
        {
            LoadGuide();
        }
        else
        {
            ExitGuide();
        }     
    }

    public virtual void Pause()
    {
        GameManager.instance.gameState = GameManager.GameState.PausedGame;
        menuDisplay.SetActive(true);

        if(clickSoundEnable)
        {
            StartCoroutine(delaySoundClickSound());
        }

        Time.timeScale = 0f;
        SettingsMenu.instance.SetVolume();
    }

    public virtual void Resume()
    {
        GameManager.instance.gameState = GameManager.GameState.Gameplay;
        menuDisplay.SetActive(false);
        
        if(backSoundEnable)
        {
            StartCoroutine(delaySoundBackSound());
        }

        Time.timeScale = 1.0f;
        SettingsMenu.instance.SetVolume();
    }

    public virtual void SaveGame()
    {
        if(GameManager.instance.gameState != GameManager.GameState.Cutscene && GameManager.instance.gameState != GameManager.GameState.Interaction)
        {
            GameManager.instance.gameState = GameManager.GameState.Gameplay;  
            menuDisplay.SetActive(false);

            if(backSoundEnable)
            {
            StartCoroutine(delaySoundBackSound());
            }
        
            Time.timeScale = 1.0f;
            SettingsMenu.instance.SetVolume();       

            HUDController.instance.SavedGameImage.GetComponent<Animation>().Play();
        }      
    }

    public virtual void Settings()
    {
        InSettings = true;

        if(clickSoundEnable)
        {
            StartCoroutine(delaySoundClickSound());
        }

        menuState = MenuState.Settings;
    }

    public virtual void DisplaySettings()
    {
        InSettings = true;

        if(clickSoundEnable)
        {
            StartCoroutine(delaySoundClickSound());
        }

        menuState = MenuState.DisplaySettings;
    }

    public virtual void AudioSettings()
    {
        InSettings = true;

        if(clickSoundEnable)
        {
            StartCoroutine(delaySoundClickSound());
        }

        menuState = MenuState.AudioSettings;
    }

    public virtual void ControlsSettings()
    {
        InSettings = true;

        if(clickSoundEnable)
        {
            StartCoroutine(delaySoundClickSound());
        }

        menuState = MenuState.ControlsSettings;
    }

    public virtual void Restart()
    {
        if(clickSoundEnable)
        {
            StartCoroutine(delaySoundClickSound());
        }

        if(GameManager.instance.gameState != GameManager.GameState.GameOver)
        {
            PlayerPrefs.SetInt("Restart", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Restart", 0);
        }
        
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);    
    }

    public virtual void LoadMenu()
    {
        if(clickSoundEnable)
        {
            StartCoroutine(delaySoundClickSound());
        }

        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Main Menu");
    }

    public virtual void LoadGuide()
    {
        guideDisplay.SetActive(true);
        HUDController.instance.ObjectiveUpdated.SetActive(false);
    }

    public virtual void ExitGuide()
    {
        guideDisplay.SetActive(false);
    }

    public virtual IEnumerator delaySoundClickSound()
    {
        clickSound.Play();
        clickSoundEnable = false;

        while(clickSound.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }    

        clickSoundEnable = true;
    }

    public virtual IEnumerator delaySoundBackSound()
    {
        backSound.Play();
        backSoundEnable = false;

        while(backSound.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }    

        backSoundEnable = true;
    }
}
