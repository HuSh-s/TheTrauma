using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public AudioSource audioSource;
    public TextMeshProUGUI subtitleText;

    public void PlayDialogue(AudioClip clip, string subtitle, float duration)
    {
        StartCoroutine(PlayLine(clip, subtitle, duration));
    }

    private IEnumerator PlayLine(AudioClip clip, string subtitle, float duration)
    {
        subtitleText.text = subtitle;
        subtitleText.gameObject.SetActive(true);
        audioSource.clip = clip;
        audioSource.Play();

        yield return new WaitForSeconds(duration);

        subtitleText.text = "";
        subtitleText.gameObject.SetActive(false);
    }
}
