using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource sfxAudioSource;

    public void PlaySoundEffect(AudioClip soundEffect, float pitchOverride = 1)
    {
        if (soundEffect == null)
            return;

        sfxAudioSource.PlayOneShot(soundEffect, 1f);
    }
}
