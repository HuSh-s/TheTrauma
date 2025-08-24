using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamineObjectDarkRoom : MonoBehaviour
{
    public GameObject ExamineText;
    public GameObject ExaminableObj;

    public bool InReach;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Reach"))
        {
            InReach = true;
            ExamineText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Reach"))
        {
            InReach = false;
            ExamineText.SetActive(false);
        }
    }
    void Start()
    {

    }

    void Update()
    {
        if (InReach && Input.GetKeyDown(KeyCode.E))
        {
            ExamineManagerDarkRoom.Instance.ShowObject(ExaminableObj);
        }
    }
}
