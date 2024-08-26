using UnityEngine;

public class SettingsButton : MonoBehaviour
{
    [SerializeField] private GameObject volumePanel;  // The panel that contains the volume controls

    public void OnSettingsButtonClick()
    {
        // Toggle the volume panel visibility
        bool isActive = volumePanel.activeSelf;
        volumePanel.SetActive(!isActive); // Toggles between showing and hiding the volume panel
    }
}
