using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusEffect : MonoBehaviour
{
    public Transform targetObject;
    public Transform _CamerMain;
    public AudioSource horrorSound;
    public GameObject _Player;

    private bool isFocusing = false;

    public void FocusObject(float duration)
    {
        if (!isFocusing)
            StartCoroutine(FocusRoutine(duration));
    }

    IEnumerator FocusRoutine(float duration)
    {
        isFocusing = true;

        _Player.GetComponent<PlayerCarController>().enabled = false;
        //player.GetComponent<PlayerMovement>().enabled = false;

        horrorSound.Play();

        // Hedefe dön
        float elapsed = 0f;
        while (elapsed < duration)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetObject.position - _CamerMain.position);
            _CamerMain.rotation = Quaternion.Slerp(_CamerMain.rotation, targetRotation, Time.deltaTime * 5f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        _Player.GetComponent<PlayerCarController>().enabled = true;
        //player.GetComponent<PlayerMovement>().enabled = true;

        isFocusing = false;
    }
}
