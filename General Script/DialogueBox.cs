using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Scriptable Audio/New Dialogue")]
public class DialogueBox : ScriptableObject
{
    public AudioClip clip;
    public string subtitle;
}
