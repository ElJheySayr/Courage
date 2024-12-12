using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using System.IO;

public class Level1_Manager : MonoBehaviour
{
    public static Level1_Manager instance;

    public GameObject playerObject;
    public GameObject jackObject;
    public GameObject[] trophiesObjects;
    public GameObject instructionUI;

    public GameObject[] signObjects;

    public bool firstInteraction;
    public bool Trophy1;
    public bool Trophy1LookAt;
    public bool Trophy2;
    public bool Trophy2LookAt;
    public bool Trophy3;
    public bool Trophy3LookAt;
    public bool lastInteraction;

    public bool AiIsEnable;

    public Transform afterTrophieTP;
    public Transform aiStartPosition;

    public ObjectiveList objectiveList;
    public GameObject globalVolume;

    protected virtual void Awake()
    {
        instance = this;
    }

    protected virtual void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        jackObject = GameObject.Find("Jack MASTER");
        trophiesObjects[0] = GameObject.Find("Trophy 1"); trophiesObjects[1] = GameObject.Find("Trophy 2"); trophiesObjects[2] = GameObject.Find("Trophy 3");
        afterTrophieTP = GameObject.Find("AfterTrophies TP").transform;
        aiStartPosition = GameObject.Find("Point 1").transform;
        globalVolume = GameObject.Find("Global Volume");
        DisableAllSigns();

        if(PlayerPrefs.GetInt("LevelSaved") == 1 && PlayerPrefs.GetInt("Restart") == 0)
        {
            LoadGame();
        }
        else
        {
            Level1_Cutscenes.instance.PlayFirstCutscene();
        }
    }

    public enum ObjectiveList
    {
        Start,
        FirstInteractionWithFamily,
        FindTheTrophies,
        LastInteractionWithFamily,
        End
    }

    public virtual void DisableAllSigns()
    {
        for(int i = 0; i < signObjects.Length; i++)
        {
            signObjects[i].SetActive(false);
        }
    }

    public virtual void BackButton()
    {
        GameManager.instance.gameState = GameManager.GameState.Gameplay;
        PauseMenu.instance.backSound.Play(); 
        instructionUI.SetActive(false);
    }

    public virtual void AiFunction()
    {
        StartCoroutine(AiCondition());
    }

    public virtual IEnumerator AiCondition()
    {
        Vector3 AiStartPostion = aiStartPosition.position;
        Vector3 PlayPos = afterTrophieTP.position;
        Quaternion PlayRot = afterTrophieTP.rotation;

        if (objectiveList == ObjectiveList.FindTheTrophies && !Trophy1 && !Trophy2 && !Trophy3 && !AiIsEnable)
        {
            AiIsEnable = true;
            Level1_SoundManager.soundManager1.suspenseSound.Play();
            HUDController.instance.objectiveListText.text = "Find the three achievements that you received before.";
            playerObject.transform.localScale = new Vector3(0.025f, 0.025f, 0.025f);
            jackObject.transform.position = aiStartPosition.position;
        }
        else if (objectiveList == ObjectiveList.FindTheTrophies && Trophy1 && Trophy2 && Trophy3 && AiIsEnable)
        {
            AiIsEnable = false;
            playerObject.GetComponent<CharacterController>().enabled = false;
            playerObject.transform.position = PlayPos;
            playerObject.transform.rotation = PlayRot;
            playerObject.GetComponent<CharacterController>().enabled = true;
            jackObject.SetActive(false);
            Level1_Cutscenes.instance.PlayThirdCutsence();
            HUDController.instance.objectiveListText.text = "Go say goodbye to your mom before you head to school.";
            playerObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }   

        yield return new WaitForEndOfFrame();
    }

    [System.Serializable]
    public class SaveData
    {
        public bool firstInteraction;
        public bool Trophy1;
        public bool Trophy1LookAt;
        public bool Trophy2;
        public bool Trophy2LookAt;
        public bool Trophy3;
        public bool Trophy3LookAt;
        public bool lastInteraction;

        public bool AiIsEnable;

        public ObjectiveList objectiveList;

        public Vector3 playerPosition;
        public Quaternion playerRotation;
        public Quaternion cameraRotation;
        public Vector3 playerScale;
        public Vector3 jackPosition;
        public Quaternion jackRotation;

        public bool jackObjectActive;
        public bool trophy1Active;
        public bool trophy2Active;
        public bool trophy3Active;

        public string objectiveListText;
        public bool ambienceSoundPlaying;
        public bool suspenseSoundPlaying;

        public bool familySignActive;

    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            firstInteraction = this.firstInteraction,
            Trophy1 = this.Trophy1,
            Trophy1LookAt = this.Trophy1LookAt,
            Trophy2 = this.Trophy2,
            Trophy2LookAt = this.Trophy2LookAt,
            Trophy3 = this.Trophy3,
            Trophy3LookAt = this.Trophy3LookAt,
            lastInteraction = this.lastInteraction,
            AiIsEnable = this.AiIsEnable,
            objectiveList = this.objectiveList,
            playerPosition = playerObject.transform.position,
            playerRotation = playerObject.transform.rotation,
            cameraRotation = Camera.main.transform.rotation,
            playerScale = playerObject.transform.localScale,
            jackPosition = jackObject.transform.position,
            jackRotation = jackObject.transform.rotation,

            jackObjectActive = jackObject.activeSelf,
            trophy1Active = trophiesObjects[0].activeSelf,
            trophy2Active = trophiesObjects[1].activeSelf,
            trophy3Active = trophiesObjects[2].activeSelf,

            objectiveListText = HUDController.instance.objectiveListText.text,
            ambienceSoundPlaying = Level1_SoundManager.soundManager1.ambienceSound.isPlaying,
            suspenseSoundPlaying = Level1_SoundManager.soundManager1.suspenseSound.isPlaying,

            familySignActive = signObjects[0].activeSelf
        };

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/level1Savefile.json", json);     
        PlayerPrefs.SetInt("Restart", 0);
        PlayerPrefs.SetInt("LevelSaved", 1);
        PauseMenu.instance.SaveGame();
        Debug.Log("Game Saved!");
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/level1Savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            this.firstInteraction = saveData.firstInteraction;
            this.Trophy1 = saveData.Trophy1;
            this.Trophy1LookAt = saveData.Trophy1LookAt;
            this.Trophy2 = saveData.Trophy2;
            this.Trophy2LookAt = saveData.Trophy2LookAt;
            this.Trophy3 = saveData.Trophy3;
            this.Trophy3LookAt = saveData.Trophy3LookAt;
            this.lastInteraction = saveData.lastInteraction;
            this.AiIsEnable = saveData.AiIsEnable;
            this.objectiveList = saveData.objectiveList;

            playerObject.GetComponent<CharacterController>().enabled = false;
            playerObject.transform.position = saveData.playerPosition;
            playerObject.transform.rotation = saveData.playerRotation;
            playerObject.transform.localScale = saveData.playerScale;
            Camera.main.transform.rotation = saveData.cameraRotation;
            playerObject.GetComponent<CharacterController>().enabled = true;
            jackObject.transform.position = saveData.jackPosition;
            jackObject.transform.rotation = saveData.jackRotation;

            jackObject.SetActive(saveData.jackObjectActive);
            trophiesObjects[0].SetActive(saveData.trophy1Active);
            trophiesObjects[1].SetActive(saveData.trophy2Active);
            trophiesObjects[2].SetActive(saveData.trophy3Active);

            signObjects[0].SetActive(saveData.familySignActive);

            HUDController.instance.objectiveListText.text = saveData.objectiveListText;

            if (saveData.ambienceSoundPlaying)
            {
                if (!Level1_SoundManager.soundManager1.ambienceSound.isPlaying)
                {
                    Level1_SoundManager.soundManager1.ambienceSound.Play();
                }         
            }
            else
            {
                if (Level1_SoundManager.soundManager1.ambienceSound.isPlaying)
                {
                    Level1_SoundManager.soundManager1.ambienceSound.Stop();
                }       
            }

            if (saveData.suspenseSoundPlaying)
            {
                if (!Level1_SoundManager.soundManager1.suspenseSound.isPlaying)
                {
                    Level1_SoundManager.soundManager1.suspenseSound.Play();
                }         
            }
            else
            {
                if (Level1_SoundManager.soundManager1.suspenseSound.isPlaying)
                {
                    Level1_SoundManager.soundManager1.suspenseSound.Stop();
                }       
            }

            PlayerPrefs.SetInt("Restart", 0);
 
            Debug.Log("Game Loaded!");
        }
        else
        {
            Debug.Log("No save file found.");
        }
    }
}