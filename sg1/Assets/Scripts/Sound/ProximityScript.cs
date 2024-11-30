using UnityEngine;

public class AntagonistProximityAlert : MonoBehaviour
{
    public AudioSource constantSound; // Constant ghost sound
    public AudioSource proximitySound; // Proximity ghost sound
    public Transform player; // Player reference
    public float alertDistance = 7f; // Distance threshold for proximity sound

    private bool isPlayerClose = false; // Track if player is within alert range

    void Start()
    {
        // Ensure the constant sound starts playing
        if (!constantSound.isPlaying)
        {
            AudioSettingsManager.RegisterAudioSource(constantSound);
            constantSound.Play();
        }
    }

    void OnDestroy()
    {
        AudioSettingsManager.UnregisterAudioSource(constantSound);
        AudioSettingsManager.UnregisterAudioSource(proximitySound);
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Adjust volumes based on master volume
        constantSound.volume = AudioSettingsManager.MasterVolume;
        proximitySound.volume = AudioSettingsManager.MasterVolume;

        // Check if the player is within alert distance
        if (distanceToPlayer <= alertDistance && !isPlayerClose)
        {
            // Stop the ghost sound and play the proximity alert sound
            constantSound.Stop();

            AudioSettingsManager.RegisterAudioSource(proximitySound);
            proximitySound.Play();
            isPlayerClose = true; // Mark player as close
        }
        else if (distanceToPlayer > alertDistance && isPlayerClose)
        {
            // Restart the ghost sound and stop the proximity alert if it was playing
            if (!constantSound.isPlaying)
            {
                constantSound.Play();
            }
            proximitySound.Stop();
            isPlayerClose = false; // Mark player as not close
        }
    }

}
