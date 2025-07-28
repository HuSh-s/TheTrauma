using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheelController : MonoBehaviour
{
    public WheelCollider referenceWheel;
    public Transform steeringWheelTransform;

    public float maxSteeringWheelRotation = 450f;
    public float rotationSmoothSpeed = 5f; // Ne kadar hýzlý yumuþasýn?

    private Quaternion initialLocalRotation;
    private Quaternion currentRotation;

    void Start()
    {
        initialLocalRotation = steeringWheelTransform.localRotation;
        currentRotation = steeringWheelTransform.localRotation;
    }

    void Update()
    {
        float steerAngleNormalized = referenceWheel.steerAngle / 30f;
        steerAngleNormalized = Mathf.Clamp(steerAngleNormalized, -1f, 1f);

        float targetRotation = steerAngleNormalized * maxSteeringWheelRotation;
        Quaternion targetSteerRotation = Quaternion.Euler(0f, -targetRotation, 0f);

        // Hedef rotasyon = baþlangýç rotasyonu + hedef direksiyon dönüþü
        Quaternion targetRotationFinal = initialLocalRotation * targetSteerRotation;

        // Smooth geçiþ yap
        currentRotation = Quaternion.Lerp(currentRotation, targetRotationFinal, Time.deltaTime * rotationSmoothSpeed);

        steeringWheelTransform.localRotation = currentRotation;
    }
}
