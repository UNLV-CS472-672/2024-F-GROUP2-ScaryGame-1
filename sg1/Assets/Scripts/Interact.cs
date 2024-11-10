using UnityEngine;

public class Interactable : MonoBehaviour
{
    public InventoryItem item; // Reference to the item to be added to the inventory

    public float interactionDistance = 3f; // Distance within which you can interact
    public Camera playerCamera; // Reference to the player's camera

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

    public void Interact()
    {
        InventoryManagement inventoryManager = FindAnyObjectByType<InventoryManagement>();
        if (inventoryManager != null)
        {
            if (gameObject.activeSelf == true)
            {
                inventoryManager.AddItemToHotBar(item); // Add the item to the inventory
            }
            gameObject.SetActive(false);
            Destroy(gameObject); // Remove the item from the scene
        }
        else
        {
            Debug.LogWarning("InventoryManager not found!");
        }
    }
}
