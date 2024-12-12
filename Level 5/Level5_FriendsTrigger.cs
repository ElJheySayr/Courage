using System.Collections;
using NUnit.Framework.Interfaces;
using UnityEngine;

public class Level5_FriendsTrigger : MonoBehaviour
{
    private Transform playerTransform;
    public GameObject friendsCamera;

    protected virtual void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        if(Vector3.Distance(transform.position, playerTransform.position) < 20f && Level5_Manager.instance.objectiveList == Level5_Manager.ObjectiveList.FindYourFriends && !Level5_Manager.instance.foundFriends)
        {
            StartCoroutine(FriendsInteractions());
        }
    }

    private IEnumerator FriendsInteractions()
    {
        Level5_Manager.instance.playerIsSafe = true;
        friendsCamera.SetActive(true);
        Level5_Manager.instance.foundFriends = true;
        Level5_SoundManager.soundManager5.PlayMultipleDialogueWithFreeze(18,28);
        Level5_Manager.instance.signObjects[0].SetActive(false);

        while(Level5_SoundManager.soundManager5.dialoguePlayer.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        friendsCamera.SetActive(false);
        
        Level5_Manager.instance.objectiveList = Level5_Manager.ObjectiveList.FoodStalls;
        HUDController.instance.objectiveListText.text = "Go to the Food Stalls.";
        HUDController.instance.ObjectiveUpdated.SetActive(true);
        Level5_Manager.instance.PicnicCollider.SetActive(false);  
        Level5_Manager.instance.playerIsSafe = false;   
    }
}
