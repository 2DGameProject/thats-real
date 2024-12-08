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

    private Coroutine typingCoroutine;
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

    private void StartTyping(string dialogue)
    {
        dialogueText.text = "";
        typingCoroutine = StartCoroutine(TypeDialogue(dialogue));
    }

    private void ResetDialogue()
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

    IEnumerator TypeDialogue(string dialogue)
    {
        foreach (char letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.025f);
        }
    }

    public void Interact(){
        if(dialogueBox.activeInHierarchy)
        {
            ResetDialogue();
        }
        else
        {
            dialogueBox.SetActive(true);
            nextDialogueButton.SetActive(false);
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }
            StartTyping(dialogues[dialogueIndex]);
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

    public void NextDialogue()
    {
        nextDialogueButton.SetActive(false);

        if(dialogueIndex < dialogues.Length - 1)
        {
            dialogueIndex++;
            StartTyping(dialogues[dialogueIndex]);
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

            NewBehaviourScript playerController = collision.GetComponent<NewBehaviourScript>();
            if (playerController != null)
            {
                playerController.SetCurrentInteractable7(this);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInteracting = false;
            ResetDialogue();

            NewBehaviourScript playerController = collision.GetComponent<NewBehaviourScript>();
            if (playerController != null)
            {
                playerController.SetCurrentInteractable7(null);
            }
        }
    }
}
