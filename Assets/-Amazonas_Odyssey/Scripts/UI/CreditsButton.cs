using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButton: MonoBehaviour
{
   public GameObject panelCredits;

    public void OnCreditsButtonClick()
    {
        
        panelCredits.SetActive(true); // Muestra el panel de créditos
    }
}
