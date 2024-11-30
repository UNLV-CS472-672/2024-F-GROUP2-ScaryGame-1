using UnityEngine;
using System;
using System.Collections;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("PlayMode")]
public class Movement : MonoBehaviour
{
    private const float GRAVITY = 9.8f;
    private const float SPEED = 1.5f;
    private const float SPRINTSPEED = 2.5f;
    private const float JUMPFORCE = 2.5f;
    private const float JUMPDELAY = 0.55f;

    private float jump_height = 0f;
    private float last_jump_time = 0;
    private float cur_speed = SPEED, target_speed = 0f;
    private float pitch_degrees = 0f, yaw_degrees = 0f;

    static public bool LockMovement = false;

    public float PITCH_SENS = 1.5f;
    public float YAW_SENS = 1.5f;

    public bool canLookAround = true;
    private Quaternion targetRotation;

    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private CharacterController PlayerCharacterController;

    // Wake-up sequence variables
    private bool isWakingUp = true; // Tracks if the player is waking up
    private Quaternion wakeUpStartRotation;
    private Quaternion wakeUpEndRotation;
    private float wakeUpDuration = 2f; // Duration of the wake-up transition in seconds

    internal IInput MyInput = new InputWrapper();

    void Start()
    {
        // Load sensitivity settings
        UpdateSensitivitySettings();

        ////////////////////// Initialize camera rotation for the wake-up sequence
        wakeUpStartRotation = Quaternion.Euler(15.045f, -373.98f, 295.89f); // Sideways orientation
        wakeUpEndRotation = Quaternion.Euler(0f, -360f, 360f);     // Upright orientation
        PlayerCamera.transform.localRotation = wakeUpStartRotation;
        /////////////////////

        // Disable cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Check if the player presses W to start the wake-up sequence
        if (isWakingUp && (MyInput.GetKeyDown(KeyCode.W) || MyInput.GetKeyDown(KeyCode.A) || MyInput.GetKeyDown(KeyCode.S) || MyInput.GetKeyDown(KeyCode.D)))
        {
            StartCoroutine(WakeUpFromBed());
        }

        // Disable movement and camera rotation during wake-up
        if (!isWakingUp && !LockMovement)
        {
            PlayerMove();
            CameraRotate();
        }
    }

    IEnumerator WakeUpFromBed()
    {
        float elapsedTime = 0f;
        while (elapsedTime < wakeUpDuration)
        {
            elapsedTime += Time.deltaTime;
            PlayerCamera.transform.localRotation = Quaternion.Slerp(wakeUpStartRotation, wakeUpEndRotation, elapsedTime / wakeUpDuration);
            yield return null;
        }
        PlayerCamera.transform.localRotation = wakeUpEndRotation;
        isWakingUp = false;

    }

    public void UpdateSensitivitySettings()
    {
        PITCH_SENS = PlayerPrefs.GetFloat("PitchSensitivity", PITCH_SENS);
        YAW_SENS = PlayerPrefs.GetFloat("YawSensitivity", YAW_SENS);
    }

    private void PlayerMove()
    {
        Vector3 vec3_move = transform.TransformDirection(
            MyInput.GetAxis("Horizontal"), 0f, MyInput.GetAxis("Vertical"));

        // Check that the player is on the ground
        if (PlayerCharacterController.isGrounded)
        {
            // Check if the spacebar is pressed
            if (MyInput.GetKey(KeyCode.Space))
            {
                // Check that at least JUMPDELAY seconds have passed since the last jump
                if ((Time.realtimeSinceStartup - last_jump_time) > JUMPDELAY)
                {
                    jump_height = JUMPFORCE;
                    last_jump_time = Time.realtimeSinceStartup;
                }
            }
            else
            {
                jump_height = -0.1f;
            }

            // Handle player sprinting
            if (MyInput.GetKey(KeyCode.LeftShift) || MyInput.GetKey(KeyCode.RightShift))
            {
                target_speed = SPRINTSPEED;
            }
            else
            {
                target_speed = SPEED;
            }
        }
        // Decrease the player's height while they are in the air (gravity)
        else
        {
            jump_height += GRAVITY * -1.2f * Time.deltaTime;
        }

        // Modify the current speed value if the target speed is much different (player is or is not sprinting)
        if (Math.Abs(cur_speed - target_speed) > 0.05)
        {
            if (cur_speed < target_speed)
            {
                // Acceleration
                cur_speed += (1f * Time.deltaTime);
            }
            else
            {
                // Deceleration (More Rapid)
                cur_speed -= (1.5f * Time.deltaTime);
            }
        }

        vec3_move.y += jump_height;
        PlayerCharacterController.Move(vec3_move * cur_speed * Time.deltaTime);
    }

    // Handles the rotation of the camera (direction the player is looking)
    private void CameraRotate()
    {
        if (canLookAround)
        {
            pitch_degrees -= MyInput.GetAxis("Mouse Y") * PITCH_SENS;
            yaw_degrees += MyInput.GetAxis("Mouse X") * YAW_SENS;

            pitch_degrees = Mathf.Clamp(pitch_degrees, -70f, 70f);

            targetRotation = Quaternion.Euler(pitch_degrees, yaw_degrees, 0f);
        }

        // Always apply the latest target rotation, which only updates if canLookAround is true
        PlayerCamera.transform.localRotation = targetRotation;
    }
}
