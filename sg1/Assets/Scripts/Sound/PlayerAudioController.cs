using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private AudioSource audioSource;  // Reference to the AudioSource
    public float delayInSeconds = 2f; // Delay before the audio starts

    void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();

        // Set initial volume based on master volume
        audioSource.volume = AudioSettingsManager.MasterVolume;

        // Delay the playback of the audio
        Invoke(nameof(PlayVoiceLine), delayInSeconds);
    }

    void Update()
    {
        // Continuously adjust the volume to match the master volume
        if (audioSource != null)
        {
            audioSource.volume = AudioSettingsManager.MasterVolume;
        }
    }

    void PlayVoiceLine()
    {
        // Play the audio clip if it's assigned to the AudioSource
        if (audioSource.clip != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogError("No audio clip assigned to the AudioSource!");
        }
    }
}
