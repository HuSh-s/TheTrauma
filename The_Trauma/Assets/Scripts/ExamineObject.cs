using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExamineObject : MonoBehaviour
{
    public GameObject ExamineText;
    public GameObject ExaminableObj;

    public bool InReach;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Reach"))
        {
            InReach = true;
            ExamineText.SetActive(false);
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
            ExamineManager.Instance.ShowObject(ExaminableObj);
        }
    }
}
