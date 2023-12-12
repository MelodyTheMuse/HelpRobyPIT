using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Animator animator;

    void Start()
    {
        // Get the Animator component attached to the player
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Get input from arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Move the player
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // Activate walking animation if speed is greater than 1
        if (movement.magnitude > 0.1f)
        {
            animator.SetFloat("movement", 1f);
        }
        else
        {
            animator.SetFloat("movement", 0f);
        }
    }
}
