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
    public float maxChargeDuration = 2f; // Maximum charge duration

    private Animator animator;
    private CharacterController characterController;
    private float ySpeed;
    private bool isCharging = false;
    private float currentChargeSpeed;
    private bool isExecutingCharge = false;

    public float chargeDamage = 50f; // Damage dealt during charge
    public float chargeCollisionRadius = 1.5f; // Radius for detecting collisions during charge

    private float chargeRate; // Dynamically calculated charge rate
    private float chargeButtonHoldTime = 0f; // Tracks how long the button is held

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        currentChargeSpeed = chargeSpeedStart;

        // Calculate the charge rate to reach max speed in maxChargeDuration
        chargeRate = (chargeSpeedMax - chargeSpeedStart) / maxChargeDuration;
        Debug.Log("RhinoMovement initialized. Calculated charge rate: " + chargeRate);
    }

    void Update()
    {
        if (isExecutingCharge)
        {
            Debug.Log("Currently executing charge. Skipping input handling.");
            return; // Skip input handling if a charge is already being executed
        }

        // Handle charging
        if (Input.GetMouseButton(2) || Input.GetKey(KeyCode.C)) // Middle mouse button or 'C' key held down
        {
            chargeButtonHoldTime += Time.deltaTime; // Accumulate button hold time
            isCharging = true;
            currentChargeSpeed = Mathf.Clamp(currentChargeSpeed + chargeRate * Time.deltaTime, chargeSpeedStart, chargeSpeedMax);
            animator.SetFloat("RhinoSpeed", currentChargeSpeed, 0.2f, Time.deltaTime);
            animator.SetBool("isWalking", true);
            Debug.Log("Charging... Current Charge Speed: " + currentChargeSpeed);
        }
        else if (isCharging) // Middle mouse button or 'C' key released
        {
            Debug.Log("Charge button released. Executing charge.");
            float chargeDuration = Mathf.Min(chargeButtonHoldTime, maxChargeDuration); // Calculate charge duration
            StartCoroutine(ExecuteCharge(chargeDuration));
            isCharging = false;
            chargeButtonHoldTime = 0f; // Reset button hold time
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
        if (isExecutingCharge)
        {
            Debug.Log("Skipping movement handling during charge.");
            return; // Disable movement if the charge is ongoing
        }

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Debug.Log("Horizontal Input: " + horizontalInput + ", Vertical Input: " + verticalInput);

        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0f;  // Flatten on the Y-axis
        cameraForward.Normalize();

        Vector3 cameraRight = cameraTransform.right;
        cameraRight.y = 0f;  // Flatten on the Y-axis
        cameraRight.Normalize();

        Vector3 movementDirection = (cameraForward * verticalInput + cameraRight * horizontalInput).normalized;
        Debug.Log("Movement Direction: " + movementDirection);

        bool isIdle = movementDirection.magnitude == 0f;
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        float currentSpeed;

        if (isRunning) {
            if (PlayerManager.Instance.stamina > 0) {
                PlayerManager.Instance.SetStamina(PlayerManager.Instance.stamina - 10f * Time.deltaTime);
                currentSpeed = runSpeed;
            } else {
                currentSpeed = walkSpeed;
            }
        } else {
            if (PlayerManager.Instance.stamina < PlayerManager.Instance.maxStamina) {
                PlayerManager.Instance.SetStamina(PlayerManager.Instance.stamina + 20f * Time.deltaTime);
            }
            currentSpeed = walkSpeed;
        }

        Debug.Log("Current Speed: " + currentSpeed);
        animator.SetFloat("RhinoSpeed", isIdle ? 0f : currentSpeed, 0.1f, Time.deltaTime);
        animator.SetBool("isWalking", !isIdle);

        ySpeed += Physics.gravity.y * Time.deltaTime;

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

    IEnumerator ExecuteCharge(float chargeDuration)
    {
        isExecutingCharge = true;
        float elapsedTime = 0f;

        // Ensure the charge starts with a minimum speed
        float chargeSpeed = Mathf.Max(currentChargeSpeed, runSpeed + 2); // Ensure charge is always faster than running

        // Calculate the corrected forward direction (90-degree adjustment)
        Vector3 correctedChargeDirection = Quaternion.Euler(0, -90, 0) * transform.forward;

        animator.SetBool("isWalking", true);
        Debug.Log("Charge started. Speed: " + chargeSpeed);

        while (elapsedTime < chargeDuration)
        {
            elapsedTime += Time.deltaTime;

            // Apply charge movement in the corrected direction
            Vector3 velocity = correctedChargeDirection * chargeSpeed;
            velocity.y = ySpeed; // Maintain gravity during charge
            characterController.Move(velocity * Time.deltaTime);

            DetectAndDamageEnemies(); // Check for collisions and damage enemies

            yield return null;
        }

        animator.SetBool("isWalking", false);
        isExecutingCharge = false;
        Debug.Log("Charge ended.");
    }

    void DetectAndDamageEnemies()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, chargeCollisionRadius);
        Debug.Log("Detected " + hitColliders.Length + " colliders within charge radius.");

        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                EnemyBaseBehavior enemyBehavior = collider.GetComponent<EnemyBaseBehavior>();
                if (enemyBehavior != null)
                {
                    enemyBehavior.GetHurt((int)chargeDamage);
                    Debug.Log("Damaged enemy: " + collider.name + " for " + chargeDamage + " damage.");
                }
            }
            else
            {
                Debug.Log("Detected non-enemy object: " + collider.name);
            }
        }
    }
}
