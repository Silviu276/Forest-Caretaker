using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController playerController;

    private Vector3 movement;
    public float speed = 5f;

    private Vector3 velocity;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance;
    private bool grounded;

    private void Update()
    {
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (grounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        movement = transform.right * x + transform.forward * z;
        velocity.y += gravity * Time.deltaTime;

        playerController.Move(movement * speed * Time.deltaTime);
        playerController.Move(velocity * Time.deltaTime);
    }
}
