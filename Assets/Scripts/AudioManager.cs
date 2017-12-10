using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    public static AudioManager instance;

    public AudioClip putClip;

    public AudioClip fireClip;

    public AudioClip fuelClip;

    public AudioClip bombClip;
    
    public AudioClip trapClip;

    public AudioClip treasureClip;
    
    public AudioClip winClip;

    public AudioClip loseClip;

    private void Awake()
    {
        instance = this;
    }
}
