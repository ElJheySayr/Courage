using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level2_MiniMapChanger : MonoBehaviour
{
    public Sprite[] miniMaps;
    public Image miniMapImage;

    public virtual void OnTriggerEnter(Collider Actor)
    {
        if(Actor.CompareTag("Player"))
        {
            miniMapImage.sprite = miniMaps[0];
        }
    }

    public virtual void OnTriggerExit(Collider Actor)
    {
        if(Actor.CompareTag("Player"))
        {
            miniMapImage.sprite = miniMaps[1];
        }
    }
}
