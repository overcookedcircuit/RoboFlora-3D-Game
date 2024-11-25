using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5;
    public float runSpeed = 10;
    public float rotationSpeed = 500;
    public float jumpSpeed = 5;
    public Transform cameraTransform;  // Reference to the camera transform

    private Animator animator;
    private CharacterController characterController;
    private float ySpeed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Get camera's forward and right directions (flatten on the Y-axis to prevent the player from tilting)
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0f;  // Zero out Y to keep movement horizontal
        cameraForward.Normalize();
        Vector3 cameraRight = cameraTransform.right;
        cameraRight.y = 0f;  // Zero out Y to keep movement horizontal
        cameraRight.Normalize();

        // Calculate the movement direction based on the camera's orientation
        Vector3 movementDirection = (cameraForward * verticalInput + cameraRight * horizontalInput).normalized;

        // Detect running input (Shift key) and adjust speed
        bool isIdle = movementDirection.magnitude == 0f;
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * currentSpeed;
        
        animator.SetFloat("CharacterSpeed",  isIdle ? 0f : currentSpeed, 0.1f, Time.deltaTime);

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            animator.SetBool("isJumping", false);
            ySpeed = -0.5f;

            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpSpeed;
                animator.SetBool("isJumping", true);
            }
        }

        Vector3 velocity = movementDirection * magnitude;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);

        // Rotate the player towards the movement direction
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
