using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DriveCar : MonoBehaviour
{
    public GameObject _Player;
    public GameObject _Car;
    public GameObject DriveText;
    public GameObject SittingPoint;
    public GameObject ExitPoint;
    public Camera _Camera;
    public bool Incar;

    //public Animator SittingCar;

    public bool InReach;

    private void Start()
    {
        InReach = false;
        Incar = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            InReach = true;
            DriveText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            InReach = false;
            DriveText.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (!Incar && InReach)
            {
                Incar = true;
                _Player.GetComponent<Player>().enabled = false;
                _Player.GetComponent<CapsuleCollider>().enabled = false;
                _Player.GetComponent<CharacterController>().enabled = false;
                _Camera.fieldOfView = 70f;
                _Car.GetComponent<CarController>().enabled = true;

                _Player.transform.position = SittingPoint.transform.position;
                _Player.transform.rotation = SittingPoint.transform.rotation;
                _Player.transform.SetParent(SittingPoint.transform);
                DriveText.SetActive(false);
            }
            else if (Incar)
            {
                Incar = false;
                _Player.transform.SetParent(null);

                _Player.transform.position = ExitPoint.transform.position;
                Vector3 flatRotation = ExitPoint.transform.eulerAngles;
                _Player.transform.rotation = Quaternion.Euler(0f, flatRotation.y, 0f);

                _Player.GetComponent<Player>().enabled = true;
                _Player.GetComponent<CapsuleCollider>().enabled = true;
                _Player.GetComponent<CharacterController>().enabled = true;
                _Camera.fieldOfView = 60f;
                _Car.GetComponent<CarController>().enabled = false;

            }
        }
    }
}
