using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Manager : MonoBehaviour
{
    public GameObject _Player;
    public GameObject _Car;
    public Camera _Camera;
    public GameObject SittingPoint;
    void Start()
    {/*
        _Player.GetComponent<Player>().enabled = false;
        _Player.GetComponent<CapsuleCollider>().enabled = false;
        _Player.GetComponent<CharacterController>().enabled = false;
        _Camera.fieldOfView = 90f;

        Vector3 flatRotation = SittingPoint.transform.eulerAngles;
        _Player.transform.rotation = Quaternion.Euler(0f, flatRotation.y, 0f);
        _Player.transform.position = SittingPoint.transform.position;
        _Player.transform.SetParent(SittingPoint.transform);

        //Activate Script
        _Player.GetComponent<PlayerCarController>().enabled = true;
        _Car.GetComponent<CarAutoDrive>().enabled = true;*/
    }

    void Update()
    {
        
    }
}
