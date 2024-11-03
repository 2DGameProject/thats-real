using System.Collections;
using UnityEngine;
using TMPro;

public class FinalDialoguesScript : MonoBehaviour
{
    public string[] dialogues;
    public int dialogueIndex = 0; 
    public GameObject dialogueBox; 
    public GameObject nextDialogueButton;
    public TMP_Text dialogueText; 
    public GameObject[] NPCsImages;

    private Coroutine typingCoroutine;
    private bool isInteracting;

    public void Start()
    {
        ShowDialogue();
    }

    public void ResetDialogue()
    {
        isInteracting = false;
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        dialogueBox.SetActive(false);
        dialogueIndex = 0;
        dialogueText.text = "";
    }

    public IEnumerator TypeDialogue(string dialogue)
    {
        dialogueText.text = ""; 
        foreach (char letter in dialogue.ToCharArray())
        {
            if(isInteracting){
                break;
            }
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

        nextDialogueButton.SetActive(true);
    }

    public void NextDialogue()
    {
        nextDialogueButton.SetActive(false);

        if (dialogueIndex < dialogues.Length - 1)
        {
            dialogueIndex++;
            for (int i = 0; i < NPCsImages.Length; i++)
            {
                NPCsImages[i].SetActive(false);
            }
            NPCsImages[dialogueIndex].SetActive(true);
            StartTyping(dialogues[dialogueIndex]);
        }
        else
        {
            ResetDialogue();
        }
    }

    public void ShowDialogue()
    {
        dialogueBox.SetActive(true);
        nextDialogueButton.SetActive(false);

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        StartTyping(dialogues[dialogueIndex]);
    }

    public void HideDialogue()
    {
        ResetDialogue();
    }

    private void StartTyping(string dialogue)
    {
        typingCoroutine = StartCoroutine(TypeDialogue(dialogue));
    }
}
