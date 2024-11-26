using UnityEngine;

public class Interacter : MonoBehaviour
{
    public float interactionDistance = 3f; // Distance within which you can interact
    public Camera playerCamera; // Reference to the player's camera

    private void Start()
    {
        playerCamera = GetComponent<Camera>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) // Check if the "F" key is pressed
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    interactable.Interact(); // Call the interact method on the hit interactable
                }
            }
        }
    }

}
