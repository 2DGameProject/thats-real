using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginValidator : MonoBehaviour
{
    public TMP_InputField loginField;
    public TMP_InputField passwordField;

    public TMP_Text loginText;
    public Button loginButton;

    void Start()
    {
        loginField.text = "";
        passwordField.text = "";
        loginText.text = "";
    }


    public void ValidateLogin()
    {
        if(loginField.text == "maria#vasconcello" && passwordField.text == "batata@123")
        {
            loginText.text = "Login Successful";
        }
        else
        {
            loginText.text = "Login Failed";
        }
    }
}
