using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Slots : MonoBehaviour
{
    private PlayerInventory inventory;
    private PlayerStats stats;
    public int numSlot;

    void Start()
    {
        inventory = PlayerInventory.Instance;
        stats = PlayerStats.instance;
    }
        
    public void UseItem()
    {
        if (inventory.items[numSlot].amount <= 0) return;

        ItemType itemType = inventory.items[numSlot].type;

        switch (itemType)
        {
            case ItemType.MEAT:
                stats.AddHungry(3);
                stats.AddThirst(1);
                break;
            case ItemType.FRUIT:
                stats.AddHungry(1);
                stats.AddThirst(3);
                break;
            case ItemType.MEDICINAL:
                stats.AddLife(1);
                break;
            default:
                Debug.Log("Unrecognized item");
                return;
        }

        inventory.items[numSlot].amount -= 1;
        if (inventory.items[numSlot].amount == 0)
        {
            inventory.EmptySlot(numSlot, GetComponent<Image>());
        }
    }
}
