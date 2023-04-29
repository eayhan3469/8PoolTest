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

        //if (pitchOverride != 1)
        //    sfxAudioSource.pitch = pitchOverride;
        //else
        //    sfxAudioSource.pitch = 0.5f + Random.Range(-0.1f, 0.1f);

        sfxAudioSource.PlayOneShot(soundEffect, 1f);
    }
}
