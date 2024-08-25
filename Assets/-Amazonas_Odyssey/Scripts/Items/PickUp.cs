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
    private bool isPlayerNear = false;
    [SerializeField] private GameObject interactionPrompt;
    [SerializeField] private GameObject optionsMenu;

    // Referencia estática a la instancia actual
    public static PickUp currentPickUp;

    void Start()
    {
        inventory = PlayerInventory.Instance;
        sprite = GetComponent<SpriteRenderer>().sprite;
    }

    private void OnEnable()
    {
        Transform uiManager = GameObject.FindWithTag("UIManager").transform;
        interactionPrompt = uiManager.GetChild(0).gameObject;
        optionsMenu = uiManager.GetChild(1).gameObject;
    }
    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            OpenOptionsMenu();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = true;
            interactionPrompt.SetActive(true);
            currentPickUp = this; // Actualizamos la referencia estática
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
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
            // Verifica si el tipo coincide y si el slot no está lleno.
            if (inventory.items[i].type == type && inventory.items[i].amount < maxItems)
            {
                inventory.items[i].isOccupied = true;
                inventory.items[i].amount += 1;
    
                var imageComponent = inventory.items[i].slotSprite.GetComponentInChildren<Image>();
                imageComponent.sprite = sprite;
                imageComponent.gameObject.SetActive(true);

                inventory.items[i].amountText.text = inventory.items[i].amount.ToString();

                gameObject.SetActive(false); // Desactivar el objeto del mundo.
                CloseOptionsMenu();

                inventory.CheckForLanceActivation();

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
            case ItemType.CARAMBOLO:
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
