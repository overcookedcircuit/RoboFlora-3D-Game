using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public float moveSpeed = 10f;      // Speed for movement
    public float verticalSpeed = 5f;   // Speed for moving up/down (E/Q keys)
    public float rotationSpeed = 500f; // Speed of rotation towards movement direction
    public Transform cameraTransform;  // Reference to the camera transform

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        // Get input for forward/backward movement (W/S)
        float moveVertical = Input.GetAxis("Vertical"); // W/S for forward/backward

        // Get input for left/right movement (A/D)
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D for left/right movement

        // Get input for ascending (E) and descending (Q)
        float moveUp = 0f;
        if (Input.GetKey(KeyCode.E))
        {
            moveUp = 1f; // E for up
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            moveUp = -1f; // Q for down
        }

        // Calculate camera-relative directions
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0f; // Flatten the forward vector to avoid tilting
        cameraForward.Normalize();
        
        Vector3 cameraRight = cameraTransform.right;
        cameraRight.y = 0f; // Flatten the right vector to avoid tilting
        cameraRight.Normalize();

        // Calculate the movement direction based on the camera's orientation
        Vector3 movementDirection = (cameraForward * moveVertical + cameraRight * moveHorizontal).normalized;
        movementDirection += Vector3.up * moveUp;

        // Move the bird in the specified direction
        transform.Translate(movementDirection * moveSpeed * Time.deltaTime, Space.World);
        // Rotate the bird to face the movement direction
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
