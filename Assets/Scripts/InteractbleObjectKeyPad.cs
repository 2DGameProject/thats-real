using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObjectKeyPad : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E;
    public bool isInRange = false;
    public KeyPadScriptClassroom keypad; // Referência ao KeyPadScriptClassroom

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
        if (keypad != null)
        {
            // Mostrar o painel de senha
            keypad.ShowKeyPad();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
        }
    }
}
