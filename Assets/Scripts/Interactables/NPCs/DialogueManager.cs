using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public GameObject dialogueBox;
    private Queue<string> sentences;
    private Dialogue dialogue;
    void Start()
    {
        sentences = new Queue<string>();

       
    }


    private void ResetComponents()
    {
        this.enabled = true;
        dialogueBox.SetActive(true);
        
    }

    public void StartDialogue()
    {
        ResetComponents();

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
    }


    public void EndDialog()
    {
        this.enabled = false;
        dialogueBox.SetActive(false);
        
           
    }


    //GETTERS Y SETTERS

    public void SetDialogue (Dialogue dialogue)
    {
        this.dialogue = dialogue;
    }
}
