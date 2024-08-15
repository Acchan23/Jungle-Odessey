using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextController : MonoBehaviour
{
    [SerializeField, TextArea(2, 5)] private string[] texts;
    [SerializeField] private UIManager uIManager;
    [SerializeField] private PlayerController player;
    

    private int index;
    private float distance;
    private readonly float minDistance = 1.2f;

    
    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            distance = Vector2.Distance(this.gameObject.transform.position, player.transform.position);
            if (distance <= minDistance)
            {
                uIManager.TextBox.SetActive(true);
                player.Investigate(true);
                ActivateText();
            }
        }
    }
    private void ActivateText()
    {
        if (index < texts.Length)
        {
            uIManager.ShowTexts(texts[index]);
            index++;
        }
        else
        {
            DeactivateTextBox();
        }
    }

    private void DeactivateTextBox()
    {
        player.Investigate(false);
        uIManager.TextBox.SetActive(false);
        index = 0;
    }
}
