using UnityEngine;

public class RhinoMovement : MonoBehaviour
{
    public float walkSpeed = 5;
    public float runSpeed = 10;
    public float rotationSpeed = 500;
    public float jumpSpeed = 5;
    public Transform cameraTransform;  // Reference to the camera transform
    public float chargeSpeedStart = 5;
    public float chargeSpeedMax = 7;
    public float chargeRate = 1;  // How quickly the speed increases during charging

    private Animator animator;
    private CharacterController characterController;
    private float ySpeed;
    private bool isCharging = false;
    private float currentChargeSpeed;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        currentChargeSpeed = chargeSpeedStart;
    }

    void Update()
    {
        // Handle charging
        if (Input.GetMouseButton(1)) // Right mouse button held down
        {
            isCharging = true;
            currentChargeSpeed = Mathf.Clamp(currentChargeSpeed + chargeRate * Time.deltaTime, chargeSpeedStart, chargeSpeedMax);
            animator.SetFloat("RhinoSpeed", currentChargeSpeed, 0.1f, Time.deltaTime);
            animator.SetBool("isWalking", true);
        }
        else if (isCharging) // Right mouse button released
        {
            ExecuteCharge();
            isCharging = false;
            currentChargeSpeed = chargeSpeedStart; // Reset charge speed for the next charge
        }
        else
        {
            // Normal movement logic when not charging
            HandleMovement();
        }
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0f;  // Flatten on the Y-axis
        cameraForward.Normalize();

        Vector3 cameraRight = cameraTransform.right;
        cameraRight.y = 0f;  // Flatten on the Y-axis
        cameraRight.Normalize();

        Vector3 movementDirection = (cameraForward * verticalInput + cameraRight * horizontalInput).normalized;

        bool isIdle = movementDirection.magnitude == 0f;
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        animator.SetFloat("RhinoSpeed", isIdle ? 0f : currentSpeed, 0.1f, Time.deltaTime);
        animator.SetBool("isWalking", !isIdle);

        ySpeed += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded)
        {
            ySpeed = -0.5f;

            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpSpeed;
            }
        }

        Vector3 velocity = movementDirection * currentSpeed;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            // Correct the default rotation (adjust Y by 90 degrees for example)
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            Quaternion correctedRotation = toRotation * Quaternion.Euler(0, 90, 0); // Adjust 90 degrees on Y-axis
            transform.rotation = Quaternion.RotateTowards(transform.rotation, correctedRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void ExecuteCharge()
    {
        float chargeMultiplier = (currentChargeSpeed == chargeSpeedMax) ? 300 : 200;
        Vector3 chargeDirection = transform.forward * (currentChargeSpeed * chargeMultiplier);

        // Apply the charge velocity (ignores vertical movement)
        Vector3 velocity = new Vector3(chargeDirection.x, ySpeed, chargeDirection.z);
        characterController.Move(velocity * Time.deltaTime);

        Debug.Log($"Charge executed with speed multiplier: {chargeMultiplier} and speed: {(currentChargeSpeed * chargeMultiplier)}");
    }
}
