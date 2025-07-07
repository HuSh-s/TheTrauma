using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DriveCar : MonoBehaviour
{
    public GameObject _Player;
    public GameObject _Car;
    public GameObject DriveText;
    public GameObject SittingPoint;
    public Camera _Camera;

    //public Animator SittingCar;

    public bool InReach;

    private void Start()
    {
        InReach = false;
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
        if (InReach && Input.GetButtonDown("Interact"))
        {
            //SittingCar.SetBool("Sit", true);
            _Player.GetComponent<Player>().enabled = false;
            _Player.GetComponent<CapsuleCollider>().enabled = false;
            _Player.GetComponent<CharacterController>().enabled = false;
            _Camera.fieldOfView = 70f;
            _Car.GetComponent<CarController>().enabled = true;
            _Player.transform.position = SittingPoint.transform.position;
            _Player.transform.rotation = SittingPoint.transform.rotation;
            _Player.transform.SetParent(SittingPoint.transform);
        }
    }
}
