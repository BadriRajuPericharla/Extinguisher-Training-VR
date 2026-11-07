using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip grabSfx;
    [SerializeField] private AudioClip pinDropSfx;
    [SerializeField] private AudioSource sfxSource;
    public static AudioManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayGrabSfx()
    {
        sfxSource.PlayOneShot(grabSfx);
    }
    public void PlayPinDropSfx()
    {
        sfxSource.PlayOneShot(pinDropSfx);
    }
}
