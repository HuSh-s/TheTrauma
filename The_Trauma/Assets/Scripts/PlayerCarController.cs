using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    public Transform playerCamera;
    public float lookSpeed = 2f;
    public float maxYawAngle = 90f;   // Max (Y)
    public float maxPitchAngle = 60f; // Max (X)

    private float currentYaw = 0f;
    private float currentPitch = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        currentYaw += mouseX;
        currentYaw = Mathf.Clamp(currentYaw, -maxYawAngle / 2f, maxYawAngle / 2f);

        currentPitch -= mouseY;
        currentPitch = Mathf.Clamp(currentPitch, -maxPitchAngle, maxPitchAngle);

        playerCamera.localRotation = Quaternion.Euler(currentPitch, currentYaw, 0f);
    }
}
