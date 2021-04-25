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
    private LayerMask groundMask;
    public float groundDistance;
    private bool grounded;

    private Animator activatedAnimator;
    private Animator[] toolsAnimators = new Animator[4];

    // start
    private void Start()
    {
        ToolsAnimatorsInit();
        groundMask = LayerMask.GetMask("Ground");
    }

    // update
    private void Update()
    {
        MovePlayer();
    }

    private void SetAnimatorMovingSpeed(float movingSpeed)
    {
        foreach (Animator toolAnimator in toolsAnimators)
            if (toolAnimator.isActiveAndEnabled)
            {
                activatedAnimator = toolAnimator;
                break;
            }
        if (activatedAnimator != null)
        {
            activatedAnimator.SetFloat("movingSpeed", movingSpeed);
            activatedAnimator.SetBool("moving", true);
        }
    }

    // does all player movement and sets movement animations
    private void MovePlayer()
    {
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (grounded && velocity.y < 0f) // player is grounded
            velocity.y = -2f; // reset physics movement

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        movement = transform.right * x + transform.forward * z; // movement based on input
        velocity.y += gravity * Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime); // moves player according to gravity formula;

        if (Input.GetKey(KeyCode.LeftShift) && movement != Vector3.zero) // sprinting
        {
            playerController.Move(movement * speed * 3 * Time.deltaTime);
            SetAnimatorMovingSpeed(2f);
        }
        else if (!Input.GetKey(KeyCode.LeftShift) && movement != Vector3.zero) // walking
        {
            playerController.Move(movement * speed * Time.deltaTime);
            SetAnimatorMovingSpeed(1f);
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetAxis("Mouse ScrollWheel") != 0f) && grounded) // jump
            velocity.y = jumpSpeed;
    }

    private void ToolsAnimatorsInit()
    {
        toolsAnimators[0] = Camera.main.transform.Find("Axe").GetComponent<Animator>();
        toolsAnimators[1] = Camera.main.transform.Find("WaterCan").GetComponent<Animator>();
        toolsAnimators[2] = Camera.main.transform.Find("Shears").GetComponent<Animator>();
        toolsAnimators[3] = Camera.main.transform.Find("Saplings").GetComponent<Animator>();
    }
}
