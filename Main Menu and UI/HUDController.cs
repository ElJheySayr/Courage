using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static GameManager;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{
    public static HUDController instance;

    public GameObject GameOverPanel;
    public TMP_Text dialogueText;
    public TMP_Text objectiveListText;
    public TMP_Text interactionText;
    public GameObject reticle;
    public GameObject newLevelImage;

    public GameObject SavedGameImage;
    public GameObject ObjectiveUpdated;

    protected virtual void Awake()
    {
        instance = this;
    }

    protected virtual void Update()
    {
        if (GameManager.instance.gameState == GameState.PausedGame || GameManager.instance.gameState == GameState.Interaction || GameManager.instance.gameState == GameState.GameOver || GameManager.instance.gameState == GameState.Cutscene)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public virtual void SetSubtitle(string subtitle, float delay)
    {
        dialogueText.text = subtitle;
        StartCoroutine(ClearAfterSeconds(delay));
    }

    public virtual void ClearSubtitle()
    {
        dialogueText.text = string.Empty;
    }

    public virtual IEnumerator ClearAfterSeconds(float delay)
    {
        yield return new WaitForSeconds(delay);
        ClearSubtitle();
    }

    public virtual void EnableInteractionText(string text)
    {
        interactionText.text = "Press [F] to Interact with " + text;
        interactionText.gameObject.SetActive(true);
        reticle.SetActive(false);
    }

    public virtual void DisableInteractionText() 
    {
        interactionText.gameObject.SetActive(false);

        if(GameManager.instance.gameState == GameState.Gameplay)
        {
            reticle.SetActive(true);
        }
        else
        {
            reticle.SetActive(false);
        }
    }

    public virtual void NewLevel()
    {
        newLevelImage.SetActive(true);
        GameManager.instance.gameState = GameState.Interaction;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
