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
    // Start is called before the first frame update
    void Start()
    {
        inventory = PlayerInventory.Instance;
        sprite = GetComponent<SpriteRenderer>().sprite;    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            for (int i = 0; i < inventory.items.Length; i++)
            {
                if(inventory.items[i].isFull == false)
                {
                    Debug.Log("New Item");
                    inventory.items[i].isFull = true;
                    inventory.items[i].amount = 1;
                    inventory.items[i].type = type;
                    inventory.items[i].name = nameItem;
                    inventory.items[i].slotSprite.GetComponent<Image>().sprite = sprite;
                    inventory.items[i].slotSprite.GetComponent<Image>().enabled = true;
                    Destroy(gameObject);
                    break;
                }
               if (inventory.items[i].isFull == true && inventory.items[i].name == nameItem && inventory.items[i].amount < 64)
                {
                    Debug.Log("add other Item ");
                    inventory.items[i].amount +=1;
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
