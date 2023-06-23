using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager_LV2 : MonoBehaviour
{
    public Sound[] sounds;

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            Debug.Log(s.name);
            s.source = gameObject.AddComponent<AudioSource>();


            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
        }

        // Play("Suspense");
    }


    //Apply sound of your choice on any event you want by calling this method and give name with which you added sound in Sounds array.\

    // you can call this method by the syntax => [' FindObjectOfType<AudioManager>().Play("[name of sound]"); ']
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound not found");
            return;
        }
        s.source.Play();
    }
}
