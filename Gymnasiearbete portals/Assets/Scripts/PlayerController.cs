using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    // Configurable parameters
    [Header("Movement")]
    [SerializeField] private float movementSpeed = 12f;
    [SerializeField] private float gravity = -9.82f;
    [SerializeField] private float jumpHeight = 3f;

    [Header("Ground Check")]
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    // Private variables
    private float xInput;
    private float zInput;
    private Vector3 movement;
    private Vector3 velocity;
    private bool isGrounded;

    // Cached references
    private CharacterController characterController;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Ground();
        Move();
        Jump();
        Gravity();
    }

    void Ground()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) 
        {
            velocity.y = -2f;
        }
    }

    void Move()
    {
        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

        movement = Vector3.ClampMagnitude((transform.right * xInput) + (transform.forward * zInput), 1f);
        characterController.Move(movement * movementSpeed * Time.deltaTime);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void Gravity()
    {
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }
}

