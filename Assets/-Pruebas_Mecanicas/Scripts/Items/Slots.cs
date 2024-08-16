using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Slots : MonoBehaviour
{
    PlayerInventory inventory;
    PlayerStats stats;
    public int numSlot;
    // Start is called before the first frame update
    void Start()
    {
        inventory = PlayerInventory.Instance;
        stats = PlayerStats.instance;
    }

    public void UseItem()
    {
        Debug.Log("Here" + inventory.items[numSlot].name);
        if (stats.lifeCur < 10 && inventory.items[numSlot].type == ItemType.FOOD && inventory.items[numSlot].amount == 1)
        {
            stats.AddHungry(3);
            inventory.EmptySlot(numSlot, GetComponent<Image>());
        }
        if(stats.lifeCur < 10 && inventory.items[numSlot].type == ItemType.FOOD && inventory.items[numSlot].amount > 1)
        {
            stats.AddHungry(3);
            inventory.items[numSlot].amount -= 1;
        }
        else
        {
            Debug.Log("Not Function");
        }
    }
}
