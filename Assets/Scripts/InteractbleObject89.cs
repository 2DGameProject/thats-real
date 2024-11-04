using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using TMPro;

public class InteractableObject89 : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E; // Tecla para interagir
    public bool isInRange = false; // Verifica se o jogador est� no alcance
    public Puzzle1516 puzzle; // Refer�ncia ao script do puzzle
    public GameObject darkOverlay; // Refer�ncia ao fundo escuro
    public GameObject player; // Refer�ncia ao jogador para desativar o movimento
    public GameObject highlightObject;
    public NewBehaviourScript playerMovement; // Refer�ncia ao script de movimento do jogador
    public GameObject messageCanvas; // Canvas com fundo branco e texto de mensagem
    public TMP_Text notificationText; // Texto de notifica��o no Canvas

    private Coroutine notificationCoroutine; // Controle da exibi��o da mensagem

    void Update()
    {
        // Se o jogador estiver no alcance e pressionar a tecla de intera��o
        if (isInRange && Input.GetKeyDown(interactKey))
        {
            // Verifica se o arm�rio est� dispon�vel (puzzle resolvido)
            if (GameStateClassroom.isCabinetAvailable)
            {
                ShowTemporaryMessage("De onde veio este Arm�rio?! Parece que h� uma passagem secreta por aqui.");

                LoadSpecificScene();
            }
            // Caso contr�rio, verifica se o jogador possui o item chave para iniciar o puzzle
            else if (GameStateClassroom.hasKeyItem)
            {
                playerMovement.rb.velocity = Vector2.zero;
                Interact();
            }
            else
            {
                ShowTemporaryMessage("Parece que est� faltando uma pe�a");
            }
        }
    }

    void Start()
    {
        isInRange = false; // Inicialmente, o jogador n�o est� no alcance
        highlightObject.SetActive(false);

        // Certifique-se de que o Canvas e o texto est�o escondidos no in�cio
        if (messageCanvas != null)
        {
            messageCanvas.SetActive(false);
        }
    }

    void LoadSpecificScene()
    {
        // Carrega diretamente a cena com o �ndice 2 ou pelo nome "Office"
            UnityEngine.SceneManagement.SceneManager.LoadScene("Office");
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

            // Garante que a mensagem e o canvas sejam ocultados ao sair do alcance
            if (notificationCoroutine != null)
            {
                StopCoroutine(notificationCoroutine);
                notificationCoroutine = null;
            }

            if (messageCanvas != null)
            {
                messageCanvas.SetActive(false);
            }
        }
    }

    // M�todo para exibir mensagem tempor�ria
    private void ShowTemporaryMessage(string message)
    {
        if (notificationCoroutine != null)
        {
            StopCoroutine(notificationCoroutine);
        }
        notificationCoroutine = StartCoroutine(DisplayMessage(message, 3f)); // Exibe a mensagem por 2 segundos
    }

    private IEnumerator DisplayMessage(string message, float duration)
    {
        notificationText.text = message;
        messageCanvas.SetActive(true); // Mostra o Canvas com o ret�ngulo branco e a mensagem

        yield return new WaitForSeconds(duration);

        messageCanvas.SetActive(false); // Oculta o Canvas ap�s o tempo definido
        notificationCoroutine = null;
    }
}
