using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class InteractableFinal : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E;  // Default to 'E' key for interaction
    public bool isInRange;
    public UnityEvent interactAction;

    public GameObject fake_walls;
    public GameObject Fake_Black;

    // Password-related fields
    public GameObject passwordPanel; // Panel containing the password input UI
    public TMP_InputField passwordInputField; // Input field for the password
    public TMP_Text instructionText; // Text for displaying instructions and feedback
    public string correctPassword = "1234"; // The correct password

    void Start()
    {
        isInRange = false;
        passwordPanel.SetActive(false); // Hide password panel initially
    }

    void Update()
    {
        // Only trigger interaction if the password panel is NOT active
        if (isInRange && !passwordPanel.activeInHierarchy && Input.GetKeyDown(interactKey))
        {
            ShowPasswordPanel(); // Show password input when pressing E
        }

        // Check for Enter key only if the password panel is active
        if (passwordPanel.activeInHierarchy && Input.GetKeyDown(KeyCode.Return))
        {
            CheckPassword(); // Submit the password when Enter is pressed
        }
    }

    public void Interact()
    {
        interactAction.Invoke();

        // Disable the tilemap and other GameObjects if password is correct
        if (fake_walls != null)
        {
            fake_walls.SetActive(false);
        }

        if (Fake_Black != null)
        {
            Fake_Black.SetActive(false);
        }
    }

    private void ShowPasswordPanel()
    {
        passwordPanel.SetActive(true); // Show the password panel
        instructionText.text = "Enter the password:"; // Set instruction text
        passwordInputField.text = ""; // Clear previous text in the input field
        passwordInputField.ActivateInputField(); // Focus the input field for typing
    }

    // Method to validate the entered password
    public void CheckPassword()
    {
        if (passwordInputField.text == correctPassword)
        {
            instructionText.text = "Password correct!";
            HidePasswordPanel(); // Hide the password panel
            Interact(); // Trigger the interaction
        }
        else
        {
            instructionText.text = "Incorrect password. Try again."; // Feedback on wrong password
            passwordInputField.text = ""; // Clear input field for retry
            passwordInputField.ActivateInputField(); // Refocus the input field for another attempt
        }
    }

    private void HidePasswordPanel()
    {
        passwordPanel.SetActive(false); // Hide the password panel
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
            HidePasswordPanel(); // Hide the password panel when out of range
        }
    }

    private void OnGUI()
    {
        if (isInRange && !passwordPanel.activeInHierarchy)
        {
            GUI.Box(new Rect(0, 0, 200, 25), "Press 'E' to interact");
        }
    }
}
