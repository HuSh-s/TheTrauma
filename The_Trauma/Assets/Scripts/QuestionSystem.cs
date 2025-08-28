using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuestionSystem : MonoBehaviour
{
    public GameObject yesText;
    public GameObject noText;      
    public AudioSource yesSound;
    public AudioSource noSound;
    public SitChair _SitChair;

    public bool InReach;
    private Collider currentButton;   // hangi butonla çarpýþýyoruz

    void Update()
    {
        if (_SitChair.IsSitting && InReach && Input.GetKeyDown(KeyCode.Q))
        {
            if (currentButton.CompareTag("Yes"))
            {
                yesSound.Play();
            }
            else if (currentButton.CompareTag("No"))
            {
                noSound.Play();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_SitChair.IsSitting && other.CompareTag("Yes"))
        {
            currentButton = other;
            yesText.SetActive(true);
            InReach = true;
        }
        else if (_SitChair.IsSitting && other.CompareTag("No"))
        {
            currentButton = other;
            noText.SetActive(true);
            InReach = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Yes"))
        {
            yesText.SetActive(false);
            InReach = false;
        }
        else if (other.CompareTag("No"))
        {
            noText.SetActive(false);
            InReach = false;
        }
    }
    public void ClearCurrentButton()
    {
        currentButton = null;
        yesText.SetActive(false);
        noText.SetActive(false);
    }
}
