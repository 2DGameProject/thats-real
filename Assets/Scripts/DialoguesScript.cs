using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguesScript : MonoBehaviour
{
    public string[] dialogues;
    public int dialogueIndex;
    public GameObject dialogueBox;
    public GameObject nextDialogueButton;
    public TMPro.TMP_Text dialogueText;

    public void ResetDialogue()
    {
        dialogueBox.SetActive(false);
        dialogueIndex = 0;
    }

    public IEnumerator TypeDialogue(string dialogue)
    {
        dialogueText.text = "";
        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void NextDialogue()
    {
        nextDialogueButton.SetActive(false);

        if(dialogueIndex < dialogues.Length - 1)
        {
            dialogueIndex++;
            StartCoroutine(TypeDialogue(dialogues[dialogueIndex]));
        }
        else
        {
            ResetDialogue();
        }
    }

    public void showDialogue()
    {
        dialogueBox.SetActive(true);
        StartCoroutine(TypeDialogue(dialogues[dialogueIndex]));
    }

    public void hideDialogue()
    {
        ResetDialogue();
    }
}
