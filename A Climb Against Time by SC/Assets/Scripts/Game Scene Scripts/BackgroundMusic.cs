using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip firstTrack;
    public AudioClip secondTrack;
    private AudioSource audioSource;
    private bool isPlayingTrack1 = true;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayNextTrack();
    }

    void PlayNextTrack()
    {
        if(isPlayingTrack1)
        {
            audioSource.clip = firstTrack;
        }
        else
        {
            audioSource.clip = secondTrack;
        }
        
        audioSource.Play();
        isPlayingTrack1 = !isPlayingTrack1;

        float nextTrackDuration = audioSource.clip.length;
        Invoke("PlayNextTrack", nextTrackDuration);
    }

    
}
