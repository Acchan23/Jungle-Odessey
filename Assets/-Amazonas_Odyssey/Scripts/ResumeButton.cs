using UnityEngine;

public class ResumeButton : MonoBehaviour
{
    public GameObject inventoryPanel; // El panel de inventario

    // Este método será llamado cuando se presione el botón "Resume"
    public void OnResumeButtonClick()
    {
        Time.timeScale = 1f; // Reanuda el tiempo del juego
        inventoryPanel.SetActive(false); // Oculta el panel de inventario
    }
}
