using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginScreenScript : MonoBehaviour
{
    public GameObject loginPanel;

    public TMP_InputField loginField;
    public TMP_InputField passwordField;

    public string username;
    public string password;

    public TMP_Text loginText;
    public Button loginButton;
    void Start()
    {
        loginField.text = "";
        passwordField.text = "";
    }

    public void OnLoginButtonClicked()
    {
        string _username = loginField.text;
        string _password = passwordField.text;
        if (_username == username && _password == password)
        {
            loginPanel.SetActive(false);
            loginText.text = "Login Successful";
        }
        else
        {
            loginText.color = Color.red;
            loginText.text = "Login Failed";
        }
    }
}
