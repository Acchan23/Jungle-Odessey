using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventorySlot : MonoBehaviour
{
    public Image icon; // Image component to display the item's icon
    public Text quantityText; // Text component to display the item's quantity

    private InventoryItem currentItem; // The item currently in this slot

    public void AddItem(InventoryItem newItem)
    {
        currentItem = newItem;
        icon.sprite = currentItem.icon;
        icon.enabled = true;
        quantityText.text = currentItem.quantity.ToString();
    }

    public void ClearSlot()
    {
        currentItem = null;
        icon.sprite = null;
        icon.enabled = false;
        quantityText.text = "";
    }
}
