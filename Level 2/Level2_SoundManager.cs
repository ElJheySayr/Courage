using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level2_SoundManager : SoundManager
{
    public static Level2_SoundManager soundManager2;

    public AudioSource ambienceSound;

    public AudioSource studentAmbienceSound;

    protected virtual void Awake()
    {
        soundManager2 = this;   
    }
}