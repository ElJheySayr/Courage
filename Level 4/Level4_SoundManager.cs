using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4_SoundManager : SoundManager
{
    public static Level4_SoundManager soundManager4;

    public AudioSource ambienceSound;
    public AudioSource anxietySound;

    protected virtual void Awake()
    {
        soundManager4 = this;    
    }
}
