using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextController : MonoBehaviour
{
    [SerializeField, TextArea(2, 5)] private string[] texts;
    [SerializeField] private UIManager uIManager;
    [SerializeField] private PlayerController player;

    private int index;
    private readonly float minDistance = 1.5f;

    private void OnMouseDown()
    {
        float distance = Vector2.Distance(this.gameObject.transform.position, player.transform.position);
        if(distance <= minDistance)
        {
            uIManager.SwitchTextBox(true);
            ActivateText();
        }
    }

    private void ActivateText()
    {
        if(index < texts.Length)
        {
            uIManager.ShowTexts(texts[index]);
            index++;
        }
        else
        {
            uIManager.SwitchTextBox(false);
            index = 0;
        }
    }

}
