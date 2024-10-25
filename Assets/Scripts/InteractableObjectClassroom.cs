using UnityEngine;

public class InteractableObjectClassroom : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E; // A tecla de intera��o
    public bool isInRange = false; // Define se o jogador est� dentro do alcance
    public PanelManager panelManager; // Refer�ncia ao PanelManager

    void Update()
    {
        // Se o jogador estiver no alcance e pressionar a tecla de intera��o
        if (isInRange && Input.GetKeyDown(interactKey))
        {
            panelManager.ShowPanel(); // Chama o m�todo para mostrar o painel
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que entrou na �rea de colis�o � o jogador
        if (collision.CompareTag("Player"))
        {
            isInRange = true; // Jogador est� no alcance
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
