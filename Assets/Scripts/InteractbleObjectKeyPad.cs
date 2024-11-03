using System.Collections;
using UnityEngine;

public class InteractableObjectKeyPad : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E;
    public bool isInRange = false;
    public KeyPadScriptClassroom keypad; // Referência ao KeyPadScriptClassroom
    public GameObject highlightObject;
    public GameObject keyPiecePuzzle; // O objeto ou painel que será exibido ao invés do KeyPad
    public PanelManagerClassroom panelManagerClassroom;
    public NewBehaviourScript playerMovement; // Referência ao script de movimento do jogador

    private Vector2 originalVelocity; // Para armazenar a velocidade original do jogador

    void Start()
    {
        highlightObject.SetActive(false);
        isInRange = false;

        // Garante que o painel do item chave esteja desativado inicialmente
        if (keyPiecePuzzle != null)
        {
            keyPiecePuzzle.SetActive(false);
        }
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(interactKey))
        {
            Interact();
        }
    }

    public void Interact()
    {
        playerMovement.rb.velocity = Vector2.zero;
        if (GameStateClassroom.hasKeyItem)
        {
            
            // Se o jogador já possui o item chave, mostra o painel do item em vez do KeyPad
            if (panelManagerClassroom != null)
            {
                
                panelManagerClassroom.ShowPanel(); // Chama ShowPanel
            }
            else
            {
                Debug.LogError("panelManagerClassroom não está referenciado no Inspector!"); // Log de erro se estiver null
            }
        }
        else if (keypad != null)
        {
            keypad.ShowKeyPad();
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            highlightObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            highlightObject.SetActive(false);
        }
    }
}
