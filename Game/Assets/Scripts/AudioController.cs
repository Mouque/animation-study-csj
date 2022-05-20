using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{      
    public AudioClip sfx;
    public AudioClip anotherSfx;
    public AudioClip bgm;
    private AudioSource audioSource;
    public static AudioController current;
    
    // Start is called before the first frame update
    void Start()
    {
        current = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
