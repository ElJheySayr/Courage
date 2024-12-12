using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;
using System.IO;

public class Level3_Manager : MonoBehaviour
{
    public static Level3_Manager instance;
    public ObjectiveList objectiveList;
    public float time = 600f;
    public TMP_Text timerText;

    public GameObject playerObj;
    public GameObject movingNpc;
    public GameObject[] signObjects;

    public int itemsCollected;
    public bool ps4GameCollected;
    public bool xboxGameCollected;
    public bool greenTshirtCollected;
    public bool grayPantsCollected;
    public bool watercolorCollected;
    public bool redNotebookCollected;
    public bool allItemsCollected;

    public bool npcOnTheGo;
    public bool npcArrived;
    public bool warningTriggered;

    protected virtual void Awake()
    {
        instance = this;
    }

    protected virtual void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        movingNpc = GameObject.Find("male consumer 2 MASTER");
        DisableAllSigns();

        if(PlayerPrefs.GetInt("LevelSaved") == 3 && PlayerPrefs.GetInt("Restart") == 0)
        {
            LoadGame();
        }
        else
        {
            Level3_Cutscenes.instance.PlayFirstCutscene();
        }      
    }

    protected virtual void Update()
    {
        if(objectiveList == ObjectiveList.GetItems)
        {
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time - minutes * 60);

            string timeString = string.Format("{0:0}:{1:00}", minutes, seconds);
            timerText.text = timeString;
            time -= Time.deltaTime;
            if(time <= 0)
            {
                GameManager.instance.gameState = GameManager.GameState.GameOver;
            }
            else if(time <= 360)
            {
                npcOnTheGo = true;
            }
        }
    }

    
    public enum ObjectiveList 
    {
        Intro,
        GetItems,
        Checkout,
        End
    }

    public virtual void DisableAllSigns()
    {
        for(int i = 0; i < signObjects.Length; i++)
        {
            signObjects[i].SetActive(false);
        }
    }

    public List<string> itemList = new List<string>
    {
        "- Green Game",
        "- Blue Game",
        "- Green T-Shirt",
        "- Gray Pants",
        "- Watercolor",
        "- Red Notebook"
    };

    public virtual void UpdateObjectiveText()
    {
        HUDController.instance.objectiveListText.text = "Go find and buy at least 5 of the following:\n";
        foreach (string objective in itemList)
        {
            HUDController.instance.objectiveListText.text += objective + "\n";
        }
    }

    public virtual void CheckAndRemoveObjective(string objectiveToRemove)
    {
        itemList.RemoveAll(obj => obj.Contains(objectiveToRemove));
        UpdateObjectiveText();
    }

    [System.Serializable]
    public class SaveData
    {
        public ObjectiveList objectiveList;
        public string objectiveListText;
        public List<string> objectives;
        public float time;
        public bool timerTextEnabled;

        public Vector3 playerPosition;
        public Quaternion playerRotation;
        public Quaternion cameraRotation;

        public Vector3 npcPosition;
        public Quaternion npcRotation;

        public int itemsCollected;
        public bool ps4GameCollected;
        public bool xboxGameCollected;
        public bool greenTshirtCollected;
        public bool grayPantsCollected;
        public bool watercolorCollected;
        public bool redNotebookCollected;
        public bool allItemsCollected;

        public bool npcOnTheGo;
        public bool npcArrived;
        public bool warningTriggered; 

        public bool ambienceSoundPlaying;
        public bool cashierSignActive;
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            time = this.time,
            timerTextEnabled = this.timerText.gameObject.activeSelf,
            itemsCollected = itemsCollected,
            ps4GameCollected = this.ps4GameCollected,
            xboxGameCollected = this.xboxGameCollected,
            greenTshirtCollected = this.greenTshirtCollected,
            grayPantsCollected = this.grayPantsCollected,
            watercolorCollected = this.watercolorCollected,
            redNotebookCollected = this.redNotebookCollected,
            allItemsCollected = this.allItemsCollected,

            npcOnTheGo = this.npcOnTheGo,
            npcArrived = this.npcArrived,
            warningTriggered = this.warningTriggered,

            objectiveList = this.objectiveList,
            playerPosition = playerObj.transform.position,
            playerRotation = playerObj.transform.rotation,
            cameraRotation = Camera.main.transform.rotation,
            npcPosition = movingNpc.transform.position,
            npcRotation = movingNpc.transform.rotation,

            objectiveListText = HUDController.instance.objectiveListText.text,
            objectives = new List<string>(this.itemList),
            
            ambienceSoundPlaying = Level3_SoundManager.soundManager3.ambienceSound.isPlaying,
            cashierSignActive = signObjects[0].activeSelf
        };

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/level3Savefile.json", json);
        PlayerPrefs.SetInt("Restart", 0);
        PlayerPrefs.SetInt("LevelSaved", 3);
        PauseMenu.instance.SaveGame();
        Debug.Log("Game Saved!");
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/level3Savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            this.time = saveData.time;
            this.timerText.gameObject.SetActive(saveData.timerTextEnabled);
            this.itemsCollected = saveData.itemsCollected;
            this.ps4GameCollected = saveData.ps4GameCollected;
            this.xboxGameCollected = saveData.xboxGameCollected;
            this.greenTshirtCollected = saveData.greenTshirtCollected;
            this.grayPantsCollected = saveData.grayPantsCollected;
            this.watercolorCollected = saveData.watercolorCollected;
            this.redNotebookCollected = saveData.redNotebookCollected;
            this.allItemsCollected = saveData.allItemsCollected;

            this.npcOnTheGo = saveData.npcOnTheGo;
            this.npcArrived = saveData.npcArrived;
            this.warningTriggered = saveData.warningTriggered;

            this.movingNpc.transform.position = saveData.npcPosition;
            this.movingNpc.transform.rotation = saveData.npcRotation;
            this.objectiveList = saveData.objectiveList;
            HUDController.instance.objectiveListText.text = saveData.objectiveListText;

            playerObj.GetComponent<CharacterController>().enabled = false;
            playerObj.transform.position = saveData.playerPosition;
            playerObj.transform.rotation = saveData.playerRotation;
            Camera.main.transform.rotation = saveData.cameraRotation;
            playerObj.GetComponent<CharacterController>().enabled = true;

            this.itemList = new List<string>(saveData.objectives); 
            signObjects[0].SetActive(saveData.cashierSignActive);

            if(saveData.objectiveList == ObjectiveList.GetItems)
            {
                UpdateObjectiveText();
            }

            if (saveData.ambienceSoundPlaying)
            {
                if (!Level3_SoundManager.soundManager3.ambienceSound.isPlaying)
                {
                    Level3_SoundManager.soundManager3.ambienceSound.Play();
                }         
            }
            else
            {
                if (Level3_SoundManager.soundManager3.ambienceSound.isPlaying)
                {
                    Level3_SoundManager.soundManager3.ambienceSound.Stop();
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
