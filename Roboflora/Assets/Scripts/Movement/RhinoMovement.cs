using UnityEngine;
using System.Collections;
using Cinemachine;

public class RhinoController : MonoBehaviour
{
    // Movement settings
    public float walkSpeed = 5;
    public float runSpeed = 7;
    public float rotationSpeed = 500;
    public float jumpSpeed = 5;
    public float chargeSpeedStart = 5;
    public float chargeSpeedMax = 10;
    public float maxChargeDuration = 2f; // Maximum charge duration

    // Camera settings
    public Transform cameraTransform;  // Reference to the camera transform
    public float lookSpeed = 5f;       // Speed of camera rotation
    private CinemachineVirtualCamera virtualCamera;

    private float rotationX = 0f;      // Track the camera's X rotation (up/down)
    private float rotationY = 0f;      // Track the camera's Y rotation (left/right)

    // Charging settings
    private float chargeRate;
    private float chargeButtonHoldTime = 0f; // Tracks how long the button is held
    private float currentChargeSpeed;
    private bool isCharging = false;
    private bool isExecutingCharge = false;

    public float chargeDamage = 50f;
    public float chargeCollisionRadius = 1.5f;

    public GameObject chargeBar;

    // Components
    private Animator animator;
    private CharacterController characterController;
    private float ySpeed;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        currentChargeSpeed = chargeSpeedStart;

        // Calculate the charge rate to reach max speed in maxChargeDuration
        chargeRate = (chargeSpeedMax - chargeSpeedStart) / maxChargeDuration;

        // Get the Cinemachine Virtual Camera component
        virtualCamera = cameraTransform.GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        if (isExecutingCharge)
        {
            return; // Skip input handling if a charge is being executed
        }

        HandleCameraControl();
        HandleMovementOrCharge();
    }

    void HandleCameraControl()
    {
        if (Input.GetMouseButton(1)) // Right mouse button
        {
            float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

            rotationX -= mouseY;
            rotationY += mouseX;

            // Clamp vertical rotation to prevent flipping
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);

            cameraTransform.localEulerAngles = new Vector3(rotationX, rotationY, 0f);
        }
    }

    void HandleMovementOrCharge()
    {
        // Charging logic
        if (Input.GetMouseButton(2) || Input.GetKey(KeyCode.C))
        {
            chargeBar.GetComponent<ChargeBar>().SetMaxCharge((int)chargeSpeedMax);
            chargeButtonHoldTime += Time.deltaTime;
            isCharging = true;
            currentChargeSpeed = Mathf.Clamp(currentChargeSpeed + chargeRate * Time.deltaTime, chargeSpeedStart, chargeSpeedMax);
            chargeBar.SetActive(true);
            chargeBar.GetComponent<ChargeBar>().SetCharge((int)currentChargeSpeed);
            animator.SetFloat("RhinoSpeed", currentChargeSpeed, 0.2f, Time.deltaTime);
            animator.SetBool("isWalking", true);
        }
        else if (isCharging)
        {
            StartCoroutine(ExecuteCharge(chargeButtonHoldTime));
            isCharging = false;
            chargeButtonHoldTime = 0f;
            currentChargeSpeed = chargeSpeedStart;
            chargeBar.SetActive(false);
        }
        else
        {
            HandleMovement();
        }
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        Vector3 cameraRight = cameraTransform.right;
        cameraRight.y = 0f;
        cameraRight.Normalize();

        Vector3 movementDirection = (cameraForward * verticalInput + cameraRight * horizontalInput).normalized;

        bool isIdle = movementDirection.magnitude == 0f;
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        float currentSpeed = isRunning ? runSpeed : walkSpeed;

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

        float chargeSpeed = Mathf.Max(currentChargeSpeed, runSpeed + 2);
        Vector3 chargeDirection = transform.forward;

        animator.SetBool("isWalking", true);

        while (elapsedTime < chargeDuration)
        {
            elapsedTime += Time.deltaTime;

            Vector3 velocity = chargeDirection * chargeSpeed;
            velocity.y = ySpeed;
            characterController.Move(velocity * Time.deltaTime);

            DetectAndDamageEnemies();

            yield return null;
        }

        animator.SetBool("isWalking", false);
        isExecutingCharge = false;
    }

    void DetectAndDamageEnemies()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, chargeCollisionRadius);

        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                EnemyBaseBehavior enemyBehavior = collider.GetComponent<EnemyBaseBehavior>();
                if (enemyBehavior != null)
                {
                    enemyBehavior.GetHurt((int)chargeDamage);
                }
            }
        }
    }
}
