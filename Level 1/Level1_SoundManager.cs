using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1_SoundManager : SoundManager
{
    public static Level1_SoundManager soundManager1;

    public AudioSource ambienceSound;
    public AudioSource suspenseSound;
    public AudioSource momSFX;

    protected virtual void Awake()
    {
        soundManager1 = this;    
    }
}
