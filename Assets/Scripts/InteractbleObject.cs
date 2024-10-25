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
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            LostTriggerAction.Invoke();
        }
    }

    private void OnGUI()
    {
        if (isInRange)
        {
            GUI.Box(new Rect(0, 0, 200, 25), "Press 'E' to interact");
        }
    }
}
