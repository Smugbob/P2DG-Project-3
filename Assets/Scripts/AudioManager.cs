using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds; //array of stored sounds to be created as sources
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds) //loop for the amount of elements in the sounds array
        {
            s.source = gameObject.AddComponent<AudioSource>(); //create new audio source of the given name and add it as a component to AudioManager
            s.source.clip = s.clip; //add the requested clip as an attribute to the respective audio source

            s.source.volume = s.volume; //add the requested volume value as an attribute to the respective audio source
        }
    }

    // Update is called once per frame
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); //using name as the query input, find the respective audio source in the sounds array
        s.source.Play(); //play the source
    }
}
