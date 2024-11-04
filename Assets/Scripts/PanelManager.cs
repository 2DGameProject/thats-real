using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{

    public GameObject panel;
    public void ShowPanel()
    {
        panel.SetActive(true);
    }
    public void ActivePanel()
    {
        panel.SetActive(true);
    }
    public void DeactivePanel()
    {
        panel.SetActive(false);
    }
    void Start()
    {
        panel.SetActive(false);
    }
}
