using System.Collections.Generic;
using UnityEngine;

public static class AudioSettingsManager
{
    private static float _masterVolume = 1.0f; // Default volume
    private static readonly List<AudioSource> activeAudioSources = new List<AudioSource>();

    public static float MasterVolume
    {
        get => _masterVolume;
        set
        {
            _masterVolume = Mathf.Clamp01(value); // Ensure that the volume stays between 0.0f and 1.0f
            UpdateAllAudioSources();
        }
    }

    public static void RegisterAudioSource(AudioSource audioSource)
    {
        if (!activeAudioSources.Contains(audioSource))
        {
            activeAudioSources.Add(audioSource);
        }
    }

    public static void UnregisterAudioSource(AudioSource audioSource)
    {
        activeAudioSources.Remove(audioSource);
    }

    private static void UpdateAllAudioSources()
    {
        foreach (var source in activeAudioSources)
        {
            if (source != null)
            {
                source.volume = _masterVolume;
            }
        }
    }
}
