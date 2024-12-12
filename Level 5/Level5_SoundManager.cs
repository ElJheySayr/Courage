using UnityEngine;

public class Level5_SoundManager : SoundManager
{
    public static Level5_SoundManager soundManager5;

    public AudioSource ambienceSound;

    protected virtual void Awake()
    {
        soundManager5 = this;
    }
}
