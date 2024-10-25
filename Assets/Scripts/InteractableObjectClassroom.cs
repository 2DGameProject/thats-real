using UnityEngine;

public class InteractableObjectClassroom : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E; // A tecla de interação
    public bool isInRange = false; // Define se o jogador está dentro do alcance
    public PanelManager panelManager; // Referência ao PanelManager

    void Update()
    {
        // Se o jogador estiver no alcance e pressionar a tecla de interação
        if (isInRange && Input.GetKeyDown(interactKey))
        {
            panelManager.ShowPanel(); // Chama o método para mostrar o painel
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que entrou na área de colisão é o jogador
        if (collision.CompareTag("Player"))
        {
            isInRange = true; // Jogador está no alcance
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Quando o jogador sair do alcance
        if (collision.CompareTag("Player"))
        {
            isInRange = false; // Jogador saiu do alcance
        }
    }
}
