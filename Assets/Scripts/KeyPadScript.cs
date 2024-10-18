using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class KeyPadScript : MonoBehaviour
{
    public TMP_Text codeOfKeypad;

    void Start()
    {
        codeOfKeypad.text = "";
        codeOfKeypad.color = Color.white;

    }

    public void AddNumber(string number)
    {
        if(codeOfKeypad.text.Length >= 4)
        {
            CodeComplete();
        }else{
            codeOfKeypad.text += number;
            if(codeOfKeypad.text.Length == 4)
            {
                CodeComplete();
            }
        }
    }

    public void CodeComplete()
    {
        if (codeOfKeypad.text == "1234")
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

    public void ShowKeyPad()
    {
        gameObject.SetActive(gameObject.activeSelf ? false : true);
    }
    
}
