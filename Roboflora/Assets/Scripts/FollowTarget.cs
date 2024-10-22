using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    // Start is called before the first frame update

    public float rotationSpeed = 0f;
    public float topClamp = 70f;
    public float bottomClamp = -40f;
    public Transform followTarget;

    // cinemachine
    private float cinemachineTargetYaw;
    private float cinemachineTargetPitch;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        CameraInput();
    }


    private void ApplyRotation(float pitch, float yaw)
    {
        followTarget.rotation = Quaternion.Euler(pitch, yaw, followTarget.eulerAngles.z);
    }

    private void CameraInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        
        cinemachineTargetPitch = UpdateRotation(cinemachineTargetPitch, mouseY, bottomClamp, topClamp, true);
        cinemachineTargetYaw = UpdateRotation(cinemachineTargetYaw, mouseX, float.MinValue, float.MaxValue, false);

        ApplyRotation(cinemachineTargetPitch, cinemachineTargetYaw);
    }

    private float UpdateRotation(float currentRotation, float input, float min, float max, bool isXAsis)
    {
        currentRotation += isXAsis ? -input : input;
        return Mathf.Clamp(currentRotation, min, max);
    }
}
