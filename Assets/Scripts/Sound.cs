using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound //Sound class for storing sound clips and related attributes
{
    public string name; //name of the audio clip

    public AudioClip clip; //created audio clip

    [Range(0f, 1f)]
    public float volume = 0.5f; //volume slider between 0x and 1x

    [HideInInspector]
    public AudioSource source; //created audio source, does not need to be serialized since this will be assigned automatically by AudioManager
}
