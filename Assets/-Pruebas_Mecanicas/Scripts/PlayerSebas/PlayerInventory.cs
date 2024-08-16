using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    private void Awake()
    {
        Instance = this;
    }
    public Item[] items;
    public void EmptySlot(int numSlot, Image img)
    {
        items[numSlot].isOccupied = false;
        items[numSlot].amount = 1;
        items[numSlot].type = ItemType.NONE;
        img.sprite = null;
        img.enabled = false;
    }
}



public enum ItemType
{
    NONE,
    WEAPON,
    FOOD
}

[System.Serializable]
public class Item
{
    public bool isOccupied;
    public int amount;
    public ItemType type;
    public string name;
    public GameObject slotSprite;
}


