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

    public TMP_Text countdownText;  // TMP_Text to display the countdown timer
    private bool countdownStarted = false;  // Track if the countdown has started
    private float countdownTime = 300f;  // 5 minutes in seconds

    private Coroutine typingCoroutine; // Reference to the typing coroutine

    void Start()
    {
        ResetDialogue();
        countdownText.gameObject.SetActive(false);  // Initially hide countdown text
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

                    if (!countdownStarted)  // Ensure countdown only starts once
                    {
                        StartCountdown();  // Start countdown immediately
                    }
                }
            }

            if (dialogueText.text == dialogues[dialogueIndex])
            {
                nextDialogueButton.SetActive(true);
            }
        }

        if (countdownStarted)
        {
            UpdateCountdown();  // Continuously update the timer
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

    private void StartCountdown()
    {
        countdownStarted = true;
        countdownText.gameObject.SetActive(true);  // Show countdown on the screen
    }

    private void UpdateCountdown()
    {
        if (countdownTime > 0)
        {
            countdownTime -= Time.deltaTime;  // Reduce time

            int minutes = Mathf.FloorToInt(countdownTime / 60);
            int seconds = Mathf.FloorToInt(countdownTime % 60);
            int milliseconds = Mathf.FloorToInt((countdownTime % 1) * 1000);  // Extract milliseconds

            // Update countdown text with minutes, seconds, and milliseconds
            countdownText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        }
        else
        {
            countdownStarted = false;  // Stop countdown when time is up
            countdownText.text = "00:00:000";  // Display 0 when time runs out
            // Optionally: Trigger an event when the timer finishes
        }
    }
}
