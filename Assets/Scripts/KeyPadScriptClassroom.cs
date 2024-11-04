using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KeyPadScriptClassroom : MonoBehaviour
{
    public TMP_Text codeOfKeypad;
    public GameObject darkOverlay;      // Referência ao fundo escuro
    public GameObject player;           // Referência ao jogador para desativar o movimento
    public GameObject closeButton;      // Referência ao botão de fechar
    public GameObject keyItem;          // Objeto chave que será revelado
    public GameObject keyItemCanvas;    // Canvas opcional para mostrar que o item foi adquirido

    void Start()
    {
        codeOfKeypad.text = "";
        codeOfKeypad.color = Color.white;

        // Inicialmente, desativamos o KeyPad, overlay, item chave e canvas
        gameObject.SetActive(false);
        darkOverlay.SetActive(false);
        closeButton.SetActive(false);

        if (keyItem != null)
        {
            keyItem.SetActive(false);
        }
        if (keyItemCanvas != null)
        {
            keyItemCanvas.SetActive(false);
        }
    }

    public void AddNumber(string number)
    {
        if (codeOfKeypad.text.Length >= 5)
        {
            CodeComplete();
        }
        else
        {
            codeOfKeypad.text += number;
            if (codeOfKeypad.text.Length == 5)
            {
                CodeComplete();
            }
        }
    }

    public void CodeComplete()
    {
        if (codeOfKeypad.text == "42334") // Verifica se o código está correto
        {
            codeOfKeypad.color = Color.green;

            // Define o estado do jogo como tendo o item chave e revela o item
            GameStateClassroom.PickUpKeyItem();
            RevealKeyItem();

            StartCoroutine(CloseKeyPadWithDelay(1f));
        }
        else
        {
            codeOfKeypad.color = Color.red;
            StartCoroutine(ResetCodeAfterDelay(0.5f));

        }
    }

    private IEnumerator CloseKeyPadWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        CloseKeyPad();
    }

    private IEnumerator ResetCodeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        codeOfKeypad.text = "";
        codeOfKeypad.color = Color.white;
    }

    private void RevealKeyItem()
    {
        // Revela o item chave ou o Canvas
        if (keyItem != null)
        {
            keyItem.SetActive(true);
        }
        if (keyItemCanvas != null)
        {
            keyItemCanvas.SetActive(true);
        }
    }

    public void ButtonClick(Button button)
    {
        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();
        AddNumber(buttonText.text);
    }

    // Mostrar o painel de senha
    public void ShowKeyPad()
    {
        bool isActive = gameObject.activeSelf ? false : true;

        // Ativar/desativar o KeyPad e o fundo escuro
        gameObject.SetActive(isActive);
        darkOverlay.SetActive(isActive);
        closeButton.SetActive(isActive);


        // Desativar/ativar o controle do jogador
        player.GetComponent<NewBehaviourScript>().enabled = !isActive;
    }

    // Fechar o Keypad manualmente pelo botão
    public void CloseKeyPad()
    {
        gameObject.SetActive(false);
        darkOverlay.SetActive(false);
        closeButton.SetActive(false);

        // Reativar o controle do jogador
        player.GetComponent<NewBehaviourScript>().enabled = true;
    }
}
