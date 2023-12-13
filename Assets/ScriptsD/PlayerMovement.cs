using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    Animator animator;
    public float speed = 12f;

    public void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z + new Vector3(0, -9.8f, 0);

        controller.Move(move * speed * Time.deltaTime);
        if(move.magnitude > 0.1f)
        {
            animator.SetFloat("movement", 1f);
        }
        else
        {
            animator.SetFloat("movement", 0f);
        }
    }

}
    

