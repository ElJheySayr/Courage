using System.Collections;
using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEngine.AI;


public class Level5_Manager : MonoBehaviour
{
    public static Level5_Manager instance;

    public ObjectiveList objectiveList;

    public GameObject playerObj;
    public GameObject enemyObj;
    public GameObject vignetteObj;
    public GameObject strangerObj;
    public GameObject fadingWhite;
    public GameObject phoneObj;
    public GameObject cautionObj;
    public GameObject[] signObjects;

    public GameObject PlaygroundCollider;
    public GameObject PicnicCollider;
    public GameObject FoodStallsCollider;
    public GameObject ZumbaCollider;

    public bool enemyEnabled;
    public bool playerIsSafe;
    public bool playerAtTheFoodStall;
    public bool strangerInteracted;
    public bool changedMusic;

    public bool playgroundArrived;
    public bool picnicArrived;
    public bool foundFriends;
    public bool foodStallsArrived;
    public bool zumbaArrived; 

    protected virtual void Awake()
    {
        instance = this;
    }

    protected virtual void Start()
    {
        GameManager.instance.gameState = GameManager.GameState.Gameplay;
        
        playerObj = GameObject.FindGameObjectWithTag("Player");
        enemyObj = GameObject.Find("Enemy");
        vignetteObj = GameObject.Find("Vignette");
        strangerObj = GameObject.Find("Stranger Master");
        fadingWhite = GameObject.Find("Fading White UI");
        cautionObj = GameObject.Find("Caution Image");
        phoneObj = GameObject.Find("Phone");

        PlaygroundCollider = GameObject.Find("Playground Trigger");
        PicnicCollider = GameObject.Find("Picnic Trigger");
        FoodStallsCollider = GameObject.Find("Food Stalls Trigger");
        ZumbaCollider = GameObject.Find("Zumba Trigger");

        enemyObj.SetActive(false);
        vignetteObj.SetActive(false);
        fadingWhite.SetActive(false);
        cautionObj.SetActive(false);
        DisableAllSigns();

        if(PlayerPrefs.GetInt("LevelSaved") == 5 && PlayerPrefs.GetInt("Restart") == 0)
        {
            LoadGame();
        }
        else
        {
            StartCoroutine(FirstScene());
        }  
    } 

    public enum ObjectiveList
    {
        Start, 
        Playground,
        Picnic,
        FindYourFriends,
        FoodStalls,
        Zumba,
        ChangeMusic,
        End
    }

    public virtual void DisableAllSigns()
    {
        for(int i = 0; i < signObjects.Length; i++)
        {
            signObjects[i].SetActive(false);
        }
    }

    public virtual IEnumerator FirstScene()
    {
        enemyObj.SetActive(true);

        Level5_SoundManager.soundManager5.SayWithFreeze(0);

        while(Level5_SoundManager.soundManager5.dialoguePlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        Level5_SoundManager.soundManager5.PlaySFXWithFreeze(0);

        while(Level5_SoundManager.soundManager5.dialoguePlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(LookAt());
        Level5_SoundManager.soundManager5.PlayMultipleDialogueWithFreeze(1,2);

        while(Level5_SoundManager.soundManager5.dialoguePlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        objectiveList = ObjectiveList.Playground;
        HUDController.instance.objectiveListText.text = "Go to the Playground for safety.";
        HUDController.instance.ObjectiveUpdated.SetActive(true);
        enemyEnabled = true;
    }

    public virtual IEnumerator LookAt()
    {
        Quaternion lookRotation = Quaternion.LookRotation(enemyObj.transform.position - Camera.main.transform.position);
        float time = 0;

        while (time < 1)
        {
            Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, lookRotation, time);
            time += Time.deltaTime * 1;
            yield return new WaitForEndOfFrame();
        }
    }

    [System.Serializable]
    public class SaveData
    {
        public ObjectiveList objectiveList;
        public string objectiveListText;

        public Vector3 playerPosition;
        public Quaternion playerRotation;
        public Quaternion cameraRotation;
        public Vector3 enemyPosition;
        public Quaternion enemyRotation;
        public Vector3 strangerPosition;
        public Quaternion strangerRotation;

        public bool VignetteActive;
        public bool playGroundColliderActive;
        public bool picnicColliderActive;
        public bool foodStallsColliderActive;
        public bool zumbaColliderActive;

        public bool enemyObjEnabled;
        public bool enemyEnabled;
        public bool playerIsSafe;
        public bool playerAtTheFoodStall;
        public bool strangerInteracted;
        public bool changedMusic;

        public bool playgroundArrived;
        public bool picnicArrived;
        public bool foundFriends;
        public bool foodStallsArrived;
        public bool zumbaArrived; 

        public bool ambienceSoundPlaying;
        public bool friendsSignActive;
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            objectiveList = this.objectiveList,
            objectiveListText = HUDController.instance.objectiveListText.text,
            
            playerPosition = this.playerObj.transform.position,
            playerRotation = this.playerObj.transform.rotation,
            cameraRotation = Camera.main.transform.rotation,
            enemyPosition = this.enemyObj.transform.position,
            enemyRotation = this.enemyObj.transform.rotation,
            strangerPosition = this.strangerObj.transform.position,
            strangerRotation = this.strangerObj.transform.rotation,

            VignetteActive = this.vignetteObj.activeSelf,
            enemyObjEnabled = this.enemyObj.activeSelf,
            playGroundColliderActive = this.PlaygroundCollider.activeSelf,
            picnicColliderActive = this.PicnicCollider.activeSelf,
            foodStallsColliderActive = this.FoodStallsCollider.activeSelf,
            zumbaColliderActive = this.ZumbaCollider.activeSelf,

            enemyEnabled = this.enemyEnabled,
            playerIsSafe = this.playerIsSafe,
            playerAtTheFoodStall = this.playerAtTheFoodStall,
            strangerInteracted = this.strangerInteracted,
            changedMusic = this.changedMusic,

            playgroundArrived = this.playgroundArrived,
            picnicArrived = this.picnicArrived,
            foundFriends = this.foundFriends,
            foodStallsArrived = this.foodStallsArrived,
            zumbaArrived = this.zumbaArrived,

            ambienceSoundPlaying = Level5_SoundManager.soundManager5.ambienceSound.isPlaying,
            friendsSignActive = signObjects[0].activeSelf
        };

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/level5Savefile.json", json);
        PlayerPrefs.SetInt("Restart", 0);
        PlayerPrefs.SetInt("LevelSaved", 5);
        PauseMenu.instance.SaveGame();
        Debug.Log("Game Saved!");
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/level5Savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            this.objectiveList = saveData.objectiveList;
            HUDController.instance.objectiveListText.text = saveData.objectiveListText;

            vignetteObj.SetActive(saveData.VignetteActive);
            enemyObj.SetActive(saveData.enemyObjEnabled);
            PlaygroundCollider.SetActive(saveData.playGroundColliderActive);
            PicnicCollider.SetActive(saveData.picnicColliderActive);
            FoodStallsCollider.SetActive(saveData.foodStallsColliderActive);
            ZumbaCollider.SetActive(saveData.zumbaColliderActive);

            playerObj.GetComponent<CharacterController>().enabled = false;
            playerObj.transform.position = saveData.playerPosition;
            playerObj.transform.rotation = saveData.playerRotation;
            Camera.main.transform.rotation = saveData.cameraRotation;
            playerObj.GetComponent<CharacterController>().enabled = true;

            enemyObj.GetComponent<NavMeshAgent>().enabled = false;
            enemyObj.transform.position = saveData.enemyPosition;
            enemyObj.transform.rotation = saveData.enemyRotation;
            enemyObj.GetComponent<NavMeshAgent>().enabled = true;

            strangerObj.GetComponent<NavMeshAgent>().enabled = false;
            strangerObj.transform.position = saveData.strangerPosition;
            strangerObj.transform.rotation = saveData.strangerRotation;
            strangerObj.GetComponent<NavMeshAgent>().enabled = true;

            this.enemyEnabled = saveData.enemyEnabled;
            this.playerIsSafe = saveData.playerIsSafe;
            this.playerAtTheFoodStall = saveData.playerAtTheFoodStall;
            this.strangerInteracted = saveData.strangerInteracted;
            this.changedMusic = saveData.changedMusic;
            this.playgroundArrived = saveData.playgroundArrived;
            this.picnicArrived = saveData.picnicArrived;
            this.foundFriends = saveData.foundFriends;
            this.foodStallsArrived = saveData.foodStallsArrived;
            this.zumbaArrived = saveData.zumbaArrived;
            
            signObjects[0].SetActive(saveData.friendsSignActive);

            if (saveData.ambienceSoundPlaying)
            {
                if (!Level5_SoundManager.soundManager5.ambienceSound.isPlaying)
                {
                    Level5_SoundManager.soundManager5.ambienceSound.Play();
                }         
            }
            else
            {
                if (Level5_SoundManager.soundManager5.ambienceSound.isPlaying)
                {
                    Level5_SoundManager.soundManager5.ambienceSound.Stop();
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
