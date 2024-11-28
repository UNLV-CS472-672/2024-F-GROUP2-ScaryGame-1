using UnityEngine;

public class MinigameInteraction : MonoBehaviour
{
    public GameObject miniGameCanvas; // Reference to the canvas with the 2D game
    public KeyCode interactionKey = KeyCode.E; // Key to open/close the mini-game
    public Movement playerMovement; // Reference to the FirstPersonController script for player movement

    private bool isPlayerNearby = false; // Tracks if the player is close to the object
    private bool isMinigameOpen = false; // Tracks if the mini-game is currently open

    public HelpInfo helpInfo; // used to display help message

    void Start()
    {
        MinigameManager.miniGamePositions.Add(this.transform.position);
    }

    void Update()
    {
        // Check for input only if the player is nearby and mini-game is closed
        if (isPlayerNearby && Input.GetKeyDown(interactionKey) && !isMinigameOpen)
        {
            OpenMinigame();
        }
        else if (isMinigameOpen)
        {
            // Check if the player presses E or any movement keys while the mini-game is open
            if (Input.GetKeyDown(interactionKey) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
                Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                CloseMinigame();
            }
        }

        if(isPlayerNearby) helpInfo.ShowMessage("Press E to open minigame", 0.1f);
    }

    // Called when the player enters the interaction zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true; // Set flag when player is close
        }
    }

    // Called when the player leaves the interaction zone
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false; // Unset flag when player moves away
        }
    }

    // Activates the 2D game canvas, disables player movement, and frees the mouse
    private void OpenMinigame()
    {
        miniGameCanvas.SetActive(true);              // Show mini-game canvas
        //playerMovement.canMove = false;              // Disable player movement script
        playerMovement.canLookAround = false;
        isMinigameOpen = true;                       // Set mini-game open flag
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Method to close the mini-game, re-enable player movement, and lock the mouse again
    private void CloseMinigame()
    {
        miniGameCanvas.SetActive(false);             // Hide mini-game canvas
        //playerMovement.canMove = true;               // Re-enable player movement
        playerMovement.canLookAround = true;
        isMinigameOpen = false;                      // Reset mini-game open flag
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
