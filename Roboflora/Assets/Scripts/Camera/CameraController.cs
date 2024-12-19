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

    void Update()
    {
        // Camera Rotation with Right Mouse Button
        if (Input.GetMouseButton(1))  // Right mouse button is 1
        {
            // Get mouse movement input
            float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

            // Calculate new rotation values
            rotationX -= mouseY;
            rotationY += mouseX;

            // Apply the rotation to the camera
            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0f);
        }

        // // Camera Zoom with Mouse Scroll Wheel
        // float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        // if (scrollInput != 0f && virtualCamera != null)
        // {
        //     // Get the current Lens settings of the virtual camera
        //     CinemachineComponentBase componentBase = virtualCamera.GetCinemachineComponent<CinemachineComponentBase>();

        //     // Handle zoom for Perspective or Orthographic cameras
        //     if (componentBase is CinemachineFramingTransposer)
        //     {
        //         CinemachineFramingTransposer framingTransposer = (CinemachineFramingTransposer)componentBase;
        //         framingTransposer.m_CameraDistance -= scrollInput * zoomSpeed;
        //         framingTransposer.m_CameraDistance = Mathf.Clamp(framingTransposer.m_CameraDistance, minFOV, maxFOV);
        //     }
        //     else if (virtualCamera.m_Lens.Orthographic)
        //     {
        //         // Adjust Orthographic size
        //         virtualCamera.m_Lens.OrthographicSize -= scrollInput * zoomSpeed;
        //         virtualCamera.m_Lens.OrthographicSize = Mathf.Clamp(virtualCamera.m_Lens.OrthographicSize, minFOV, maxFOV);
        //     }
        //     else
        //     {
        //         // Adjust Field of View for Perspective cameras
        //         virtualCamera.m_Lens.FieldOfView -= scrollInput * zoomSpeed * 10f;  // Multiply by 10 for a reasonable zoom speed
        //         virtualCamera.m_Lens.FieldOfView = Mathf.Clamp(virtualCamera.m_Lens.FieldOfView, minFOV, maxFOV);
        //     }
        // }
    }
}