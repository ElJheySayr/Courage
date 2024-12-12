using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1_Exit : MonoBehaviour
{
    public virtual void OnTriggerEnter(Collider Actor)
    {
        if (Actor.CompareTag("Player") && Level1_Manager.instance.objectiveList == Level1_Manager.ObjectiveList.End)
        {
            HUDController.instance.NewLevel();
        }
    }
}
