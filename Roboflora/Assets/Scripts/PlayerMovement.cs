using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f; // Movement speed
    public float jumpForce = 8f; // Jump force
    public float gravity = -15f; // Gravity force
    public float fallMultiplier = 10f; // Gravity multiplier for when the player is falling
    public float lowJumpMultiplier = 2f; // Gravity multiplier for low jumps (releasing jump early)

    public CharacterController controller; // Reference to the character controller
    private Vector3 velocity; // Store player's velocity
    public Transform groundCheck; // Reference to an empty object checking for ground
    public float groundDistance = 0.4f; // Distance from the ground check object to the ground
    public LayerMask groundMask; // Mask that defines what is considered ground

    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Check if the player is on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Reset velocity if on the ground
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Stick to the ground with a small negative value
        }

        // Get input for movement (WASD or arrow keys)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Move player in relation to camera direction
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Apply movement
        controller.Move(move * speed * Time.deltaTime);

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // Apply gravity
        if (velocity.y < 0) // Falling down
        {
            velocity.y += gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (velocity.y > 0 && !Input.GetButton("Jump")) // Releasing jump early
        {
            velocity.y += gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        // Always apply standard gravity
        velocity.y += gravity * Time.deltaTime;

        // Move player by velocity (for gravity and jumping)
        controller.Move(velocity * Time.deltaTime);
    }
}