using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Final_Interaction : MonoBehaviour
{
    public bool isInteracting;
    public string[] dialogues;
    public TMP_Text dialogueText;
    public GameObject dialogueBox;
    public GameObject nextDialogueButton;
    private int dialogueIndex;

    private Coroutine typingCoroutine; // Reference to the typing coroutine

    void Start()
    {
        ResetDialogue();
    }

    void Update()
    {
        if (isInteracting)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (dialogueBox.activeInHierarchy)
                {
                    ResetDialogue();
                }
                else
                {
                    ResetDialogue(); // Reset dialogue to start fresh
                    dialogueBox.SetActive(true);

                    // Stop any ongoing typing coroutine before starting a new one
                    if (typingCoroutine != null)
                    {
                        StopCoroutine(typingCoroutine);
                    }

                    typingCoroutine = StartCoroutine(TypeDialogue(dialogues[dialogueIndex]));
                }
            }

            if (dialogueText.text == dialogues[dialogueIndex])
            {
                nextDialogueButton.SetActive(true);
            }
        }
    }

    private void ResetDialogue()
    {
        dialogueIndex = 0;
        dialogueText.text = ""; // Clear the text to start fresh
        dialogueBox.SetActive(false);
        nextDialogueButton.SetActive(false); // Hide next button initially

        // Stop the typing coroutine if it's running to avoid overlap
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null; // Reset the reference
        }
    }

    IEnumerator TypeDialogue(string dialogue)
    {
        dialogueText.text = ""; // Clear text before typing
        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void NextDialogue()
    {
        nextDialogueButton.SetActive(false);

        // Stop any ongoing typing coroutine before starting a new one
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        if (dialogueIndex < dialogues.Length - 1)
        {
            dialogueIndex++;
            dialogueText.text = ""; // Clear text before starting next dialogue
            typingCoroutine = StartCoroutine(TypeDialogue(dialogues[dialogueIndex]));
        }
        else
        {
            ResetDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInteracting = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInteracting = false;
            ResetDialogue(); // Reset when leaving the interaction zone
        }
    }
}
