using UnityEngine;

public class InteractableObject89 : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E; // Tecla para interagir
    public bool isInRange = false; // Verifica se o jogador est� no alcance
    public Puzzle1516 puzzle; // Refer�ncia ao script do puzzle
    public GameObject darkOverlay; // Refer�ncia ao fundo escuro
    public GameObject player; // Refer�ncia ao jogador para desativar o movimento
    public GameObject highlightObject;
    public NewBehaviourScript playerMovement; // Refer�ncia ao script de movimento do jogador
    private Vector2 originalVelocity; // Para armazenar a velocidade original do jogador

    void Update()
    {
        // Se o jogador estiver no alcance e pressionar a tecla de intera��o
        if (isInRange && Input.GetKeyDown(interactKey))
        {
            playerMovement.rb.velocity = Vector2.zero;
            Interact();
        }
    }

    void Start()
    {
        isInRange = false; // Inicialmente, o jogador n�o est� no alcance
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
            isInRange = true; // Jogador est� no alcance para interagir
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

    // M�todo para exibir o prompt de intera��o
    private void OnGUI()
    {
        if (isInRange)
        {
            // Exibe o prompt na tela
            
            GUI.Box(new Rect(0, 0, 200, 25), "Press 'E' to interact");
        }
    }
}
