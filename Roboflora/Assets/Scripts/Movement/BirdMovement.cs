using UnityEngine;
using Cinemachine;

public class BirdController : MonoBehaviour
{
    // Movement settings
    public float moveSpeed = 10f;       // Speed for movement
    public float verticalSpeed = 5f;   // Speed for moving up/down (E/Q keys)
    public float rotationSpeed = 500f; // Speed of rotation towards movement direction

    // Camera control settings
    public float lookSpeed = 5f;       // Speed of camera rotation
    public Transform cameraTransform;  // Reference to the camera transform
    private CinemachineVirtualCamera virtualCamera;

    private CharacterController characterController; // CharacterController for physics-based movement
    private Vector3 yVelocity = Vector3.zero;        // For handling gravity
    private float rotationX = 0f;                    // Track the camera's X rotation (up/down)
    private float rotationY = 0f;                    // Track the camera's Y rotation (left/right)

    void Start()
    {
        // Initialize components
        characterController = GetComponent<CharacterController>();
        virtualCamera = cameraTransform.GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        HandleMovement();
        HandleCameraControl();
    }

    void HandleMovement()
    {
        // Get input for forward/backward movement (W/S)
        float moveVertical = Input.GetAxis("Vertical");

        // Get input for left/right movement (A/D)
        float moveHorizontal = Input.GetAxis("Horizontal");

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

        // Combine movement and gravity
        Vector3 velocity = movementDirection * moveSpeed + yVelocity;

        // Move the character
        characterController.Move(velocity * Time.deltaTime);

        // Rotate the bird to face the movement direction
        if (movementDirection != Vector3.zero && !Input.GetMouseButton(1))
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void HandleCameraControl()
    {
        // Camera rotation with right mouse button
        if (Input.GetMouseButton(1))
        {
            // Get mouse movement input
            float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

            // Calculate new rotation values
            rotationX -= mouseY;
            rotationY += mouseX;

            // Clamp vertical rotation to prevent flipping
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);

            // Apply rotation to the camera
            cameraTransform.localEulerAngles = new Vector3(rotationX, rotationY, 0f);
        }
    }
}
