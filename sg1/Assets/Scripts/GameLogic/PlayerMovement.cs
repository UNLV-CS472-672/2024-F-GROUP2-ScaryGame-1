using UnityEngine;
using System;

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

    //These fields will be visible in the Unity Editor, for selection from the user
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private CharacterController PlayerCharacterController;


    //Called when player is initialized
    void Start()
    {
        // Load sensitivity settings
        UpdateSensitivitySettings();

        ////////////////////// Initialize camera rotation
        Vector3 initialEuler = PlayerCamera.transform.localRotation.eulerAngles;
        pitch_degrees = initialEuler.x;
        yaw_degrees = initialEuler.y;
        targetRotation = Quaternion.Euler(pitch_degrees, yaw_degrees, 0f);
        /////////////////////

        //Disable cursor
        Cursor.lockState = CursorLockMode.Locked; 
    }

    //Called once per frame
    void Update()
    {
        if (LockMovement == false)
        {
            PlayerMove();
            CameraRotate();
        }
    }

    public void UpdateSensitivitySettings()
    {
        PITCH_SENS = PlayerPrefs.GetFloat("PitchSensitivity", PITCH_SENS);
        YAW_SENS = PlayerPrefs.GetFloat("YawSensitivity", YAW_SENS);
    }

    private void PlayerMove()
    {

        Vector3 vec3_move = transform.TransformDirection(
            Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));


        //check that the player is on the ground
        if (PlayerCharacterController.isGrounded)
        {
            //check if the spacebar is pressed
            if (Input.GetKey(KeyCode.Space))
            {
                //check that at least JUMPDELAY seconds have passed since the last jump
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

            //Handle player sprinting
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                target_speed = SPRINTSPEED;
            }
            else
            {
                target_speed = SPEED;
            }
        }

        //Decrease the player's height while they are in air (gravity)
        else
        {
            jump_height += GRAVITY * -1.2f * Time.deltaTime;
        }

        //Modify the current speed value if the target speed is much different (player is or is not sprinting)
        if (Math.Abs(cur_speed - target_speed) > 0.05)
        {
            if (cur_speed < target_speed)
            {
                //Acceleration
                cur_speed += (1f * Time.deltaTime);
            }
            else
            {
                //Deceleration (More Rapid)
                cur_speed -= (1.5f * Time.deltaTime);
            }
        }
        
        vec3_move.y += jump_height;
        PlayerCharacterController.Move(vec3_move * cur_speed * Time.deltaTime);
    }

    //Handles the rotation of the camera (direction the player is looking)
    private void CameraRotate()
    {
        if (canLookAround)
        {
            pitch_degrees -= Input.GetAxis("Mouse Y") * PITCH_SENS;
            yaw_degrees += Input.GetAxis("Mouse X") * YAW_SENS;

            pitch_degrees = Mathf.Clamp(pitch_degrees, -70f, 70f);

            targetRotation = Quaternion.Euler(pitch_degrees, yaw_degrees, 0f);
        }

        // Always apply the latest target rotation, which only updates if canLookAround is true
        PlayerCamera.transform.localRotation = targetRotation;
    }


}