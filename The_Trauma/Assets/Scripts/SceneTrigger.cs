using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SaveManager.SaveScene(sceneToLoad);
            StartCoroutine(LoadSceneNextFrame());
        }
    }

    private IEnumerator LoadSceneNextFrame()
    {
        yield return null;
        SceneManager.LoadScene(sceneToLoad);
    }
}
