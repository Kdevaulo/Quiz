using System;
using UnityEngine;

public class SoundPlaying : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSources;
    public void PlaySound(int audioSource)
    {
        if (audioSources.Length != 0)
        {
            if (audioSource < audioSources.Length && audioSource >= 0)
            {
                audioSources[audioSource].Play();
            }
            else
            {
                throw new Exception("bad audioSource id");
            }
        }
        else
        {
            throw new Exception("audioSources == 0");
        }
    }
}
