using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public float lookSpeed = 5f;   // Speed of camera rotation
    // public float zoomSpeed = 10f;   // Speed of zooming in/out
    // public float minFOV = 5f;     // Minimum zoom (field of view)
    // public float maxFOV = 10f;     // Maximum zoom (field of view)
    private CinemachineVirtualCamera virtualCamera;

    private float rotationX = 0f; // Track the camera's X rotation (up/down)
    private float rotationY = 0f;

    void Start()
    {
        // Get the Cinemachine Virtual Camera component
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // void Update()
    // {
    //     // Camera Rotation with Right Mouse Button
    //     if (Input.GetMouseButton(1))  // Right mouse button is 1
    //     {
    //         // Get mouse movement input
    //         float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
    //         float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

    //         // Calculate new rotation values
    //         rotationX -= mouseY;
    //         rotationY += mouseX;

    //         // Apply the rotation to the camera
    //         transform.localEulerAngles = new Vector3(rotationX, rotationY, 0f);
    //     }

    // }
}