using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public float moveSpeed = 10f;      // Speed for forward/backward movement
    public float rotationSpeed = 100f; // Speed for rotating left/right
    public float verticalSpeed = 5f;   // Speed for moving up/down (E/Q keys)

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        // Get input for forward/backward movement (W/S)
        float moveVertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime; // W/S for forward/backward

        // Get input for rotation (A/D)
        float moveHorizontal = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime; // A/D for left/right rotation

        // Get input for ascending (E) and descending (Q)
        float moveUp = 0f;
        if (Input.GetKey(KeyCode.E))
        {
            moveUp = verticalSpeed * Time.deltaTime; // E for up
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            moveUp = -verticalSpeed * Time.deltaTime; // Q for down
        }

        // Move the bird forward/backward and up/down
        transform.Translate(Vector3.forward * moveVertical); // Forward/backward
        transform.Translate(Vector3.up * moveUp);            // Up/down

        // Rotate the bird left/right
        transform.Rotate(Vector3.up * moveHorizontal);       // Rotate based on A/D
    }
}
