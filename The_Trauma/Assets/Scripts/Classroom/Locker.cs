using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker : MonoBehaviour
{
    public Animator locker;
    public GameObject openText;
    public GameObject LockUI;

    public AudioSource LockerSoundOpen;
    public AudioSource LockerSoundClose;

    public bool inReach;

    public bool isLocked = true;
    public int[] correctCode = new int[3];
    public int[] enteredCode = new int[3];
    public bool isCodePanelOpen = false;

    void Start()
    {
        inReach = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = true;
            openText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = false;
            openText.SetActive(false);
        }
    }

    void Update()
    {
        if (inReach && Input.GetButtonDown("Interact"))
        {
            if (isLocked)
            {
                OpenCodePanel();
            }
            else
            {
                ToggleDoor();
            }
        }
    }
    void OpenCodePanel()
    {
        if (!isCodePanelOpen)
        {
            isCodePanelOpen = true;
            LockUI.SetActive(true);

            // Buraya: þifre UI açma kodunu yazacaksýn.
        }
    }
    public void CheckCode()
    {
        if (enteredCode[0] == correctCode[0] &&
            enteredCode[1] == correctCode[1] &&
            enteredCode[2] == correctCode[2])
        {
            Debug.Log("Doðru þifre! Kilit açýldý.");
            isLocked = false;
            isCodePanelOpen = false;

            // Ýstersen kilit açýlma sesi ekleyebilirsin
        }
        else
        {
            Debug.Log("Yanlýþ þifre!");
        }
    }
    void ToggleDoor()
    {
        bool isOpen = locker.GetBool("Open");

        if (isOpen)
            LockerCloses();
        else
            LockerOpens();
    }
    void LockerOpens()
    {
        locker.SetBool("Open", true);
        locker.SetBool("Closed", false);
        LockerSoundOpen.Play();
    }
    void LockerCloses()
    {
        locker.SetBool("Open", false);
        locker.SetBool("Closed", true);
        LockerSoundClose.Play();
    }
}
