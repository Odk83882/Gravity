using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField]
    AudioSource SFXsource;

    public AudioClip click;
    public AudioClip hitHurt;
    public AudioClip jump;
    public AudioClip laserShoot;
    public AudioClip powerUp;
    public AudioClip dash;

    // Adjust this method to manage the AudioClip lifecycle if necessary
    public void PlaySFX(AudioClip clip)
    {
        // Stop any currently playing clip
        SFXsource.Stop();

        // Assign the new clip to the AudioSource
        SFXsource.clip = clip;

        // Play the assigned clip
        SFXsource.Play();
    }
}
