using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Boss_Interaction : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isInteracting;

    public string[] dialogues;
    public TMP_Text dialogueText;
    public GameObject dialogueBox;
    public GameObject nextDialogueButton;
    private int dialogueIndex;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isInteracting)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(dialogueBox.activeInHierarchy)
                {
                    ResetDialogue();
                }
                else
                {
                    dialogueBox.SetActive(true);
                    StartCoroutine(TypeDialogue(dialogues[dialogueIndex]));
                }
            }

            if(dialogueText.text == dialogues[dialogueIndex])
            {
                nextDialogueButton.SetActive(true);

            }

            return;
        }
        
    }

    private void ResetDialogue()
    {
        dialogueIndex = 0;
        dialogueText.text = "";
        dialogueBox.SetActive(false);
    }

    IEnumerator TypeDialogue(string dialogue)
    {
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
            dialogueText.text = "";
            StartCoroutine(TypeDialogue(dialogues[dialogueIndex]));
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
            ResetDialogue();
        }
    }


}
