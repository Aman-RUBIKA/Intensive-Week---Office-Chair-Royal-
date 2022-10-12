using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    
    public PlaySound[] clip;

    private void Start()
    {
        Play("Theme");
        Debug.Log("playing song");
    }

    private void Awake()
    {
        foreach (PlaySound s in clip)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }   
  
    }

    public void Play (string name)
    {
        PlaySound s = Array.Find(clip, PlaySound  => PlaySound.name == name);
        s.source.Play();
    } 
    public void PlayOneShot (string name)
    {
        PlaySound s = Array.Find(clip, PlaySound  => PlaySound.name == name);
        s.source.PlayOneShot(s.clip, s.volume);
    }



}
