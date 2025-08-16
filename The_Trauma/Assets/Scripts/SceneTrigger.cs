using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; // Inspector’dan ayarlayacaksýn

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SaveManager.SaveScene(sceneToLoad);

            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
