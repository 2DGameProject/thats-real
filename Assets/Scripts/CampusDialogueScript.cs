using System.Collections;
using UnityEngine;
using TMPro;

public class CampusDialogueScript : MonoBehaviour
{
    public int dialogueIndex = 0; 
    public GameObject dialogueBox; 

    public TMP_Text dialogueText; 
    public string[] dialogues;
    public GameObject[] NPCsImages;

    public AudioClip[] audioClips;
    public AudioSource audioSource;

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

        yield return new WaitForSeconds(2f);

        NextDialogue();
    }

    public void NextDialogue()
    {
        if (dialogueIndex < dialogues.Length - 1)
        {
            dialogueIndex++;
            for (int i = 0; i < NPCsImages.Length; i++)
            {
                NPCsImages[i].SetActive(false);
            }
            
            audioSource.clip = audioClips[dialogueIndex];
            audioSource.Play();
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
        audioSource.clip = audioClips[0];
        audioSource.Play();
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
