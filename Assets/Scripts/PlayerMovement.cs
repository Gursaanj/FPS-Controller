using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float gravity = -9.81f;
    private CharacterController controller;
    private float horizontalAxis;
    private float verticalAxis;
    private float minSlopeLimit = 45.0f;
    private float maxSlopeLimit = 100.0f;
    private Vector3 horizontalMovement;
    private Vector3 verticalMovement;
    private Vector3 movement;
    private Vector3 velocity;
    private bool isGrounded;
    private const float groundedVelocity = -2f;

    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponentInChildren<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        horizontalAxis = Input.GetAxis("Horizontal");
        verticalAxis = Input.GetAxis("Vertical");

        Move();
        VerticalMotion();
        Jump();
    }

    private void Move()
    {
        horizontalMovement = transform.right * horizontalAxis;
        verticalMovement = transform.forward * verticalAxis;
        movement = Vector3.Normalize(horizontalMovement + verticalMovement);

        controller.Move(movement * speed * Time.deltaTime);
    }

    private void VerticalMotion()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(0.5f * velocity * Time.deltaTime);
        ResetVerticalVelocity();
    }

    private void ResetVerticalVelocity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            //to ensure that the player is forced down on the ground
            controller.slopeLimit = minSlopeLimit;
            velocity.y = groundedVelocity;
        }

        if ((controller.collisionFlags & CollisionFlags.Above) != 0)
        {
            velocity.y = groundedVelocity;
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            controller.slopeLimit = maxSlopeLimit;
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }
}
