using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public FixedJoystick moveJoystick;
    public Animator anim;
    public float speed;
    // Update is called once per frame
    public Rigidbody2D rb;

    private float moveH, moveV;
    private InteractableObjectClassroom currentInteractable;
    private InteractableObjectKeyPad currentKeyPad;
    private InteractableObject89 currentInteractable89;
    private InteractbleObject currentInteractable2;
    private Boss_Interaction currentInteractable3;
    private InteractableFinal currentInteractable4;
    private PortaFinal currentInteractable5;
    private Final_Interaction currentInteractable6;
    private Patrol currentInteractable7;


    void Awake() {
        
    }
    void Update()
    {
        // Check for the physical E key press
        if (Input.GetKeyDown(KeyCode.E))
        {
            HandleEKeyPress();
        }

        moveH = moveJoystick.Horizontal;
        moveV = moveJoystick.Vertical;
        
        Vector3 movement = new Vector3(moveH, moveV, 0f);
        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);

        rb.velocity = new Vector2(movement.x * speed, movement.y * speed);
    }

    public void HandleEKeyPress()
    {
        // This is the logic that happens when E is pressed
        Debug.Log("E key press handled!");
    }

    public void SetCurrentInteractable(InteractableObjectClassroom interactable)
    {
        currentInteractable = interactable;
    }
    public InteractableObjectClassroom GetCurrentInteractable()
    {
        return currentInteractable;
    }
    public void SetCurrentKeypadInteractable(InteractableObjectKeyPad keypad)
    {
        currentKeyPad = keypad;
    }
    public InteractableObjectKeyPad GetCurrentKeyPad()
    {
        return currentKeyPad;
    }
    public InteractableObject89 GetCurrentInteractable89()
    {
        return currentInteractable89;
    }
    public void SetCurrentInteractable89(InteractableObject89 interactable89)
    {
        currentInteractable89 = interactable89;
    }
    public void SetCurrentInteractable2(InteractbleObject interactable2)
    {
        currentInteractable2 = interactable2;
    }
    public InteractbleObject GetCurrentInteractable2()
    {
        return currentInteractable2;
    }
    public void SetCurrentInteractable3(Boss_Interaction interactable3)
    {
        currentInteractable3 = interactable3;
    }
    public Boss_Interaction GetCurrentInteractable3()
    {
        return currentInteractable3;
    }
    public void SetCurrentInteractable4(InteractableFinal interactable4)
    {
        currentInteractable4 = interactable4;
    }
    public InteractableFinal GetCurrentInteractable4()
    {
        return currentInteractable4;
    }
    public void SetCurrentInteractable5(PortaFinal interactable5)
    {
        currentInteractable5 = interactable5;
    }
    public PortaFinal GetCurrentInteractable5()
    {
        return currentInteractable5;
    }
    public void SetCurrentInteractable6(Final_Interaction interactable6)
    {
        currentInteractable6 = interactable6;
    }
    public Final_Interaction GetCurrentInteractable6()
    {
        return currentInteractable6;
    }
    public void SetCurrentInteractable7(Patrol interactable7)
    {
        currentInteractable7 = interactable7;
    }
    public Patrol GetCurrentInteractable7()
    {
        return currentInteractable7;
    }


    public void OnInteractButtonPressed()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interagir();
        }
        if(currentKeyPad != null)
        {
            currentKeyPad.Interact();
        }
        if(currentInteractable89 != null)
        {
            currentInteractable89.Interact();
        }
        if(currentInteractable2 != null)
        {
            currentInteractable2.Interact();
        }
        if(currentInteractable3 != null)
        {
            currentInteractable3.Interact();
        }
        if(currentInteractable4 != null)
        {
            currentInteractable4.ShowPasswordPanel();
        }
        if(currentInteractable5 != null)
        {
            currentInteractable5.Interact();
        }
        if(currentInteractable6 != null)
        {
            currentInteractable6.Interact();
        }
        if(currentInteractable7 != null)
        {
            currentInteractable7.Interact();
        }
    }
}
