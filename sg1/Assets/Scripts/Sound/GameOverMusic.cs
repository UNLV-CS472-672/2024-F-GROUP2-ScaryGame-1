using UnityEngine;

public class GameOverMusic : MonoBehaviour
{
    public AudioClip gameOverClip;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = gameOverClip;
        audioSource.loop = true; // Loop the music
        audioSource.playOnAwake = false; // Don't play automatically
        audioSource.volume = AudioSettingsManager.MasterVolume; // Set volume based on master volume
        AudioSettingsManager.RegisterAudioSource(audioSource);
        audioSource.Play();
    }

    void OnDestroy()
    {
        AudioSettingsManager.UnregisterAudioSource(audioSource);
    }
}
