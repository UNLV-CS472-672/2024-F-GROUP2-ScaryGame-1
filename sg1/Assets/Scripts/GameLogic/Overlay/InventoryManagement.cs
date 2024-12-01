using UnityEngine;
using UnityEngine.UI;

public class InventoryManagement : MonoBehaviour
{
    public InventorySlot[] hotbar;
    public InventoryItem testing;

    public GameObject floorsalt_object; // Existing object for floor salt
    public GameObject hand_salt;       // New object for salt in the player's hand
    public GameObject hand_health;     // New object for health pack in the player's hand
    public Transform Player;

    public HealthSlider healthSlider;
    public int healthPackStrength = 50;

    private int currentSlotIndex = 0;
    private GameObject currentlyEquippedObject; // Tracks the currently equipped object in hand

    // Clearing All Slots when Game Starts
    void Start()
    {
        ClearAllSlots();
        UpdateHotbarSelection();
    }

    void Update()
    {
        NumberInput();
        DropItemInput();
    }

    private void NumberInput()
    {
        // Check number keys 1-9 to select the corresponding slot in the hotbar
        for (int i = 1; i <= hotbar.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                currentSlotIndex = i - 1; // Map key 1 to slot 0, key 2 to slot 1, etc.
                UpdateHotbarSelection();
                UpdateEquippedObject(); // Update the displayed object
                break;
            }
        }
    }

    private void DropItemInput()
    {
        if (Input.GetKeyDown(KeyCode.G)) // Check if the "G" key is pressed
        {
            RemoveItemFromHotBar();
            UpdateEquippedObject(); // Update the displayed object
        }
    }

    private void UpdateHotbarSelection()
    {
        for (int i = 0; i < hotbar.Length; i++)
        {
            InventorySlot slot = hotbar[i];
            // Use color change to indicate selection
            slot.GetComponent<Image>().color = (i == currentSlotIndex) ? Color.red : Color.white;
        }
    }

    private void UpdateEquippedObject()
    {
        // Disable the currently equipped object
        if (currentlyEquippedObject != null)
        {
            currentlyEquippedObject.SetActive(false);
            currentlyEquippedObject = null;
        }

        // Check if there's an item in the currently selected slot
        if (!hotbar[currentSlotIndex].empty)
        {
            InventoryItem currentItem = hotbar[currentSlotIndex].getItem();
            if (currentItem.itemName == "Salt")
            {
                currentlyEquippedObject = hand_salt;
            }
            else if (currentItem.itemName == "Healthpack")
            {
                currentlyEquippedObject = hand_health;
            }

            // Enable the selected item prefab
            if (currentlyEquippedObject != null)
            {
                currentlyEquippedObject.SetActive(true);
            }
        }
    }

    public void AddItemToHotBar(InventoryItem newItem)
    {
        // Check for an empty slot starting from the current index
        for (int i = 0; i < hotbar.Length; i++)
        {
            if (hotbar[i].empty) // Find the first empty slot
            {
                hotbar[i].addItem(newItem);
                currentSlotIndex = i;
                UpdateEquippedObject(); // Update the displayed object
                return;
            }
        }

        // Optionally, you can handle the case where all slots are full
        Debug.Log("All slots are full!");
    }

    public void RemoveItemFromHotBar()
    {
        if (!hotbar[currentSlotIndex].empty)
        {
            UseItem(hotbar[currentSlotIndex].getItem());
            hotbar[currentSlotIndex].ClearSlot();
            UpdateEquippedObject(); // Update the displayed object
            if (currentSlotIndex != 0) currentSlotIndex -= 1;
            return;
        }
    }

    public void ClearAllSlots()
    {
        foreach (InventorySlot slot in hotbar)
        {
            slot.ClearSlot();
        }
    }

    public void UseItem(InventoryItem usedItem)
    {
        // Use the currently selected item
        if (usedItem.itemName == "Salt")
        {
            // Drop salt at player's feet
            Vector3 floorsalt_loc = Player.transform.position;
            floorsalt_loc.y = -0.63f;
            GameObject fs = Instantiate(floorsalt_object, floorsalt_loc, Quaternion.identity);
            fs.SetActive(true);

            // Play the salt shaker sound
            SoundManager.Instance?.PlaySaltShakerSound();
        }
        else if (usedItem.itemName == "Healthpack")
        {
            // Heal the player
            healthSlider.Heal(healthPackStrength);
            Debug.Log($"Healed player for {healthPackStrength}. Current health: {healthSlider.currentHealth}");

            // Play the health pack sound
            SoundManager.Instance?.PlayHealthpackSound();
        }

        // Clear the current prefab if the item is consumed
        if (usedItem.itemName == "Healthpack" || usedItem.itemName == "Salt")
        {
            if (currentlyEquippedObject != null)
            {
                currentlyEquippedObject.SetActive(false);
                currentlyEquippedObject = null;
            }
        }
    }
}
