using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    
    public Animator door;
    public GameObject openText;
    public AudioSource doorSoundOpen;
    public AudioSource doorSoundClose;
    public bool inReach;
    private bool isOpen = false;
    void Start()
    {
        inReach = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            openText.SetActive(true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            openText.SetActive(false);
        }
    }
    void Update()
    {
        if (inReach && Input.GetButtonDown("Interact"))
        {
            bool isOpen = door.GetBool("Open");

            if (isOpen)
            {
                DoorCloses();
            }
            else
            {
                DoorOpens();
            }
        }
    }
    void DoorOpens()
    {
        door.SetBool("Open", true);
        door.SetBool("Closed", false);
        doorSoundOpen.Play();

    }
    void DoorCloses()
    {
        door.SetBool("Open", false);
        door.SetBool("Closed", true);
        doorSoundClose.Play();
    }
}

