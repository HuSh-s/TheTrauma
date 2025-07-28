using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1Manager : MonoBehaviour
{
    [Header("Player")]
    public GameObject _Player;
    public Camera _Camera;
    [Header("Car")]
    public GameObject _Car;
    public GameObject sittingPoint;
    public GameObject exitPoint;
    [Header("FadeScreen")]
    public Animator fadeAnimator;
    public GameObject fadeCanvas;
    //public GameObject UIElements;

    public AudioSource CarRainSound;


    void Start()
    {
        PlayFadeIn();
        CharInCarActivate();
        CarRainSound.Play();
    }

    void Update()
    {
        
    }

    //Car In and Out
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

    //ScreenFade
    void PlayFadeIn()
    {
        fadeCanvas.SetActive(true);
        //UIElements.SetActive(false);

        fadeAnimator.SetBool("FadeIn",true);
        Invoke(nameof(DisableFadeCanvas), 2f);
    }
    void DisableFadeCanvas()
    {
        fadeCanvas.SetActive(false);
        //UIElements.SetActive(true);

        fadeAnimator.SetBool("FadeIn", false);
    }
}
