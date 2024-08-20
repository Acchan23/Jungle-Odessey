using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    PlayerInventory inventory;
    private Sprite sprite;
    public ItemType type;
    private readonly int maxItems = 5;
    public GameObject interactionPrompt;
    private bool isPlayerNear = false;
    public GameObject optionsMenu;

    // Referencia estática a la instancia actual
    public static PickUp currentPickUp;

    void Start()
    {
        inventory = PlayerInventory.Instance;
        sprite = GetComponent<SpriteRenderer>().sprite;
        interactionPrompt.SetActive(false);
        optionsMenu.SetActive(false);
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            OpenOptionsMenu();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isPlayerNear = true;
            interactionPrompt.SetActive(true);
            currentPickUp = this; // Actualizamos la referencia estática
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            isPlayerNear = false;
            interactionPrompt.SetActive(false);
            optionsMenu.SetActive(false);
            if (currentPickUp == this) currentPickUp = null; // Limpiamos la referencia estática
        }
    }

    private void OpenOptionsMenu()
    {
        Time.timeScale = 0f; // Pausar el juego si es necesario
        interactionPrompt.SetActive(false);
        optionsMenu.SetActive(true);
        

        // Actualizamos los botones del menú de opciones
        Button takeButton = optionsMenu.transform.Find("TakeButton").GetComponent<Button>();
        takeButton.onClick.RemoveAllListeners();
        takeButton.onClick.AddListener(TakeItem);

        Button eatButton = optionsMenu.transform.Find("EatButton").GetComponent<Button>();
        eatButton.onClick.RemoveAllListeners();
        eatButton.onClick.AddListener(EatItem);

        Button leaveButton = optionsMenu.transform.Find("LeaveButton").GetComponent<Button>();
        leaveButton.onClick.RemoveAllListeners();
        leaveButton.onClick.AddListener(LeaveItem);
    }

    public void CloseOptionsMenu()
    {
        Time.timeScale = 1f; // Reanudar el juego si estaba pausado
        optionsMenu.SetActive(false);
    }

    public void TakeItem()
    {
        Debug.Log("TakeItem called");
        for (int i = 0; i < inventory.items.Length; i++)
        {
            if (!inventory.items[i].isOccupied)
            {
                inventory.items[i].isOccupied = true;
                inventory.items[i].amount = 1;
                inventory.items[i].type = type;
                inventory.items[i].slotSprite.GetComponent<Image>().sprite = sprite;
                inventory.items[i].slotSprite.GetComponent<Image>().enabled = true;
                gameObject.SetActive(false);
                CloseOptionsMenu();
                break;
            }
            if (inventory.items[i].isOccupied && inventory.items[i].type == type && inventory.items[i].amount < maxItems)
            {
                inventory.items[i].amount += 1;
                gameObject.SetActive(false);
                CloseOptionsMenu();
                break;
            }
        }
    }

    public void EatItem()
    {
        Debug.Log("Eat called");
        PlayerStats stats = PlayerStats.instance;
        switch (type)
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
        gameObject.SetActive(false);
        CloseOptionsMenu();
    }

    public void LeaveItem()
    {
        Debug.Log("leave called");
        CloseOptionsMenu();
    }
}
