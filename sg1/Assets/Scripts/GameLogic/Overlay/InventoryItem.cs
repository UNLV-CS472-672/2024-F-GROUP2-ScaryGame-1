using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    // Name and Icon Properties for Inventory Item
    public string itemName;
    public Sprite icon;
}
