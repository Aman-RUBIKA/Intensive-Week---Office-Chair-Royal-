using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playsound : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    void Start()
    {
        //SoundManager.Instance.PlayAudio(_clip);
    }
}
