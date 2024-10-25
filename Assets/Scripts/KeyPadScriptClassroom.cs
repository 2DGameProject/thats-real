using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KeyPadScriptClassroom : MonoBehaviour
{
    public TMP_Text codeOfKeypad;
    public GameObject darkOverlay;  // Referência ao fundo escuro
    public GameObject player;       // Referência ao jogador para desativar o movimento
    public GameObject closeButton;  // Referência ao botão de fechar

    void Start()
    {
        codeOfKeypad.text = "";
        codeOfKeypad.color = Color.white;

        // Inicialmente, desativamos o KeyPad e o overlay
        gameObject.SetActive(false);
        darkOverlay.SetActive(false);
        closeButton.SetActive(false);
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
        if (codeOfKeypad.text == "42334")
        {
            codeOfKeypad.color = Color.green;
            Debug.Log("Correct code!");
        }
        else
        {
            codeOfKeypad.color = Color.red;
            Debug.Log("Incorrect code!");
            codeOfKeypad.text = "";
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
