using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>(); // List of all inventory items

    // Method to add an item to the inventory
    public void AddItem(InventoryItem newItem)
    {
        InventoryItem existingItem = items.Find(item => item.itemName == newItem.itemName);

        if (existingItem != null)
        {
            existingItem.quantity += newItem.quantity; // Add to the quantity if the item already exists
        }
        else
        {
            items.Add(newItem); // Add the new item to the inventory
        }
    }

    // Method to remove an item from the inventory
    public void RemoveItem(string itemName, int quantity)
    {
        InventoryItem item = items.Find(i => i.itemName == itemName);

        if (item != null)
        {
            item.quantity -= quantity;
            if (item.quantity <= 0)
            {
                items.Remove(item); // Remove the item if quantity is zero or less
            }
        }
    }

    // Method to check if the inventory has a certain item with enough quantity
    public bool HasItem(string itemName, int quantity)
    {
        InventoryItem item = items.Find(i => i.itemName == itemName);
        return item != null && item.quantity >= quantity;
    }
}
