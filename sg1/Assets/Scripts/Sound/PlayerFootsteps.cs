using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] footstepSounds; // Array to hold different footstep clips
    public float walkStepInterval = 0.5f; // Time interval between footsteps when walking
    public float sprintStepInterval = 0.28f; // Time interval between footsteps when sprinting
    public KeyCode sprintKey = KeyCode.LeftShift;

    private float stepTimer = 0f;
    private bool isMoving = false; // To track if the player is moving
    private bool isSprinting = false;
    private bool isGettingOutOfBed = true; // Start in the "getting out of bed" state
    public float gettingOutOfBedDuration = 3f; // Duration of the "getting out of bed" state
    private float bedTimer = 0f; // Timer for the "getting out of bed" state

    void Update()
    {
        if (isGettingOutOfBed)
        {
            // Handle the "getting out of bed" timer
            bedTimer += Time.deltaTime;
            if (bedTimer >= gettingOutOfBedDuration)
            {
                isGettingOutOfBed = false; // Transition to normal movement
            }
            return; // Skip footstep logic while getting out of bed
        }

        isSprinting = Input.GetKey(sprintKey); 
        float currentStepInterval = isSprinting ? sprintStepInterval : walkStepInterval;
        audioSource.volume = AudioSettingsManager.MasterVolume;

        isMoving = IsPlayerMoving(); // Check if the player is moving

        if (isMoving)
        {
            stepTimer += Time.deltaTime;

            if (stepTimer >= currentStepInterval)
            {
                PlayFootstepSound();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f; // Reset timer immediately when movement stops
        }
    }

    private bool IsPlayerMoving()
    {
        return Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.1f;
    }

    private void PlayFootstepSound()
    {
        if (footstepSounds.Length > 0)
        {
            // Play a random footstep sound
            int index = Random.Range(0, footstepSounds.Length);
            audioSource.PlayOneShot(footstepSounds[index]);
        }
    }

    public void FinishGettingOutOfBed()
    {
        isGettingOutOfBed = false;
    }
}
