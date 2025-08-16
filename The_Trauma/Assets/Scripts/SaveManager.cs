using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    void Awake()
    {
        LoadSavedScene();
    }
    public static void SaveScene(string sceneName)
    {
        PlayerPrefs.SetString("SavedScene", sceneName);
        PlayerPrefs.Save();
    }

    public static void LoadSavedScene()
    {
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            string sceneToLoad = PlayerPrefs.GetString("SavedScene");
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            // Ýlk sahne default açýlýr
            SceneManager.LoadScene("NoReturn");
        }
    }
}
