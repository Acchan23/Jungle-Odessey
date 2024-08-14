using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    public string itemName; // The name of the item
    public Sprite icon; // The icon representing the item in the UI
    public int quantity; // The quantity of the item
}