using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DeerTriggerEvent : MonoBehaviour
{
    public Transform deer;               // Geyik objesi
    public Transform cameraTransform;    // Ana kamera
    public AudioSource horrorSound;      // Ses efekti (isteðe baðlý)

    public GameObject _Player;
    private bool triggered = false;
    private Quaternion originalRotation;

    private void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Car"))
        {
            triggered = true;

            // Geyik aktif olsun
            if (deer != null)
                deer.gameObject.SetActive(true);

            StartCoroutine(FocusOnDeer());
            StartCoroutine(DisableDeerAfterDelay(4f));
        }
    }

    IEnumerator FocusOnDeer()
    {
        _Player.GetComponent<PlayerCarController>().enabled = false;

        originalRotation = cameraTransform.rotation;

        float elapsed = 0f;
        float duration = 2f; // Geyik animasyonu süresi

        if (horrorSound != null)
            horrorSound.Play();

        // 4 saniye boyunca her karede geyiðe bak
        while (elapsed < duration)
        {
            Quaternion targetRotation = Quaternion.LookRotation(deer.position - cameraTransform.position);
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, targetRotation, Time.deltaTime * 5f); // daha yumuþak dönüþ
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Kamerayý eski rotasyona döndür
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 2f;
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, originalRotation, t);
            yield return null;
        }
    }

    IEnumerator DisableDeerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (deer != null)
            deer.gameObject.SetActive(false);
        _Player.GetComponent<PlayerCarController>().enabled = true;
    }
}
