using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassettePlay : MonoBehaviour
{
    [Header("Refs")]
    public Animator casette;
    public GameObject playText;
    public GameObject stopText; // kullanmýyoruz ama referans kalabilir
    public AudioSource cassetteMusic;
    public AudioSource insertSound;
    public AudioSource removeSound;
    public MyDialogueScript dialogueScript;

    [Header("Trigger")]
    public Collider triggerCollider;

    [Header("State")]
    public bool inReach;
    public bool IsPlay;                    // anlýk çalýyor mu
    public bool interactionEnabled = false; // sadece dia6 sonrasý true olacak
    public bool hasEverBeenInserted = false; // tek kullanýmlýk kilit

    [Header("Broken Tape Settings")]
    public bool brokenTapeMode = true;     // senaryoda bozuk kaset
    public float startOffsetSec = 15f;     // 15. saniyeden baþlat
    public float playDurationSec = 15f;    // 15 saniye çalsýn
    public float ejectAnimDuration = 1f;   // çýkýþ animasyonu süresi

    void Start()
    {
        if (triggerCollider == null) triggerCollider = GetComponent<Collider>();
        inReach = false;
        IsPlay = false;

        // oyun baþýnda tam kilitli
        interactionEnabled = false;
        hasEverBeenInserted = false;

        if (playText) playText.SetActive(false);
        if (stopText) stopText.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!interactionEnabled || hasEverBeenInserted) return;
        if (other.CompareTag("Reach"))
        {
            inReach = true;
            if (playText) playText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = false;
            if (playText) playText.SetActive(false);
            if (stopText) stopText.SetActive(false);
        }
    }

    void Update()
    {
        // tek kullanýmlýk kilit + dia6 bekleme
        if (!interactionEnabled || hasEverBeenInserted) return;

        if (inReach && Input.GetButtonDown("Interact"))
        {
            // ilk ve tek takýþ; manuel çýkarma yok
            hasEverBeenInserted = true;      // anýnda kilitle
            if (playText) playText.SetActive(false);
            if (stopText) stopText.SetActive(false);
            if (triggerCollider) triggerCollider.enabled = false; // tekrar etkileþim olmasýn

            StartCoroutine(PlayOnceRoutine());
        }
    }

    IEnumerator PlayOnceRoutine()
    {
        // takma animasyonu
        casette.SetBool("Play", true);
        casette.SetBool("Stop", false);
        if (insertSound) insertSound.Play();

        // takma animasyonunu bekle (uygun ise anim event ile deðiþtir)
        yield return new WaitForSeconds(2f);

        // müziði baþlat
        if (brokenTapeMode)
        {
            if (cassetteMusic)
            {
                cassetteMusic.time = Mathf.Clamp(startOffsetSec, 0f, cassetteMusic.clip.length - 0.1f);
                cassetteMusic.Play();
                IsPlay = true;
            }

            // bozuk kaset: kýsýtlý süre çal
            yield return new WaitForSeconds(playDurationSec);

            // otomatik çýkar + bitir
            yield return StartCoroutine(EjectAndFinish());
        }
        else
        {
            if (cassetteMusic)
            {
                cassetteMusic.Play();
                IsPlay = true;
            }
        }
    }

    IEnumerator EjectAndFinish()
    {
        // müziði durdur
        if (cassetteMusic && cassetteMusic.isPlaying)
            cassetteMusic.Stop();
        IsPlay = false;

        // çýkýþ animasyonu
        casette.SetBool("Play", false);
        casette.SetBool("Stop", true);
        if (removeSound) removeSound.Play();

        // animasyonun tamamlanmasýný bekle
        yield return new WaitForSeconds(ejectAnimDuration);

        // tamamen devre dýþý
        if (playText) playText.SetActive(false);
        if (stopText) stopText.SetActive(false);
        inReach = false;

        // bu scripti ve tetikleyiciyi kapat
        if (triggerCollider) triggerCollider.enabled = false;
        this.enabled = false;

        // sonraki diyalog
        if (dialogueScript != null)
            dialogueScript.diaLine7();
    }

    /// <summary>
    /// Dia6 sonrasýnda MyDialogueScript çaðýracak.
    /// Oyuncu hâlihazýrda trigger içindeyse prompt’u açýyoruz.
    /// </summary>
    public void EnableAfterDialogue6()
    {
        if (this.enabled == false) return; // her ihtimale karþý
        interactionEnabled = true;
        if (inReach && !hasEverBeenInserted && playText)
            playText.SetActive(true);
    }
}
