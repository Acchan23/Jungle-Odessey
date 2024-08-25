using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    public Sprite lanceSprite;
    public Item[] items;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        InitializeSlots();
        AssignTextsToSlots();
    }

    public void EmptySlot(int numSlot, Image img)
    {
        items[numSlot].isOccupied = false;
        items[numSlot].amount = 0;
        img.color = new Color(img.color.r, img.color.g, img.color.b, 0.5f); // Opcional: cambia la transparencia para indicar que está vacío


        if (items[numSlot].amountText != null)
        {
            items[numSlot].amountText.text = "0";
        }
    }


    public void InitializeSlots()
    {
        items[0].type = ItemType.KNIFE;
        items[1].type = ItemType.WOOD;
        items[2].type = ItemType.lANCE;
        items[3].type = ItemType.MEAT;
        items[4].type = ItemType.CARAMBOLO;
        items[5].type = ItemType.MEDICINAL;
    }

    private void AssignTextsToSlots()
    {
        foreach (var item in items)
        {
            item.amountText = item.slotSprite.GetComponentInChildren<TextMeshProUGUI>();

            if (item.amountText != null)
            {
                item.amountText.text = "0";
            }
        }
    }
    //
    public bool HasEnoughWood()
    {
        foreach (var item in items)
        {
            if (item.type == ItemType.WOOD && item.isOccupied && item.amount == 3)
            {
                return true;
            }
        }
        return false;
    }

}

[System.Serializable]
public class Item
{
    public bool isOccupied;
    public int amount;
    public ItemType type;
    public GameObject slotSprite;
    public TextMeshProUGUI amountText;

}

public enum ItemType
{
    NONE,
    KNIFE,
    WOOD,
    lANCE,
    MEAT,
    CARAMBOLO,
    MEDICINAL
}
