using UnityEngine;

public class InteractableObjectClassroom : MonoBehaviour
{
    public PanelManagerClassroom panelManagerClassroom;
    public GameObject highlightObject;
    public NewBehaviourScript playerMovement;

    [HideInInspector]
    public bool isInRange = false;

    void Start()
    {
        highlightObject.SetActive(false);
    }

    public void Interagir()
    {
        if (isInRange)
        {
            playerMovement.rb.velocity = Vector2.zero;
            panelManagerClassroom.ShowPanel();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            highlightObject.SetActive(true);

            // Pega a referência do Player e informa qual objeto está no alcance
            NewBehaviourScript playerController = collision.GetComponent<NewBehaviourScript>();
            if (playerController != null)
            {
                Debug.Log("Player is in range");
                playerController.SetCurrentInteractable(this);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            highlightObject.SetActive(false);
            isInRange = false;

            NewBehaviourScript playerController = collision.GetComponent<NewBehaviourScript>();
            if (playerController != null && playerController.GetCurrentInteractable() == this)
            {
                playerController.SetCurrentInteractable(null);
            }
        }
    }
}
