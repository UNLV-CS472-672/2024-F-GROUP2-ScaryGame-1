using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;

    private InventoryItem currentItem;

    // Adding Item to Slot
    public void addItem(InventoryItem newItem)
    {
        currentItem = newItem;
        icon.sprite = newItem.icon;
        icon.enabled = true;
    }

    // Clearing Current Slot
    public void ClearSlot()
    {
        currentItem = null;
        icon.sprite = null;
        icon.enabled = false;
    }
}
