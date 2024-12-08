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
                StartCoroutine(DelayedSceneLoad());
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
        isInRange = false; 
        highlightObject.SetActive(false);


        if (messageCanvas != null)
        {
            messageCanvas.SetActive(false);
        }
    }

    private IEnumerator DelayedSceneLoad()
    {
        yield return new WaitForSeconds(2.5f);
        LoadSpecificScene();
    }

    void LoadSpecificScene()
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void Interact()
    {

        puzzle.StartPuzzle();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            highlightObject.SetActive(true);
            isInRange = true; 
            NewBehaviourScript playerController = collision.GetComponent<NewBehaviourScript>();
            if (playerController != null)
            {
                playerController.SetCurrentInteractable89(this);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false; // Jogador saiu do alcance
            highlightObject.SetActive(false);

            NewBehaviourScript playerController = collision.GetComponent<NewBehaviourScript>();
            if (playerController != null)
            {
                playerController.SetCurrentInteractable89(null);
            }
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
