using UnityEngine;

public class Movement : MonoBehaviour
{
    private const float GRAVITY = 9.8f;
    private const float SPEED = 1.5f;
    private const float JUMPFORCE = 3f;
    private const float PITCH_SENS = 1.5f;
    private const float YAW_SENS = 2.5f;

    //These fields will be visible in the Unity Editor, for selection from the user
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private CharacterController PlayerCharacterController;

    float jump_height = 0f;
    float pitch_degrees = 0f, yaw_degrees = 0f;


    //Called when player is initialized
    void Start()
    {
        //Disable cursor
        Cursor.lockState = CursorLockMode.Locked; 
    }

    //Called once per frame
    void Update()
    {
        PlayerMove();
        CameraRotate();
    }
    private void PlayerMove()
    {
        Vector3 vec3_move = transform.TransformDirection(
            Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));


        if (PlayerCharacterController.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jump_height = JUMPFORCE;
            }
            else
            {
                jump_height = -0.1f;
            }
        }
        else
        {
            jump_height += GRAVITY * -2f * Time.deltaTime;
        }
        
        vec3_move.y += jump_height;
        PlayerCharacterController.Move(vec3_move * SPEED * Time.deltaTime);
    }
    private void CameraRotate()
    {
        pitch_degrees -= Input.GetAxis("Mouse Y") * PITCH_SENS;
        yaw_degrees += Input.GetAxis("Mouse X") * YAW_SENS;

        pitch_degrees = Mathf.Clamp(pitch_degrees, -70f, 70f);

        PlayerCamera.transform.localRotation = Quaternion.Euler(pitch_degrees, yaw_degrees, 0f);
    }
}
