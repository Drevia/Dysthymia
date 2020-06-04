using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;


public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public Sound[] sounds;
    

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;


        foreach (Sound s in sounds)
        {
            GameObject newSound = new GameObject(s.name);
            newSound.transform.SetParent(this.transform);
            newSound.transform.localPosition = Vector3.zero;

            s.source = newSound.AddComponent<AudioSource>();

            s.source.clip = s.clip[UnityEngine.Random.Range(0, s.clip.Length)];
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
           
            
        }


        Play("Vide");
        Play("Ambiance");

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {

            Play("Vide");
            Play("Ambiance");
        }
    }

    public void Play(string name)
    {
        Debug.Log("Play sound :" + name);
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s.clip.Length > 1)
            s.source.clip = s.clip[UnityEngine.Random.Range(0, s.clip.Length)];

        s.source.Play();
    }
}
