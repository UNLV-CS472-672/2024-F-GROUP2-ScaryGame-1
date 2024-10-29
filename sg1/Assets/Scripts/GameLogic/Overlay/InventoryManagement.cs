using UnityEngine;

public class InventoryManagement : MonoBehaviour
{
    public InventorySlot[] hotbar;
    public InventoryItem testing;

    private int currentSlotIndex = 0; 

    // Clearing All Slots when Game Starts
    void Start()
    {
        ClearAllSlots();
    }


    public void AddItemToHotBar(InventoryItem newItem)
    {
        // Check for an empty slot starting from the current index
        for (int i = 0; i < hotbar.Length; i++)
        {
            int index = (currentSlotIndex + i) % hotbar.Length; // Cycle through slots
            if (hotbar[index].icon.sprite == null) // Find the first empty slot
            {
                hotbar[index].addItem(newItem);
                currentSlotIndex = (index + 1) % hotbar.Length; // Move to the next slot for the next addition
                return;
            }
        }

        // Optionally, you can handle the case where all slots are full
        Debug.Log("All slots are full!");
    }

    public void ClearAllSlots()
    {
        foreach (InventorySlot slot in hotbar)
        {
            slot.ClearSlot();
        }
    }
}
