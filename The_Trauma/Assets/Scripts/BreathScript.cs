using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class BreathScript : MonoBehaviour
{
    public Animator blackOverlayAnimator; // Ekran için
    public Animator breathTextAnimator;   // "BREATH" yazýsý
    public GameObject hintText;           // "Press SPACE " yazýsý
    public GameObject BreathText;           // "Press breath" yazýsý
    public GameObject Canvas;           // "Press breath" yazýsý
   // public AudioSource breathSound;

    private int breathCount = 0;
    private bool breathSequenceActive = false;

    private void Start()
    {
        hintText.SetActive(false);
        BreathText.SetActive(false);
        Canvas.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            StartBreathSequence();
        }
    }

    public void StartBreathSequence()
    {
        if (!breathSequenceActive)
        {
            StartCoroutine(BreathRoutine());
        }
    }

    IEnumerator BreathRoutine()
    {
        Canvas.SetActive(true);
        breathSequenceActive = true;
        breathCount = 0;

        blackOverlayAnimator.SetTrigger("FadeIn");

        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 0f;

        hintText.SetActive(true);
        BreathText.SetActive(true);

        while (breathCount < 3)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                breathCount++;
                breathTextAnimator.SetTrigger("Breathe");
                // breathSound.Play();
                if (breathCount == 3)
                {
                    yield return new WaitForSecondsRealtime(1.5f);
                }
            }
            yield return null;
        }

        hintText.SetActive(false);
        BreathText.SetActive(false);

        Time.timeScale = 1f;

        blackOverlayAnimator.SetTrigger("FadeOut");

        yield return new WaitForSecondsRealtime(3f);
        Canvas.SetActive(false);
        breathSequenceActive = false;
    }
}
