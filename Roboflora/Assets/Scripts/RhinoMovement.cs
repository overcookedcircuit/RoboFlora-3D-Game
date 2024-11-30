using UnityEngine;
using System.Collections;

public class RhinoMovement : MonoBehaviour
{
    public float walkSpeed = 5;
    public float runSpeed = 7;
    public float rotationSpeed = 500;
    public float jumpSpeed = 5;
    public Transform cameraTransform;  // Reference to the camera transform
    public float chargeSpeedStart = 5;
    public float chargeSpeedMax = 10;
    public float chargeRate = 5;  // How quickly the speed increases during charging

    private Animator animator;
    private CharacterController characterController;
    private float ySpeed;
    private bool isCharging = false;
    private float currentChargeSpeed;
    private bool isExecutingCharge = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        currentChargeSpeed = chargeSpeedStart;
    }

    void Update()
    {
        if (isExecutingCharge) return; // Skip input handling if a charge is already being executed

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
            StartCoroutine(ExecuteCharge());
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
        if (isExecutingCharge) return; // Disable movement if the charge is ongoing

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
    IEnumerator ExecuteCharge()
    {
        isExecutingCharge = true;
        float chargeDuration = 2f; // Charge for 2 seconds
        float elapsedTime = 0f;

        // Ensure the charge starts with a minimum speed
        float chargeSpeed = Mathf.Max(currentChargeSpeed, runSpeed + 2); // Ensure charge is always faster than running

        // Calculate the corrected forward direction (90-degree adjustment)
        Vector3 correctedChargeDirection = Quaternion.Euler(0, -90, 0) * transform.forward;

        animator.SetBool("isWalking", true);

        while (elapsedTime < chargeDuration)
        {
            elapsedTime += Time.deltaTime;

            // Apply charge movement in the corrected direction
            Vector3 velocity = correctedChargeDirection * chargeSpeed;
            velocity.y = ySpeed; // Maintain gravity during charge
            characterController.Move(velocity * Time.deltaTime);

            yield return null;
        }

        animator.SetBool("isWalking", false);
        isExecutingCharge = false;
    }


}
