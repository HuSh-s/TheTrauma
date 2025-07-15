using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAutoDrive : MonoBehaviour
{
    public Transform[] waypoints;
    public float maxSpeed = 10f;
    public float rotationSpeed = 5f;
    public float slowDownDistance = 5f;

    [Header("Speed Needle")]
    public Transform speedNeedle;
    public float needleMultiplier = 5f;

    private int currentIndex = 0;
    private float currentSpeed;
    private Vector3 lastPosition;
    void Start()
    {
        currentSpeed = maxSpeed;
        lastPosition = transform.position;
    }

    void Update()
    {

        if (currentIndex >= waypoints.Length) return;

        // Yön belirle
        Vector3 direction = (waypoints[currentIndex].position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Hedefe olan mesafe
        float distanceToTarget = Vector3.Distance(transform.position, waypoints[currentIndex].position);

        // yavaþla
        if (currentIndex == waypoints.Length - 1 && distanceToTarget < slowDownDistance)
        {
            float t = distanceToTarget / slowDownDistance;
            currentSpeed = Mathf.Lerp(0f, maxSpeed, t);
        }
        else
        {
            currentSpeed = maxSpeed;
        }

        // Hareket
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentIndex].position, currentSpeed * Time.deltaTime);

        // Hedef noktaya ulaþtýysa sýradaki waypoint'e geç
        if (distanceToTarget < 1f)
        {
            currentIndex++;
        }

        // Anlýk hýz hesapla
        float movedDistance = Vector3.Distance(transform.position, lastPosition);
        float speedPerSecond = movedDistance / Time.deltaTime;
        lastPosition = transform.position;

        // Ýbre döndürme
        if (speedNeedle != null)
        {
            float clampedSpeed = Mathf.Clamp(speedPerSecond * needleMultiplier, -180f, 0f); // max dönüþ -180 derece
            speedNeedle.localRotation = Quaternion.Euler(0f, 0f, clampedSpeed);
        }
    }
}
