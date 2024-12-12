using System.Collections;
using UnityEngine;

public class Level4_Triggers : MonoBehaviour
{
    private void OnTriggerEnter(Collider Actor)
    {
        if(gameObject.name == "Bathroom Trigger" && Actor.CompareTag("Player") && Level4_Manager.instance.objectiveList == Level4_Manager.ObjectiveList.GoToBathroom &&  !Level4_Manager.instance.goToTheBathroom)
        {
            Level4_Manager.instance.goToTheBathroom = true;
            Level4_Cutscenes.instance.PlaySecondCutscene();
        }
    }
}
