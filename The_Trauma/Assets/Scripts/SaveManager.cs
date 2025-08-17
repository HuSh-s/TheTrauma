using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    void Awake()
    {
        // Oyunu açtýðýnda kaydedilmiþ sahneyi yükle
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            string sceneToLoad = PlayerPrefs.GetString("SavedScene");
            // Eðer zaten bu sahnedeysek tekrar yükleme
            if (SceneManager.GetActiveScene().name != sceneToLoad)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }

    public static void SaveScene(string sceneName)
    {
        PlayerPrefs.SetString("SavedScene", sceneName);
        PlayerPrefs.Save(); // commit'i garanti et
        Debug.Log("Scene saved: " + sceneName);
    }
}

