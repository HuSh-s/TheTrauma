using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAutoDrive : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentIndex = 0;

    public List<int> brakePoints = new List<int>();

    public WheelCollider frontLeft;
    public WheelCollider frontRight;
    public WheelCollider rearLeft;
    public WheelCollider rearRight;

    public Transform carTransform;
    public float maxSteerAngle = 30f;

    public float maxMotorTorque = 700f;
    public float slowMotorTorque = 500f;
    public float maxBrakeTorque = 500f;
    public float waypointThreshold = 5f;

    public float targetSpeed = 15f; // m/s cinsinden hedef hýz
    public float minSpeed = 5f;     // m/s cinsinden minimum hýz (altýna düþmesin)

    private Rigidbody rb;

    private float currentMotorTorque = 0f;
    private float currentBrakeTorque = 0f;

    public float motorTorqueSmoothSpeed = 5f;
    public float brakeTorqueSmoothSpeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (waypoints.Length == 0) return;

        Transform target = waypoints[currentIndex];
        Vector3 relativeVector = carTransform.InverseTransformPoint(target.position);
        float distanceToWaypoint = relativeVector.magnitude;

        float speed = rb.velocity.magnitude;

        float steer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;

        // Direksiyon açýsýný uygula
        frontLeft.steerAngle = steer;
        frontRight.steerAngle = steer;

        // Hedef hýz (normal veya yavaþlama noktasý için)
        float desiredSpeed = targetSpeed;

        if (brakePoints.Contains(currentIndex))
        {
            desiredSpeed = targetSpeed * 0.5f; // Yavaþlama noktalarýnda hedef hýz yarý yarýya
        }

        // Eðer hýz minimum hýzýn altýndaysa, motoru maksimum güçle çalýþtýr, fren kaldýr
        if (speed < minSpeed)
        {
            currentBrakeTorque = 0f;
            currentMotorTorque = maxMotorTorque;
        }
        else
        {
            // Normal hýz kontrolü
            float speedError = desiredSpeed - speed;

            float targetMotorTorque = 0f;
            float targetBrakeTorque = 0f;

            if (speedError > 0.1f) // Hýz düþükse motor torku ver
            {
                targetMotorTorque = Mathf.Clamp(speedError * 200f, slowMotorTorque, maxMotorTorque);
                targetBrakeTorque = 0f;
            }
            else if (speedError < -0.1f) // Hýz yüksekse fren uygula
            {
                targetBrakeTorque = Mathf.Clamp(-speedError * 300f, 0f, maxBrakeTorque);
                targetMotorTorque = 0f;
            }
            else // Hýz neredeyse hedefte ise ne motor ne fren uygula
            {
                targetMotorTorque = 0f;
                targetBrakeTorque = 0f;
            }

            // Torklarý yumuþak þekilde güncelle
            currentMotorTorque = Mathf.Lerp(currentMotorTorque, targetMotorTorque, Time.fixedDeltaTime * motorTorqueSmoothSpeed);
            currentBrakeTorque = Mathf.Lerp(currentBrakeTorque, targetBrakeTorque, Time.fixedDeltaTime * brakeTorqueSmoothSpeed);
        }

        // Arka tekerlere motor torku uygula
        rearLeft.motorTorque = currentMotorTorque;
        rearRight.motorTorque = currentMotorTorque;

        // Fren uygula (tüm tekerlerde)
        frontLeft.brakeTorque = currentBrakeTorque;
        frontRight.brakeTorque = currentBrakeTorque;
        rearLeft.brakeTorque = currentBrakeTorque;
        rearRight.brakeTorque = currentBrakeTorque;

        // Waypoint kontrolü
        if (distanceToWaypoint < waypointThreshold)
        {
            currentIndex++;
            if (currentIndex >= waypoints.Length)
                currentIndex = 0;
        }
    }
}
