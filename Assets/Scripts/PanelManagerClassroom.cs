using UnityEngine;

public class PanelManagerClassroom : MonoBehaviour
{
    public GameObject panel;
    public GameObject darkOverlay;
    public GameObject player;

    void Start()
    {
        panel.SetActive(false);
        darkOverlay.SetActive(false);
    }

    public void ShowPanel() // Certifique-se de que este m�todo � p�blico
    {
        panel.SetActive(true);
        darkOverlay.SetActive(true);

        if (player != null)
        {
            player.GetComponent<NewBehaviourScript>().enabled = false;
        }
    }

    public void ClosePanel() // Tamb�m pode ser necess�rio para fechar o painel
    {
        panel.SetActive(false);
        darkOverlay.SetActive(false);

        if (player != null)
        {
            player.GetComponent<NewBehaviourScript>().enabled = true;
        }
    }
}
