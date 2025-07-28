using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Manager : MonoBehaviour
{
    public GameObject _Player;
    public GameObject _Car;
    public Camera _Camera;
    public GameObject sittingPoint;
    public GameObject exitPoint;
    void Start()
    {
       CharInCarActivate();
    }

    void Update()
    {
        
    }

    void CharInCarActivate()
    {
        _Player.GetComponent<Player>().enabled = false;
        _Player.GetComponent<CapsuleCollider>().enabled = false;
        _Player.GetComponent<CharacterController>().enabled = false;
        _Camera.fieldOfView = 80f;

        Vector3 flatRotation = sittingPoint.transform.eulerAngles;
        _Player.transform.rotation = Quaternion.Euler(0f, flatRotation.y, 0f);
        _Player.transform.position = sittingPoint.transform.position;
        _Player.transform.SetParent(sittingPoint.transform);

        //Activate Script
        _Player.GetComponent<PlayerCarController>().enabled = true;
        _Car.GetComponent<CarAutoDrive>().enabled = true;
    }

    void CharOutCarActivate()
    {
        _Player.GetComponent<Player>().enabled = true;
        _Player.GetComponent<CapsuleCollider>().enabled = true;
        _Player.GetComponent<CharacterController>().enabled = true;
        _Camera.fieldOfView = 70f;

        Vector3 flatRotation = exitPoint.transform.eulerAngles;
        _Player.transform.rotation = Quaternion.Euler(0f, flatRotation.y, 0f);
        _Player.transform.position = exitPoint.transform.position;
        _Player.transform.SetParent(sittingPoint.transform);

        //Activate Script
        _Player.GetComponent<PlayerCarController>().enabled = false;
        _Car.GetComponent<CarAutoDrive>().enabled = false;
    }

}
