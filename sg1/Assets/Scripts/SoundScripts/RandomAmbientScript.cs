using UnityEngine;

public class RandomAmbientSound : MonoBehaviour
{
    public AudioSource audioSource; // The AudioSource component to play ambient sounds
    public AudioClip[] ambientClips; // Array to hold the ambient audio clips
    public float minDelay = 5f; // Minimum delay between sounds
    public float maxDelay = 15f; // Maximum delay between sounds
    public float minVolume = 0.5f; // Minimum volume for each clip
    public float maxVolume = 1.0f; // Maximum volume for each clip

    private float delayTimer;

    void Start()
    {
        PlayRandomAmbientClip();
    }

    void Update()
    {
        delayTimer -= Time.deltaTime;

        if (!audioSource.isPlaying && delayTimer <= 0f)
        {
            PlayRandomAmbientClip();
        }
    }

    void PlayRandomAmbientClip()
    {
        if (ambientClips.Length > 0)
        {
            // Pick a random clip from the array
            audioSource.clip = ambientClips[Random.Range(0, ambientClips.Length)];

            // Set a random volume within the specified range
            audioSource.volume = Random.Range(minVolume, maxVolume);

            audioSource.Play();

            delayTimer = Random.Range(minDelay, maxDelay);
        }
    }
}
