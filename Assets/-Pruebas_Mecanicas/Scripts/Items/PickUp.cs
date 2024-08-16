using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PickUp : MonoBehaviour
{
    PlayerInventory inventory;
    private Sprite sprite;
    public ItemType type;
    public string nameItem;
    private readonly int maxItems = 5;
    // Start is called before the first frame update
    void Start()
    {
        inventory = PlayerInventory.Instance;
        sprite = GetComponent<SpriteRenderer>().sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (Item item in inventory.items)
            {
                if (item.name == nameItem && item.amount == maxItems) return;
            }
           
            for (int i = 0; i < inventory.items.Length; i++)
            {
                if (!inventory.items[i].isOccupied)
                {
                    Debug.Log("New Item");
                    inventory.items[i].isOccupied = true;
                    inventory.items[i].amount = 1;
                    inventory.items[i].type = type;
                    inventory.items[i].name = nameItem;
                    inventory.items[i].slotSprite.GetComponent<Image>().sprite = sprite;
                    inventory.items[i].slotSprite.GetComponent<Image>().enabled = true;
                    gameObject.SetActive(false);
                    break;
                }
                if (inventory.items[i].isOccupied == true && inventory.items[i].name == nameItem && inventory.items[i].amount < maxItems)
                {
                    Debug.Log("add other Item ");
                    inventory.items[i].amount += 1;
                    gameObject.SetActive(false);
                    break;
                }
            }
        }
    }
}
