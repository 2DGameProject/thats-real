using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Patrol : MonoBehaviour
{

    public float speed;
    public float startWaitTime;
    public float TimeToReachSpot;
    public Transform[] moveSpots;
    public bool isInteracting;

    public string[] dialogues;
    public TMP_Text dialogueText;
    public GameObject dialogueBox;
    public GameObject nextDialogueButton;
    private int dialogueIndex;


    private float waitTime;
    private float _timeToReachSpot;
    private int InitialSpot;
    
    void Start()
    {
        InitialSpot = 0;
    }

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

        transform.position = Vector2.MoveTowards(transform.position, moveSpots[InitialSpot].position, speed * Time.deltaTime);


        if(Vector2.Distance(transform.position, moveSpots[InitialSpot].position) < 0.2f)
        {
            if(waitTime <= 0)
            {
                InitialSpot += 1;
                if(InitialSpot == moveSpots.Length)
                {
                    InitialSpot = 0;
                }
                waitTime = startWaitTime;
            }else{
                waitTime -= Time.deltaTime;
            }
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
