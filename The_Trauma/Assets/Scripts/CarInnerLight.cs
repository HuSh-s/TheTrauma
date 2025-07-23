using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInnerLight : MonoBehaviour
{
    public GameObject[] FrontLights;
    public GameObject LightModel;
    public Material LightOffMaterial;
    public Material LightOnMaterial;
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
        SetLightModelMaterial();
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

            SetLightModelMaterial();

            switchClick.Play();
        }
    }

    void SetLightModelMaterial()
    {
        if (LightModel != null)
        {
            Renderer renderer = LightModel.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = lightsAreOn ? LightOnMaterial : LightOffMaterial;
            }
        }
    }
}
