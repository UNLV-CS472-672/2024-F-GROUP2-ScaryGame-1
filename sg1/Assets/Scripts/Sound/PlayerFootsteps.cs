using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioSource footstepSource;  // Reference to the AudioSource component
    public AudioClip[] footstepClips;   // Array to hold multiple footstep sounds
    public float stepInterval = 0.5f;   // Time between steps

    private float stepTimer;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        stepTimer = stepInterval;
        AudioSettingsManager.RegisterAudioSource(footstepSource);
    }

    void OnDestroy()
    {
        AudioSettingsManager.UnregisterAudioSource(footstepSource);
    }

    void Update()
    {
        // Check if the player is moving
        if (characterController.isGrounded && characterController.velocity.magnitude > 0)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0)
            {
                PlayFootstep();
                stepTimer = stepInterval;
            }
        }
    }

    void PlayFootstep()
    {
        if (footstepClips.Length > 0)
        {
            // Select a random clip from the array
            AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
            
            // Apply master volume
            footstepSource.volume = AudioSettingsManager.MasterVolume;
            footstepSource.PlayOneShot(clip);
        }
    }
}
