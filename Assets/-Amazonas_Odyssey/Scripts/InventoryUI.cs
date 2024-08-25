using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory; // Reference to the inventory
    public Transform itemsParent; // Parent object that holds the inventory slots
    public InventorySlot[] slots; // Array of inventory slots

    [SerializeField] private GameObject pauseMenuPanel; // The panel that contains the buttons
    [SerializeField] private GameObject settingsPanel;  // The settings panel
    [SerializeField] private GameObject creditsPanel;   // The credits panel

    void Start()
    {
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        UpdateUI();
        // Ensure the menu panels are hidden at the start
        pauseMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    // Resume the game and close the pause menu
    public void ResumeGame()
    {
        Time.timeScale = 1f; // Resume the game
        pauseMenuPanel.SetActive(false); // Hide the pause menu panel
    }

    // Open the settings panel
    public void OpenSettings()
    {
        pauseMenuPanel.SetActive(false); // Hide the pause menu panel
        settingsPanel.SetActive(true);   // Show the settings panel
    }

    // Open the credits panel
    public void OpenCredits()
    {
        pauseMenuPanel.SetActive(false); // Hide the pause menu panel
        creditsPanel.SetActive(true);    // Show the credits panel
    }
}
