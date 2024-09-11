using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 700f; // Sensitivity of the mouse input
    public Transform playerBody; // Reference to the player body (or any object the camera is attached to)
    public float verticalClamp = 80f; // Maximum vertical angle to prevent flipping

    private float xRotation = 0f; // Current rotation around the x-axis

    void Update()
    {
        if (Input.GetMouseButton(1)) // Right mouse button is held down
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // Rotate the camera around the x-axis (vertical rotation)
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -verticalClamp, verticalClamp); // Clamp the rotation to prevent flipping
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // Rotate the player body around the y-axis (horizontal rotation)
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}