using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public static GameManager instance;

    public LevelState levelState;
    public GameState gameState;

    public enum LevelState
    {
        MainMenu,
        Stage1,
        Stage2,
        Stage3, 
        Stage4,
        Stage5,
        Credits
    }

    public enum GameState
    {
        Gameplay,
        PausedGame,  
        Interaction,
        Cutscene,
        GameOver
    }

    protected void Awake()
    {
        instance = this;
    }

    protected void Start()
    {
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            levelState = LevelState.MainMenu;
        }
        else if (SceneManager.GetActiveScene().name == "Stage 1")
        {
            levelState = LevelState.Stage1;
        }
        else if (SceneManager.GetActiveScene().name == "Stage 2")
        {
            levelState = LevelState.Stage2;
        }
        else if (SceneManager.GetActiveScene().name == "Stage 3")
        {
            levelState = LevelState.Stage3;
        }
        else if (SceneManager.GetActiveScene().name == "Stage 4")
        {
            levelState = LevelState.Stage4;
        }
        else if (SceneManager.GetActiveScene().name == "Stage 5")
        {
            levelState = LevelState.Stage5;
        }
        else
        {
            levelState = LevelState.Credits;
        }
    }
}
