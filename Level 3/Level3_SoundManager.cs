using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level3_SoundManager : SoundManager
{
    public static Level3_SoundManager soundManager3;
    public AudioSource ambienceSound;

    protected virtual void Awake()
    {
        soundManager3 = this;    
    }
}
