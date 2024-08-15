using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory; // Reference to the inventory
    public Transform itemsParent; // Parent object that holds the inventory slots
    public InventorySlot[] slots; // Array of inventory slots

    void Start()
    {
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        UpdateUI();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
