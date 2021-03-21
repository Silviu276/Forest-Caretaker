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
    public float jumpSpeed = 6f;

    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance;
    private bool grounded;

    public Animator axeAnimator;

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
        playerController.Move(velocity * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift) && movement != Vector3.zero)
        {
            playerController.Move(movement * speed * 3 * Time.deltaTime);
            axeAnimator.SetFloat("movingSpeed", 2f);
            axeAnimator.SetBool("moving", true);
        }
        else if (!Input.GetKey(KeyCode.LeftShift) && movement != Vector3.zero)
        {
            playerController.Move(movement * speed * Time.deltaTime);
            axeAnimator.SetFloat("movingSpeed", 1f);
            axeAnimator.SetBool("moving", true);
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetAxis("Mouse ScrollWheel") != 0f) && grounded)
        {
            velocity.y = jumpSpeed;
        }
    }
}
