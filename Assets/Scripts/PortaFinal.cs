using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // For scene management

public class PortaFinal : MonoBehaviour
{
    public bool isInteracting;
    public bool isInRange; // Check if the player is in range

    void Start()
    {
        isInRange = false; // Initially, the player is not in range   
    }

    void Update()
    {
        if (isInteracting && isInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Porta final aberta");
            LoadFinalScene(); // Load the final scene
            isInteracting = false; // Prevent further interactions
        }
    }

    private void LoadFinalScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Room"); // Replace "Menu" with the exact name of your final scene
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            isInteracting = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            isInteracting = false;
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
