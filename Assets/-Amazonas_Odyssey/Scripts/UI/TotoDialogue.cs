using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering;
using System;

public class TotoDialogue : MonoBehaviour
{
   private bool isPlayerInRange;
   private bool didDialogueStart; 
   private int lineIndex;
   private float typingTime = 0.05f;
   [SerializeField] private GameObject dialogueMark; 
   [SerializeField] private GameObject dialoguePanel;
   [SerializeField] private TMP_Text dialogueText; 
   [SerializeField , TextArea (4,6)] private string[] dialogueLines; 
    // Update is called once per frame
    void Update()
    {
        if(isPlayerInRange && Input.GetButtonDown("Fire1"))
        {   
            if(!didDialogueStart)
            {
              StartDialogue();          
            }     
            else if(dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }       
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }
    }

    private void StartDialogue()
    {

        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        dialogueMark.SetActive(false);
        lineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if(lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            dialogueMark.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach(char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
       if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            dialogueMark.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogueMark.SetActive(false);
        } 
    }

}
