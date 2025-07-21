using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassettePlay : MonoBehaviour
{
    public Animator casette;
    public GameObject playText;
    public AudioSource cassetteMusic;
    public bool inReach;
    public bool IsPlay;

    void Start()
    {
        inReach = false;
        IsPlay = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            playText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            playText.SetActive(false);
        }
    }

    void Update()
    {
        if (inReach && Input.GetButtonDown("Interact"))
        {
            if (!IsPlay)
            {
                PlayMusic();
                IsPlay = true;
            }
            else
            {
                StopMusic();
                IsPlay = false;
            }

            playText.SetActive(false);
        }
    }
    void PlayMusic()
    {
        casette.SetBool("Play", true);
        casette.SetBool("Stop", false);
        cassetteMusic.Play();
    }

    void StopMusic()
    {
        casette.SetBool("Play", false);
        casette.SetBool("Stop", true);
        cassetteMusic.Stop();
    }
}
