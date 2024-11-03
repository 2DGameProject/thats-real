using UnityEngine;

public class InteractableObject89 : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E; // Tecla para interagir
    public bool isInRange = false; // Verifica se o jogador está no alcance
    public Puzzle1516 puzzle; // Referência ao script do puzzle
    public GameObject darkOverlay; // Referência ao fundo escuro
    public GameObject player; // Referência ao jogador para desativar o movimento
    public GameObject highlightObject;
    public NewBehaviourScript playerMovement; // Referência ao script de movimento do jogador
    private Vector2 originalVelocity; // Para armazenar a velocidade original do jogador

    void Update()
    {
        // Se o jogador estiver no alcance e pressionar a tecla de interação
        if (isInRange && Input.GetKeyDown(interactKey))
        {
            playerMovement.rb.velocity = Vector2.zero;
            Interact();
        }
    }

    void Start()
    {
        isInRange = false; // Inicialmente, o jogador não está no alcance
        highlightObject.SetActive(false);
    }

    public void Interact()
    {
        // Inicia o puzzle, exibe o fundo escuro e desativa o movimento do jogador
        puzzle.StartPuzzle();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            highlightObject.SetActive(true);
            isInRange = true; // Jogador está no alcance para interagir
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false; // Jogador saiu do alcance
            highlightObject.SetActive(false);
            
        }
    }

    // Método para exibir o prompt de interação
    private void OnGUI()
    {
        if (isInRange)
        {
            // Exibe o prompt na tela
            
            GUI.Box(new Rect(0, 0, 200, 25), "Press 'E' to interact");
        }
    }
}
