using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Boss_Interaction : MonoBehaviour
{
    public bool isInteracting;
    public bool isInRange; // Check if the player is in range
    public string[] dialogues;
    public TMP_Text dialogueText;
    public GameObject dialogueBox;
    public GameObject nextDialogueButton;
    private int dialogueIndex;
    private Coroutine typingCoroutine; // Reference to the current typing coroutine

    void Start()
    {
        ResetDialogue(); // Ensure everything is reset on start
        isInRange = false; // Initially, the player is not in range
    }

    // void Update()
    // {
    //     if (isInteracting)
    //     {
    //         if (Input.GetKeyDown(KeyCode.E))
    //         {
    //             ResetDialogue();

    //             if (dialogueBox.activeInHierarchy)
    //             {
    //                 ResetDialogue();
    //             }
    //             else
    //             {
    //                 ResetDialogue(); // Reset dialogue to start fresh
    //                 dialogueBox.SetActive(true);

    //                 // Stop any ongoing typing coroutine before starting a new one
    //                 if (typingCoroutine != null)
    //                 {
    //                     StopCoroutine(typingCoroutine);
    //                 }

    //                 typingCoroutine = StartCoroutine(TypeDialogue(dialogues[dialogueIndex]));
    //             }
    //         }

    //         // Show next button when typing completes
    //         if (dialogueText.text == dialogues[dialogueIndex])
    //         {
    //             nextDialogueButton.SetActive(true);
    //         }
    //     }
    // }

    public void Interact(){
        if (isInteracting)
        {
            ResetDialogue();

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

        // Show next button when typing completes
        if (dialogueText.text == dialogues[dialogueIndex])
        {
            nextDialogueButton.SetActive(true);
        }
    }
    private void ResetDialogue()
    {
        dialogueIndex = 0;
        dialogueText.text = ""; // Clear any existing text
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
        dialogueText.text = ""; // Clear text before typing begins
        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void NextDialogue()
    {
        nextDialogueButton.SetActive(false);

        // Check if this is the last dialogue in the array
        if (dialogueIndex < dialogues.Length - 1)
        {
            dialogueIndex++;

            // Stop any ongoing typing coroutine before starting a new one
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }

            typingCoroutine = StartCoroutine(TypeDialogue(dialogues[dialogueIndex]));
        }
        else
        {
            ResetDialogue(); // Close dialogue if it's the last index
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true; // Set player as in range
            isInteracting = true;

            NewBehaviourScript playerController = collision.GetComponent<NewBehaviourScript>();
            if (playerController != null)
            {
                playerController.SetCurrentInteractable3(this);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false; // Set player as out of range
            isInteracting = false;
            ResetDialogue(); // Reset when leaving the interaction zone

            NewBehaviourScript playerController = collision.GetComponent<NewBehaviourScript>();
            if (playerController != null && playerController.GetCurrentInteractable3() == this)
            {
                playerController.SetCurrentInteractable3(null);
            }
        }
    }

    private void OnGUI()
    {
        if (isInRange && !dialogueBox.activeInHierarchy) // Show message only if in range and dialogue box is closed
        {
            GUI.Box(new Rect(0, 0, 200, 25), "Press 'E' to interact");
        }
    }
}
