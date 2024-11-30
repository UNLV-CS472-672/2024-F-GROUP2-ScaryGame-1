using UnityEngine;

public static class AudioSettingsManager
{
    private static float _masterVolume = 1.0f; // Default volume

    public static float MasterVolume
    {
        get => _masterVolume;
        set
        {
            _masterVolume = Mathf.Clamp01(value); // Ensure the volume stays between 0.0f and 1.0f
        }
    }
}
