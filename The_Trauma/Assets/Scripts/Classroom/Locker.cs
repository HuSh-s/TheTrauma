using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Locker : MonoBehaviour
{
    [Header("Door")]
    public Animator locker;
    public AudioSource LockerSoundOpen;
    public AudioSource LockerSoundClose;

    [Header("Lock")]
    public Animator lockAnimator;
    public GameObject lockObj;
    public AudioSource LockOpensSound;
    public AudioSource lockedsound;

    [Header("UI")]
    public GameObject openText;
    public GameObject LockUI;
    public TextMeshProUGUI codeText;
    public GameObject LockedText;

    [Header("Lock Settings")]
    public bool isLocked = true;
    public int[] correctCode = new int[3] { 5, 3, 8 };

    private int[] enteredCode = new int[3];
    private int currentIndex = 0;
    private bool inReach = false;
    private bool isCodePanelOpen = false;

    void Start()
    {
        openText.SetActive(false);
        LockUI.SetActive(false);
        LockedText.SetActive(false);
        ResetCode();
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
                OpenCodePanel();
            else
                ToggleDoor();
        }

        if (isCodePanelOpen)
        {
            ReadNumberInput();

            if (Input.GetKeyDown(KeyCode.Tab))
                CloseCodePanel();
        }
    }

    void OpenCodePanel()
    {
        isCodePanelOpen = true;
        LockUI.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        ResetCode();
    }

    void ReadNumberInput()
    {
        for (int i = 0; i <= 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i) ||
                Input.GetKeyDown(KeyCode.Keypad0 + i))
            {
                EnterNumber(i);
                break;
            }
        }
    }

    void EnterNumber(int number)
    {
        if (currentIndex >= 3) return;

        enteredCode[currentIndex] = number;
        currentIndex++;

        UpdateCodeText();

        if (currentIndex == 3)
            CheckCode();
    }

    void UpdateCodeText()
    {
        codeText.text =
            enteredCode[0].ToString() +
            enteredCode[1].ToString() +
            enteredCode[2].ToString();
    }

    void ResetCode()
    {
        currentIndex = 0;
        enteredCode = new int[3] { 0, 0, 0 };
        UpdateCodeText();
    }

    void CheckCode()
    {
        if (enteredCode[0] == correctCode[0] &&
            enteredCode[1] == correctCode[1] &&
            enteredCode[2] == correctCode[2])
        {
            isLocked = false;

            if (lockAnimator != null)
            {
                lockAnimator.SetTrigger("Unlock");
                LockOpensSound.Play();

                // Kilit animasyonu bittikten sonra objeyi kaldýr
                StartCoroutine(RemoveLockAfterAnimation());
            }

            CloseCodePanel();
        }
        else
        {
            ResetCode();
            lockedsound.Play();

            // Yanlýþ þifre yazýsý göster  2 saniye sonra kapat
            StartCoroutine(ShowLockedText());
        }
    }

    IEnumerator ShowLockedText()
    {
        LockedText.SetActive(true);
        yield return new WaitForSeconds(2f);
        LockedText.SetActive(false);
    }

    IEnumerator RemoveLockAfterAnimation()
    {
        yield return new WaitForSeconds(1f);

        if (lockObj != null)
            lockObj.SetActive(false);
    }

    void CloseCodePanel()
    {
        isCodePanelOpen = false;
        LockUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        if (LockerSoundOpen) LockerSoundOpen.Play();
    }

    void LockerCloses()
    {
        locker.SetBool("Open", false);
        locker.SetBool("Closed", true);
        if (LockerSoundClose) LockerSoundClose.Play();
    }
}
