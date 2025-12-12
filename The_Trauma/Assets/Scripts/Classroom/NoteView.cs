using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class NoteView : MonoBehaviour
{

    public GameObject openText;
    public GameObject NoteUI;
    public bool inReach;
    public bool isOpen;
    void Start()
    {
        isOpen = false;
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
            if (isOpen)
            {
                NoteUI.SetActive(false);
            }
            else
            {
                NoteUI.SetActive(true);
            }
        }
    }
}
