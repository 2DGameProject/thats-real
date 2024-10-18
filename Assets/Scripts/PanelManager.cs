using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{

    public GameObject panel;
    public void ShowPanel()
    {
        panel.SetActive(panel.activeSelf ? false : true);
    }

    void Start()
    {
        panel.SetActive(false);
    }
}
