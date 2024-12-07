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
    void Update()
    {
        // Check for the physical E key press
        if (Input.GetKeyDown(KeyCode.E))
        {
            HandleEKeyPress();
        }

        // moveH = Input.GetAxis("Horizontal");
        // moveV = Input.GetAxis("Vertical");

        moveH = moveJoystick.Horizontal;
        moveV = moveJoystick.Vertical;
        
        Vector3 movement = new Vector3(moveH, moveV, 0f);
        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);

        // transform.position += movement * speed * Time.deltaTime;
        // rb.MovePosition(transform.position + movement * speed * Time.deltaTime);
        rb.velocity = new Vector2(movement.x * speed, movement.y * speed);
    }

    public void HandleEKeyPress()
    {
        // This is the logic that happens when E is pressed
        Debug.Log("E key press handled!");
    }
}
