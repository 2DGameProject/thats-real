using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Patrol : MonoBehaviour
{
    public float speed;
    public float startWaitTime;
    public Transform[] moveSpots;
    public bool isInteracting;

    public string[] dialogues;
    public TMP_Text dialogueText;
    public GameObject dialogueBox;
    public GameObject nextDialogueButton;
    private int dialogueIndex;

    public Animator anim;

    private float waitTime;
    private int InitialSpot;
    private Vector3 lastPosition;

    void Start()
    {
        InitialSpot = 0;
        lastPosition = transform.position;
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
            
            anim.SetFloat("Vertical", 0);
            anim.SetFloat("Horizontal", 0);
            anim.SetFloat("Speed", 0);
            
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, moveSpots[InitialSpot].position, speed * Time.deltaTime);

        Vector3 movementDirection = transform.position - lastPosition;

        if (Mathf.Abs(movementDirection.x) >= Mathf.Abs(movementDirection.y))
        {
            if (movementDirection.x > 0)
            {
                anim.SetFloat("Horizontal", 1);
                anim.SetFloat("Vertical", 0);
            }
            else if (movementDirection.x < 0)
            {
                anim.SetFloat("Horizontal", -1);
                anim.SetFloat("Vertical", 0);
            }
        }
        else
        {
            if (movementDirection.y > 0)
            {
                anim.SetFloat("Vertical", 1);
                anim.SetFloat("Horizontal", 0);
            }
            else if (movementDirection.y < 0)
            {
                anim.SetFloat("Vertical", -1);
                anim.SetFloat("Horizontal", 0);
            }
        }

        anim.SetFloat("Speed", movementDirection.magnitude);

        lastPosition = transform.position;

        if(Vector2.Distance(transform.position, moveSpots[InitialSpot].position) < 0.2f)
        {
            if(waitTime <= 0)
            {
                InitialSpot++;
                if(InitialSpot == moveSpots.Length)
                {
                    InitialSpot = 0;
                }
                waitTime = startWaitTime;
            }
            else
            {
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
