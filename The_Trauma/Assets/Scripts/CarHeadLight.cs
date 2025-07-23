using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHeadLight : MonoBehaviour
{
    public GameObject[] FrontLights;
    public GameObject lightsText;
    public AudioSource switchClick;

    public bool lightsAreOn;
    public bool inReach;

    void Start()
    {
        inReach = false;
        lightsAreOn = false;

        foreach (var item in FrontLights)
        {
            item.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = true;
            lightsText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = false;
            lightsText.SetActive(false);
        }
    }

    void Update()
    {
        if (inReach && Input.GetButtonDown("Interact"))
        {
            lightsAreOn = !lightsAreOn;

            foreach (var item in FrontLights)
            {
                item.SetActive(lightsAreOn);
            }
            switchClick.Play();
        }
    }
}
