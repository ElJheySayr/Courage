using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.AI;

public class Level2_Manager : MonoBehaviour
{
    public static Level2_Manager instance;
    public ObjectiveList objectiveList;

    public GameObject playerObj;
    public GameObject friend1Obj;
    public GameObject friend2Obj;
    public GameObject bulliesObj;
    public GameObject bully1Obj;
    public GameObject bully2Obj;
    public GameObject profObj;
    public GameObject gradeBookObj;

    public GameObject[] signObjects;

    public bool friendsFirstInteraction;
    public bool professorFirstInteraction;
    public bool friendsSecondInteraction;
    public bool bullyInteraction;
    public bool bully1Enable;
    public bool bully2Enable;
    public bool professorSecondInteraction;
    public bool gradeBook;
    public bool professorThirdInteraction;

    protected virtual void Awake()
    {
        instance = this;
    }

    protected virtual void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        friend1Obj = GameObject.Find("Treisha");
        friend2Obj = GameObject.Find("Beatrice");
        bulliesObj = GameObject.Find("Bullies");
        bully1Obj = GameObject.Find("Bully 1");
        bully2Obj = GameObject.Find("Bully 2");
        profObj = GameObject.Find("Male Teacher 1 MASTER"); 
        gradeBookObj = GameObject.Find("Gradebook");

        bulliesObj.SetActive(false);
        DisableAllSigns();

        if(PlayerPrefs.GetInt("LevelSaved") == 2 && PlayerPrefs.GetInt("Restart") == 0)
        {
            LoadGame();
        }
        else
        {
            Level2_Cutscenes.instance.PlayFirstCutscene();
        }    
    }

    public enum ObjectiveList
    {
        Intro,
        GoToLibrary,
        TakeTheExam,
        AfterExamConsulation,
        GoToCafeteria,
        FirstFacultyConsultation, 
        GetGradeBookAtClassroom,
        GetGradeBookAtCafeteria,
        SecondFacultyConsultation,
        End
    }

    public virtual void DisableAllSigns()
    {
        for(int i = 0; i < signObjects.Length; i++)
        {
            signObjects[i].SetActive(false);
        }
    }

    public virtual void TeleportPlayer(float x, float y, float z, float rotation)
    {
        playerObj.gameObject.GetComponent<CharacterController>().enabled = false;
        playerObj.transform.position = new Vector3(x,y,z);
        playerObj.transform.rotation = Quaternion.Euler(playerObj.transform.rotation.x, rotation, playerObj.transform.rotation.z);
        playerObj.gameObject.GetComponent<CharacterController>().enabled = true;
    }

    [System.Serializable]
    public class SaveData
    {
        public bool friendsFirstInteraction;
        public bool professorFirstInteraction;
        public bool friendsSecondInteraction;
        public bool bullyInteraction;
        public bool bully1Enable;
        public bool bully2Enable;
        public bool professorSecondInteraction;
        public bool gradeBook;
        public bool professorThirdInteraction;

        public ObjectiveList objectiveList;
        public string objectiveListText;

        public Vector3 playerPosition;
        public Quaternion playerRotation;
        public Quaternion cameraRotation;

        public Vector3 friend1Pos;
        public Quaternion friend1Rot;
        public Vector3 friend2Pos;
        public Quaternion friend2Rot;
        public Vector3 bully1Pos;
        public Quaternion bully1Rot;
        public Vector3 bully2Pos;
        public Quaternion bully2Rot;
        public Vector3 profPos;
        public Quaternion profRot;

        public bool bullyActive;
        public bool gradeBookActive;
        public bool ambienceSoundPlaying;
        public bool studentAmbienceSoundPlaying;

        public bool bully2NavMeshActive;
        public bool friendsSignActive;
        public bool professorSignActive;
    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            friendsFirstInteraction = this.friendsFirstInteraction,
            professorFirstInteraction = this.professorFirstInteraction,
            friendsSecondInteraction = this.friendsSecondInteraction,
            bullyInteraction = this.bullyInteraction,
            bully1Enable = this.bully1Enable,
            bully2Enable = this.bully2Enable,
            professorSecondInteraction = this.professorSecondInteraction,
            gradeBook = this.gradeBook,
            professorThirdInteraction = this.professorThirdInteraction,

            objectiveList = this.objectiveList,
            playerPosition = playerObj.transform.position,
            playerRotation = playerObj.transform.rotation,
            cameraRotation = Camera.main.transform.rotation,

            friend1Pos = friend1Obj.transform.position,
            friend1Rot = friend1Obj.transform.rotation,
            friend2Pos = friend2Obj.transform.position,
            friend2Rot = friend2Obj.transform.rotation,
            bully1Pos = bully1Obj.transform.position,
            bully1Rot = bully1Obj.transform.rotation,
            bully2Pos = bully2Obj.transform.position,
            bully2Rot = bully2Obj.transform.rotation,
            profPos = profObj.transform.position,
            profRot = profObj.transform.rotation,

            objectiveListText = HUDController.instance.objectiveListText.text,

            ambienceSoundPlaying = Level2_SoundManager.soundManager2.ambienceSound.isPlaying,
            studentAmbienceSoundPlaying = Level2_SoundManager.soundManager2.studentAmbienceSound.isPlaying,

            bullyActive = bulliesObj.activeSelf,
            gradeBookActive = gradeBookObj.activeSelf,

            bully2NavMeshActive = bully2Obj.GetComponent<NavMeshAgent>().enabled,
            friendsSignActive = signObjects[0].activeSelf,
            professorSignActive = signObjects[1].activeSelf,
        };

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/level2Savefile.json", json);
        PlayerPrefs.SetInt("Restart", 0);
        PlayerPrefs.SetInt("LevelSaved", 2);
        PauseMenu.instance.SaveGame();
        Debug.Log("Game Saved!");
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/level2Savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            this.friendsFirstInteraction = saveData.friendsFirstInteraction;
            this.professorFirstInteraction = saveData.professorFirstInteraction;
            this.friendsSecondInteraction = saveData.friendsSecondInteraction;
            this.bullyInteraction = saveData.bullyInteraction;
            this.bully1Enable = saveData.bully1Enable;
            this.bully2Enable = saveData.bully2Enable;
            this.professorSecondInteraction = saveData.professorSecondInteraction;
            this.gradeBook = saveData.gradeBook;
            this.professorThirdInteraction = saveData.professorThirdInteraction;
            this.objectiveList = saveData.objectiveList;

            playerObj.GetComponent<CharacterController>().enabled = false;
            playerObj.transform.position = saveData.playerPosition;
            playerObj.transform.rotation = saveData.playerRotation;
            Camera.main.transform.rotation = saveData.cameraRotation;
            playerObj.GetComponent<CharacterController>().enabled = true;       

            friend1Obj.transform.position = saveData.friend1Pos;
            friend1Obj.transform.rotation = saveData.friend1Rot;
            friend2Obj.transform.position = saveData.friend2Pos;
            friend2Obj.transform.rotation = saveData.friend2Rot;
            bully1Obj.transform.position = saveData.bully1Pos;
            bully1Obj.transform.rotation = saveData.bully1Rot;
            bully2Obj.transform.position = saveData.bully2Pos;
            bully2Obj.transform.rotation = saveData.bully2Rot;
            bully2Obj.GetComponent<NavMeshAgent>().enabled = saveData.bully2NavMeshActive;
            profObj.transform.position = saveData.profPos;
            profObj.transform.rotation = saveData.profRot;

            bulliesObj.SetActive(saveData.bullyActive);
            gradeBookObj.SetActive(saveData.gradeBookActive);
            
            HUDController.instance.objectiveListText.text = saveData.objectiveListText;

            signObjects[0].SetActive(saveData.friendsSignActive);
            signObjects[1].SetActive(saveData.professorSignActive);

            if (saveData.ambienceSoundPlaying)
            {
                if (!Level2_SoundManager.soundManager2.ambienceSound.isPlaying)
                {
                    Level2_SoundManager.soundManager2.ambienceSound.Play();
                }         
            }
            else
            {
                if (Level2_SoundManager.soundManager2.ambienceSound.isPlaying)
                {
                    Level2_SoundManager.soundManager2.ambienceSound.Stop();
                }       
            }

            if (saveData.studentAmbienceSoundPlaying)
            {
                if (!Level2_SoundManager.soundManager2.studentAmbienceSound.isPlaying)
                {
                    Level2_SoundManager.soundManager2.studentAmbienceSound.Play();
                }         
            }
            else
            {
                if (Level2_SoundManager.soundManager2.studentAmbienceSound.isPlaying)
                {
                    Level2_SoundManager.soundManager2.studentAmbienceSound.Stop();
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
