using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Cache")]
    [SerializeField] private AudioSource source;

    [Header("Sounds")]
    public AudioClip computerStartupSound;
    public AudioClip ambienceStartSound;
    public AudioClip ambienceSound;
    public AudioClip ambienceEndSound;
    public AudioClip mouseClickSound;
    public AudioClip errorSound;
    public AudioClip beepSound;



    private void Awake()
    {
        if (Instance == null) { Instance = this; } else { Destroy(this); return; }
    }

    public void PlaySFX(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }

    public void StartAmbience()
    {
        source.PlayOneShot(ambienceStartSound);
        source.clip = ambienceSound;
        source.loop = true;
        source.PlayDelayed(ambienceStartSound.length);
    }

    public void EndAmbience()
    {
        source.Stop();
        source.PlayOneShot(ambienceEndSound);
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            source.PlayOneShot(mouseClickSound);
        }
    }
}
