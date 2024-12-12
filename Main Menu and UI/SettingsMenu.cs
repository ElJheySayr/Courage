using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public static SettingsMenu instance;

    [Header("Display Settings")]
    public Volume renderVolume;
    public ColorAdjustments colorAdjustments;
    public Slider brightnessSlider;
    public Toggle fullscreenToggle;
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    [Header("Sound Settings")]
    public AudioMixer generalMixer;
    public AudioMixer gameMixer;
    public Slider volumeSlider;
    
    protected virtual void Awake()
    {
        instance = this;

        if(!renderVolume.profile.TryGet(out colorAdjustments)) throw new System.NullReferenceException(nameof(colorAdjustments));         
    }

    protected virtual void Start ()
    {
        LoadVolume();
        LoadBrightness();
        LoadFullscreen();   
        LoadResolution();
    }

    public virtual void SetVolume()
    {
        float volume = volumeSlider.value;

        generalMixer.SetFloat("General Volume", Mathf.Log10(volume) * 20);

        if(GameManager.instance.gameState == GameManager.GameState.Gameplay)
        {
            gameMixer.SetFloat("Game Volume", Mathf.Log10(volume) * 20);
        }
        else
        {
            gameMixer.SetFloat("Game Volume", -80f);
        }

        PlayerPrefs.SetFloat("VolumeSave", volume);
    }

    public virtual void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("VolumeSave");
        SetVolume();
    }

    public virtual void SetFullscreen()
    {
        bool isFullscreen = fullscreenToggle.isOn;

        if(isFullscreen)
        {
            PlayerPrefs.SetInt("FullscreenSave", 1);
        }
        else
        {
            PlayerPrefs.SetInt("FullscreenSave", 0);
        }

        Screen.fullScreen = isFullscreen;
    }

    public virtual void LoadFullscreen()
    {
        if(PlayerPrefs.GetInt("FullscreenSave") == 1)
        {
            fullscreenToggle.isOn = true;
        }
        else
        {
            fullscreenToggle.isOn = false;
        }

        SetFullscreen();
    }

    public virtual void SetBrightness()
    {
        float brightness = brightnessSlider.value;
        colorAdjustments.postExposure.value = brightness;

        PlayerPrefs.SetFloat("BrightnessSave", brightness);
    }

    public virtual void LoadBrightness()
    {
        brightnessSlider.value = PlayerPrefs.GetFloat("BrightnessSave");
        SetBrightness();
    }

    public virtual void SetResolution(int resolutionIndex)
    {
        switch (resolutionIndex)
        {
            case 0:
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                break;
            case 1: 
                Screen.SetResolution(1280, 720, Screen.fullScreen);
                break;
        }

        PlayerPrefs.SetInt("ResolutionSave", resolutionIndex);
    }

    public virtual void LoadResolution()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> reso = new List<string>
        {
            "1920 x 1080",
            "1280 x 720"
        };

        int savedResolutionIndex = PlayerPrefs.GetInt("ResolutionSave", -1);
        int currentResolution = 0;

        for(int i = 0; i < reso.Count; i++)
        {
            string[] dimensions = reso[i].Split('x');
            int width = int.Parse(dimensions[0].Trim());
            int height = int.Parse(dimensions[1].Trim());

            if(i == savedResolutionIndex)
            {
                currentResolution = i;
            }
            else if(savedResolutionIndex == -1 && resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolution = i;
            }
        }

        resolutionDropdown.AddOptions(reso);
        resolutionDropdown.value = currentResolution;
        resolutionDropdown.RefreshShownValue();

        SetResolution(currentResolution);
    }
}
