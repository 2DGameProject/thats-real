using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailCrotroller : MonoBehaviour
{
    public List<GameObject> mailPanels;

    void Start()
    {
        foreach (GameObject mailPanel in mailPanels)
        {
            mailPanel.SetActive(false);
        }
        mailPanels[0].SetActive(true);
    }

    public void OpenMail(int mailIndex)
    {
        foreach (GameObject mailPanel in mailPanels)
        {
            mailPanel.SetActive(false);
        }
        mailPanels[mailIndex].SetActive(true);
    }
}
