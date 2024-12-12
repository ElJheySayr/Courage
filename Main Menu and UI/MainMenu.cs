using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Sounds")]
    public AudioSource bgMusic;
    public AudioSource clickSound;
    public AudioSource backSound;
    private bool clickSoundEnable = true;
    private bool backSoundEnable = true;

    [Header("GameObjects")]
    public GameObject menuPanel;  
    public GameObject settingsPanel;
    public GameObject displaySettingsPanel;  
    public GameObject audioSettingsPanel;  
    public GameObject controlsSettingsPanel;  
    public GameObject creditsPanel;  
    public GameObject continueButton;

    public MenuState menuState;
    public Sprite[] levelImages;
    public Image currentLevelImage;

    public enum MenuState
    {
        MainMenu,
        Settings,
        DisplaySettings,
        AudioSettings,
        ControlsSettings,
        Credits
    }
    protected virtual void Start()
    {
        bgMusic.Play();
        menuState = MenuState.MainMenu;

        if(PlayerPrefs.HasKey("LevelSaved") && PlayerPrefs.GetInt("Restart") != 1)
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }

    protected void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            if(menuState == MenuState.Settings)
            {
                menuState = MenuState.MainMenu;
                settingsPanel.SetActive(false);
                menuPanel.SetActive(true);

                if(backSoundEnable)
                {
                    StartCoroutine(delaySoundBackSound());
                }
            }
            else if(menuState == MenuState.DisplaySettings)
            {
                menuState = MenuState.MainMenu;
                menuPanel.SetActive(true);
                settingsPanel.SetActive(false);
                displaySettingsPanel.SetActive(false);
                audioSettingsPanel.SetActive(false);
                controlsSettingsPanel.SetActive(false);

                if(backSoundEnable)
                {
                    StartCoroutine(delaySoundBackSound());
                }
            }
            else if(menuState == MenuState.AudioSettings)
            {
                menuState = MenuState.MainMenu;
                menuPanel.SetActive(true);
                settingsPanel.SetActive(false);
                displaySettingsPanel.SetActive(false);
                audioSettingsPanel.SetActive(false);
                controlsSettingsPanel.SetActive(false);

                if(backSoundEnable)
                {
                    StartCoroutine(delaySoundBackSound());
                }
            }
            else if(menuState == MenuState.ControlsSettings)
            {
                menuState = MenuState.MainMenu;
                menuPanel.SetActive(true);
                settingsPanel.SetActive(false);
                displaySettingsPanel.SetActive(false);
                audioSettingsPanel.SetActive(false);
                controlsSettingsPanel.SetActive(false);

                if(backSoundEnable)
                {
                    StartCoroutine(delaySoundBackSound());
                }
            }
            else if(menuState == MenuState.Credits)
            {
                menuState = MenuState.MainMenu;
                creditsPanel.SetActive(false);
                menuPanel.SetActive(true);

                if(backSoundEnable)
                {
                    StartCoroutine(delaySoundBackSound());
                }   
            }
        }
    }

    public virtual void ContinueStory()
    {
        if(clickSoundEnable)
        {
            StartCoroutine(delaySoundClickSound());
        }
        
        
        if(PlayerPrefs.GetInt("LevelSaved") == 1)
        {
            currentLevelImage.sprite = levelImages[0];
            currentLevelImage.gameObject.SetActive(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (PlayerPrefs.GetInt("LevelSaved") == 2)
        {
            currentLevelImage.sprite = levelImages[1];
            currentLevelImage.gameObject.SetActive(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
        else if (PlayerPrefs.GetInt("LevelSaved") == 3)
        {
            currentLevelImage.sprite = levelImages[2];
            currentLevelImage.gameObject.SetActive(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
        }
        else if (PlayerPrefs.GetInt("LevelSaved") == 4)
        {
            currentLevelImage.sprite = levelImages[3];
            currentLevelImage.gameObject.SetActive(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
        }
        else if (PlayerPrefs.GetInt("LevelSaved") == 5)
        {
            currentLevelImage.sprite = levelImages[4];
            currentLevelImage.gameObject.SetActive(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 5);
        }
    }

    public virtual void NewStory()
    {
        if(clickSoundEnable)
        {
            StartCoroutine(delaySoundClickSound());
        }

        PlayerPrefs.DeleteKey("LevelSaved");

        currentLevelImage.sprite = levelImages[0];
        currentLevelImage.gameObject.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public virtual void Settings()
    {
        if(clickSoundEnable)
        {
            StartCoroutine(delaySoundClickSound());
        }

        menuState = MenuState.Settings;
    }

    public virtual void DisplaySettings()
    {
        if(clickSoundEnable)
        {
            StartCoroutine(delaySoundClickSound());
        }

        menuState = MenuState.DisplaySettings;
    }

    public virtual void AudioSettings()
    {
        if(clickSoundEnable)
        {
            StartCoroutine(delaySoundClickSound());
        }

        menuState = MenuState.AudioSettings;
    }

    public virtual void ControlsSettings()
    {
        if(clickSoundEnable)
        {
            StartCoroutine(delaySoundClickSound());
        }

        menuState = MenuState.ControlsSettings;
    }

    public virtual void Credits()
    {
        if(clickSoundEnable)
        {
            StartCoroutine(delaySoundClickSound());
        }

        menuState = MenuState.Credits;
    }

    public virtual void ExitFunction()
    {
        if(clickSoundEnable)
        {
            StartCoroutine(delaySoundClickSound());
        }

        Application.Quit();
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
