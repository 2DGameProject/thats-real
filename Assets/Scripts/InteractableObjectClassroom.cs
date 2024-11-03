using UnityEngine;
public class InteractableObjectClassroom : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E; // A tecla de intera��o
    public bool isInRange = false; // Define se o jogador est� dentro do alcance
    public PanelManagerClassroom panelManagerClassroom; // Refer�ncia ao PanelManager
    public GameObject highlightObject;

    public NewBehaviourScript playerMovement; // Refer�ncia ao script de movimento do jogador

    private Vector2 originalVelocity; // Para armazenar a velocidade original do jogador
    void Start()
    {
        highlightObject.SetActive(false);
    }

void Update()
    {
        // Se o jogador estiver no alcance e pressionar a tecla de intera��o
        if (isInRange && Input.GetKeyDown(interactKey))
        {
            playerMovement.rb.velocity = Vector2.zero;
            panelManagerClassroom.ShowPanel(); // Chama o m�todo para mostrar o painel
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que entrou na �rea de colis�o � o jogador
        if (collision.CompareTag("Player"))
        {
            isInRange = true; // Jogador est� no alcance
            highlightObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Quando o jogador sair do alcance
        if (collision.CompareTag("Player"))
        {
            highlightObject.SetActive(false);
            isInRange = false; // Jogador saiu do alcance
        }
    }
}