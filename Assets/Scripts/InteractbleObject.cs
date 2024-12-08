using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InteractbleObject : MonoBehaviour
{
    public KeyCode interactKey;
    public bool isInRange;
    public UnityEvent interactAction;
    public UnityEvent LostTriggerAction;
    public GameObject highlightObject;

    void Update()
    {
        if (isInRange && Input.GetKeyDown(interactKey))
        {
            Interact();
        }
    }

    void Start()
    {
        isInRange = false;
    }

    public void Interact()
    {
        interactAction.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;

            NewBehaviourScript playerController = collision.GetComponent<NewBehaviourScript>();
            if (playerController != null)
            {
                playerController.SetCurrentInteractable2(this);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            highlightObject.SetActive(false);
            LostTriggerAction.Invoke();
            NewBehaviourScript playerController = collision.GetComponent<NewBehaviourScript>();
            if (playerController != null)
            {
                playerController.SetCurrentInteractable2(null);
            }
        }
    }

    private void OnGUI()
    {
        if (isInRange)
        {
            highlightObject.SetActive(true);
            GUI.Box(new Rect(0, 0, 200, 25), "Press 'E' to interact");
        }
    }
}
