using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Rendering;
using System;
public class MatiasDialogue : MonoBehaviour
{

    private bool didDialogueStart, startGame = true; 
    private int lineIndex;
    private float typingTime = 0.05f;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText; 
    [SerializeField , TextArea (4,6)] private string[] dialogueLines; 
    // Start is called before the first frame update

    private void Start() {
        if(!didDialogueStart)
        {
            Invoke("StartDialogue",1.0f);          
        }   
    }
    void Update()
    {
        if(startGame == true && Input.GetButtonDown("Fire1"))
        {
        if(dialogueText.text == dialogueLines[lineIndex])
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
        lineIndex = 0;
        Time.timeScale = 0f;
        StartCoroutine(ShowLine());
    }

    //Va mostrando línea por línea de diálogo
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
            startGame = false; 
            Time.timeScale = 1f;
        }
    }

    //Prepara la línea de dialogo que se va a mostrar y da el efecto de tipeo
    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach(char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }
}
