using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InteractableFinal : MonoBehaviour
{
    public KeyCode interactKey;
    public bool isInRange;
    public UnityEvent interactAction;

    public GameObject fake_walls;
    public GameObject Fake_Black;
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

        // Disable the tilemap and other GameObjects
        if (fake_walls != null)
        {
            fake_walls.SetActive(false);  // This disables the Tilemap GameObject and its collider
        }

        if (Fake_Black != null)
        {
            Fake_Black.SetActive(false);  // Optional: Disable another GameObject if needed
        }
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

