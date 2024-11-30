using UnityEngine;
using UnityEngine.UI;

public class InventoryManagement : MonoBehaviour
{
    public InventorySlot[] hotbar;
    public InventoryItem testing;

    public GameObject floorsalt_object;
    public Transform Player;

    public HealthSlider healthSlider;
    public int healthPackStrength = 50;

    private int currentSlotIndex = 0; 

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
                break;
            }
        }
    }

    private void DropItemInput()
    {
        if (Input.GetKeyDown(KeyCode.G)) // Check if the "G" key is pressed
        {
            RemoveItemFromHotBar();

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


    public void AddItemToHotBar(InventoryItem newItem)
    {
        // Check for an empty slot starting from the current index
        for (int i = 0; i < hotbar.Length; i++)
        {
            //int index = (currentSlotIndex + i) % hotbar.Length; // Cycle through slots
            if (hotbar[i].empty == true) // Find the first empty slot
            {
                hotbar[i].addItem(newItem);
                currentSlotIndex = i;
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
        //check if the type of item used is Salt
        if (usedItem.itemName == "Salt")
        {
            //get the player's position and change the y value to the specific number just above the floor
            Vector3 floorsalt_loc;
            floorsalt_loc = Player.transform.position;
            floorsalt_loc.y = -0.63f;
            //create a new instance of floor salt, and set it to be active (visible)
            GameObject fs = Instantiate(floorsalt_object, floorsalt_loc, transform.rotation);
            fs.SetActive(true);
        }
        if (usedItem.itemName == "Healthpack")
        {
            healthSlider.Heal(healthPackStrength);
            Debug.Log($"Healed player for {healthPackStrength}. Current health: {healthSlider.currentHealth}");
        }
    }
}
