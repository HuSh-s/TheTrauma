using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassettePlay : MonoBehaviour
{
    public Animator casette;
    public GameObject playText;
    public GameObject stopText;
    public AudioSource cassetteMusic;
    public AudioSource insertSound;
    public AudioSource removeSound;
    public bool inReach;
    public bool IsPlay;

    void Start()
    {
        inReach = false;
        IsPlay = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Reach"))
        {
            inReach = true;
            if (!IsPlay)
                playText.SetActive(true);
            else
                stopText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Reach"))
        {
            inReach = false;
            playText.SetActive(false);
            stopText.SetActive(false);
        }
    }

    void Update()
    {
        if (inReach && Input.GetButtonDown("Interact"))
        {
            if (!IsPlay)
            {
                StartCoroutine(PlayMusicDelayed());
                IsPlay = true;
            }
            else
            {
                StopMusic();
                IsPlay = false;
            }

            playText.SetActive(false);
            stopText.SetActive(false);
        }
    }

    IEnumerator PlayMusicDelayed()
    {
        casette.SetBool("Play", true);
        casette.SetBool("Stop", false);

        if (insertSound != null)
            insertSound.Play();

        yield return new WaitForSeconds(2f);

        cassetteMusic.Play();
    }

    void StopMusic()
    {
        casette.SetBool("Play", false);
        casette.SetBool("Stop", true);
        cassetteMusic.Stop();
        removeSound.Play();
    }
}
