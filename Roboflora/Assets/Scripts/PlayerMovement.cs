using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    public float rotationSpeed = 500;
    public float jumpSpeed = 5;
    public float jumpButtonGracePeriod = 3;
    public Transform cameraTransform;  // Reference to the camera transform

    private Animator animator;
    private CharacterController characterController;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Get camera's forward and right directions (flatten on the Y-axis to prevent the player from tilting)
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0f;  // Zero out Y to keep movement horizontal
        cameraForward.Normalize();

        Vector3 cameraRight = cameraTransform.right;
        cameraRight.y = 0f;  // Zero out Y to keep movement horizontal
        cameraRight.Normalize();

        // Calculate the movement direction based on the camera's orientation
        Vector3 movementDirection = (cameraForward * verticalInput + cameraRight * horizontalInput).normalized;

        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = jumpSpeed;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            characterController.stepOffset = 0;
        }

        Vector3 velocity = movementDirection * magnitude;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);

        // Rotate the player towards the movement direction
        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("isWalking", true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}