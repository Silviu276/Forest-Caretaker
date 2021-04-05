using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController playerController;

    private Vector3 movement;
    public float speed = 5f;

    private Vector3 velocity; // physics movement
    public float gravity = -9.81f;
    public float jumpSpeed = 6f;

    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance;
    private bool grounded;

    public Animator axeAnimator;

    private void Update()
    {
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (grounded && velocity.y < 0f) // player is grounded
        {
            velocity.y = -2f; // reset physics movement
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        movement = transform.right * x + transform.forward * z; // movement based on input
        velocity.y += gravity * Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime); // moves player according to gravity formula;

        if (Input.GetKey(KeyCode.LeftShift) && movement != Vector3.zero) // sprinting
        {
            playerController.Move(movement * speed * 3 * Time.deltaTime);
            axeAnimator.SetFloat("movingSpeed", 2f);
            axeAnimator.SetBool("moving", true);
        }
        else if (!Input.GetKey(KeyCode.LeftShift) && movement != Vector3.zero) // walking
        {
            playerController.Move(movement * speed * Time.deltaTime);
            axeAnimator.SetFloat("movingSpeed", 1f);
            axeAnimator.SetBool("moving", true);
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetAxis("Mouse ScrollWheel") != 0f) && grounded) // jump
        {
            velocity.y = jumpSpeed;
        }
    }
}
