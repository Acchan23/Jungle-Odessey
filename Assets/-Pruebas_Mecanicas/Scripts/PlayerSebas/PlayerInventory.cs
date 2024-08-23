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
        items[numSlot].type = ItemType.NONE;
        img.sprite = null;
        img.enabled = false;

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
    public void WoodSearch()
    {
         foreach (var item in items)
        {
            if (item.type == ItemType.WOOD && item.isOccupied && item.amount == 3)
            {
                GameManager.Instance.Victory();
            }
        }
    }
    public void CheckForLanceActivation()
    {
        bool hasKnife = false;
        bool hasWood = false;

        // Verifica si el inventario tiene el cuchillo y la madera
        foreach (var item in items)
        {
            if (item.type == ItemType.KNIFE && item.isOccupied)
            {
                hasKnife = true;
            }
            if (item.type == ItemType.WOOD && item.isOccupied)
            {
                hasWood = true;
            }
        }

        // Si ambos están presentes, activa la lanza
        if (hasKnife && hasWood)
        {
            foreach (var item in items)
            {
                if (item.type == ItemType.lANCE)
                {
                    item.isOccupied = true;
                    // Aquí puedes actualizar la imagen del item si es necesario
                    var imageComponent = item.slotSprite.GetComponentInChildren<Image>();
                    imageComponent.sprite = lanceSprite;
                    imageComponent.gameObject.SetActive(true);
                    break;
                }
            }
        }
    }

}

[System.Serializable]
public class Item
{
    public bool isOccupied;
    public int amount;
    public ItemType type;
    public GameObject slotSprite;
    public TextMeshProUGUI amountText; // Campo para el texto del contador.
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
