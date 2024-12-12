using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class Level4_Manager : MonoBehaviour
{
    public static Level4_Manager instance;

    public GameObject playerObj;
    public GameObject cloneObj;
    public GameObject padlockObj;
    public GameObject certificatesObj;
    public GameObject[] signObjects;

    public float time = 360f;
    public TMP_Text timerText;
    public TMP_InputField inputField;
    public GameObject cabinetUI;

    public Sprite[] spritesImages;
    public Image itemImage;
    public GameObject itemDisplay;

    public bool interactedWithAuntie;
    public bool goToTheBathroom;
    public bool cabinetUnlocked;
    public bool cabinetOpened;
    public bool cerificates;
    public bool interactedWithUncle;
    public JournalPage journalPage;
    public bool inspirationalBooks;
    public bool journal;
    public bool telephone;
    public bool drawings;
    public bool exercise;
    public bool interactedWithGrandpa;

    public GameObject cabinetLeftDoorObj;
    public BoxCollider cabinetCollider;
    public GameObject cabinetRightDoorObj;

    public ObjectiveList objectiveList;

    protected virtual void Awake()
    {
        instance = this;     
    }

    protected virtual void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        cloneObj = GameObject.Find("Clone");
        padlockObj = GameObject.Find("Padlock");
        cabinetLeftDoorObj = GameObject.Find("Left");
        cabinetRightDoorObj = GameObject.Find("Right");
        certificatesObj = GameObject.Find("Certificates of Joshua");
        DisableAllSigns();

        if(PlayerPrefs.GetInt("LevelSaved") == 4 && PlayerPrefs.GetInt("Restart") == 0)
        {
            LoadGame();
        }
        else
        {
            Level4_Cutscenes.instance.PlayFirstCutscene();
        }     
    }

    protected virtual void Update()
    {
        if(objectiveList == ObjectiveList.OldParentRoom || objectiveList == ObjectiveList.GrandpaOffice)
        {
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time - minutes * 60);

            string timeString = string.Format("{0:0}:{1:00}", minutes, seconds);
            timerText.text = timeString;
            time -= Time.deltaTime;

            if(time <= 0)
            {
                GameOver.instance.PlayGameOver();
            }
        }
    }

    public enum ObjectiveList
    {
        Intro,
        InteractWithAuntie,
        GoToBathroom,
        OldParentRoom,
        InteractWithUncle,
        GrandpaOffice,
        InteractWithGrandfather,
        End
    }

    public enum JournalPage
    {
        None,
        InspirationalBooks,
        Journal,
        Telephone,
        Drawings,
        ExerciseEquipments
    }

    public virtual void DisableAllSigns()
    {
        for(int i = 0; i < signObjects.Length; i++)
        {
            signObjects[i].SetActive(false);
        }
    }

    public virtual void JoshuaSize()
    {
        playerObj.transform.localScale = new Vector3(0.025f, 0.025f, 0.025f);
    }

    public virtual void ShowItem(int num)
    {
        HUDController.instance.interactionText.text = string.Empty;
        itemImage.sprite = spritesImages[num];
        GameManager.instance.gameState = GameManager.GameState.Interaction;
        SettingsMenu.instance.SetVolume();
        itemDisplay.SetActive(true);
    }

    public virtual void SubmitButton()
    {
        if(inputField.text == "231989" || inputField.text == "2/3/1989" || inputField.text == "February 3, 1989" || inputField.text == "february 3, 1989" || inputField.text == "Feb 3, 1989" || inputField.text == "feb 3, 1989")
        {
            Level4_SoundManager.soundManager4.PlaySFX(2);
            cabinetUnlocked = true;
            padlockObj.SetActive(false);
        }
        else
        {   
            if(!Level4_SoundManager.soundManager4.dialoguePlayer.isPlaying)
            {
                Level4_SoundManager.soundManager4.PlaySFX(3);
                Level4_SoundManager.soundManager4.Say(47);
            } 
        }

        GameManager.instance.gameState = GameManager.GameState.Gameplay;
        SettingsMenu.instance.SetVolume();
        cabinetUI.SetActive(false);
    }

    public virtual void BackButton()
    {
        GameManager.instance.gameState = GameManager.GameState.Gameplay;
        SettingsMenu.instance.SetVolume();
        itemDisplay.SetActive(false);
    }

    [System.Serializable]
    public class SaveData
    {
        public float time;
        public bool interactedWithAuntie;
        public bool goToTheBathroom;
        public bool cabinetUnlocked;
        public bool cabinetOpened;
        public bool cerificates;
        public bool interactedWithUncle;
        public JournalPage journalPage;
        public bool inspirationalBooks;
        public bool journal;
        public bool telephone;
        public bool drawings;
        public bool exercise;
        public bool interactedWithGrandpa;

        public ObjectiveList objectiveList;
        public string objectiveListText;

        public Vector3 playerPosition;
        public Quaternion playerRotation;
        public Vector3 playerScale;
        public Quaternion cameraRotation;

        public Quaternion cabinetDoorLeft;
        public Quaternion cabinetDoorRight;

        public bool timeTextActive;
        public bool cloneActive;
        public bool padlockActive;
        public bool certificatesActive;

        public bool cabinetColliderActive;

        public bool ambienceSoundPlaying;
        public bool anxietySoundPlaying;

        public bool auntSignActive;
        public bool uncleSignActive;
        public bool grandpaSignActive;
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            goToTheBathroom = this.goToTheBathroom,
            cabinetUnlocked = this.cabinetUnlocked,
            cabinetOpened = this.cabinetOpened,
            interactedWithUncle = this.interactedWithUncle,
            cerificates = this.cerificates,
            journalPage = this.journalPage,
            inspirationalBooks = this.inspirationalBooks,
            journal = this.journal,
            telephone = this.telephone,
            drawings = this.drawings,
            exercise = this.exercise,
            interactedWithGrandpa = this.interactedWithGrandpa,

            objectiveList = this.objectiveList,
            objectiveListText = HUDController.instance.objectiveListText.text,
            time = this.time,
            playerPosition = this.playerObj.transform.position,
            playerRotation = this.playerObj.transform.rotation,
            playerScale = this.playerObj.transform.localScale,
            cameraRotation = Camera.main.transform.rotation,

            cabinetDoorLeft = this.cabinetLeftDoorObj.transform.rotation,
            cabinetDoorRight = this.cabinetRightDoorObj.transform.rotation,

            timeTextActive = this.timerText.gameObject.activeSelf,
            cloneActive = this.cloneObj.gameObject.activeSelf,
            padlockActive = this.padlockObj.gameObject.activeSelf,
            certificatesActive = this.certificatesObj.gameObject.activeSelf,

            cabinetColliderActive = this.cabinetCollider.enabled,

            ambienceSoundPlaying = Level4_SoundManager.soundManager4.ambienceSound.isPlaying,
            anxietySoundPlaying = Level4_SoundManager.soundManager4.anxietySound.isPlaying,

            auntSignActive = signObjects[0].activeSelf,
            uncleSignActive = signObjects[1].activeSelf,
            grandpaSignActive = signObjects[2].activeSelf
        };

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/level4Savefile.json", json);
        PlayerPrefs.SetInt("Restart", 0);
        PlayerPrefs.SetInt("LevelSaved", 4);
        PauseMenu.instance.SaveGame();
        Debug.Log("Game Saved!");
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/level4Savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            this.goToTheBathroom = saveData.goToTheBathroom;
            this.cabinetUnlocked = saveData.cabinetUnlocked;
            this.cabinetOpened = saveData.cabinetOpened;
            this.interactedWithUncle = saveData.interactedWithUncle;
            this.cerificates = saveData.cerificates;
            this.journalPage = saveData.journalPage;
            this.inspirationalBooks = saveData.inspirationalBooks;
            this.journal = saveData.journal;
            this.telephone = saveData.telephone;
            this.drawings = saveData.drawings;
            this.exercise = saveData.exercise;
            this.interactedWithGrandpa = saveData.interactedWithGrandpa;

            this.objectiveList = saveData.objectiveList;
            HUDController.instance.objectiveListText.text = saveData.objectiveListText;

            playerObj.GetComponent<CharacterController>().enabled = false;
            playerObj.transform.position = saveData.playerPosition;
            playerObj.transform.rotation = saveData.playerRotation;
            playerObj.transform.localScale = saveData.playerScale;
            Camera.main.transform.rotation = saveData.cameraRotation;
            playerObj.GetComponent<CharacterController>().enabled = true;

            timerText.gameObject.SetActive(saveData.timeTextActive);
            cloneObj.SetActive(saveData.cloneActive);
            padlockObj.SetActive(saveData.padlockActive);
            certificatesObj.SetActive(saveData.certificatesActive);

            cabinetLeftDoorObj.transform.rotation = saveData.cabinetDoorLeft;
            cabinetRightDoorObj.transform.rotation = saveData.cabinetDoorRight;

            cabinetCollider.enabled = saveData.cabinetColliderActive;
            
            signObjects[0].SetActive(saveData.auntSignActive);
            signObjects[1].SetActive(saveData.uncleSignActive);
            signObjects[2].SetActive(saveData.grandpaSignActive);

            if (saveData.ambienceSoundPlaying)
            {
                if (!Level4_SoundManager.soundManager4.ambienceSound.isPlaying)
                {
                    Level4_SoundManager.soundManager4.ambienceSound.Play();
                }         
            }
            else
            {
                if (Level4_SoundManager.soundManager4.ambienceSound.isPlaying)
                {
                    Level4_SoundManager.soundManager4.ambienceSound.Stop();
                }       
            }

            if (saveData.anxietySoundPlaying)
            {
                if (!Level4_SoundManager.soundManager4.anxietySound.isPlaying)
                {
                    Level4_SoundManager.soundManager4.anxietySound.Play();
                }         
            }
            else
            {
                if (Level4_SoundManager.soundManager4.anxietySound.isPlaying)
                {
                    Level4_SoundManager.soundManager4.anxietySound.Stop();
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
