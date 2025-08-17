using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenuTMP : MonoBehaviour
{
    [Header("UI Buttons")]
    [SerializeField] private Button continueButton;
    [SerializeField] private TMP_Text continueButtonText;

    [SerializeField] private Button newGameButton;
    [SerializeField] private TMP_Text newGameButtonText;

    [Header("Default New Game Scene")]
    [SerializeField] private string defaultSceneName = "NoReturn";

    private void Start()
    {
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            continueButton.interactable = true;
        }
        else
        {
            continueButton.interactable = false;
        }

        // Butonlara listener ekle
        continueButton.onClick.AddListener(OnContinueClicked);
        newGameButton.onClick.AddListener(OnNewGameClicked);
    }

    private void OnContinueClicked()
    {
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            string sceneToLoad = PlayerPrefs.GetString("SavedScene");
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void OnNewGameClicked()
    {
        // Kaydý sýfýrla
        PlayerPrefs.DeleteKey("SavedScene");
        PlayerPrefs.Save();

        // Default sahneyi aç
        SceneManager.LoadScene(defaultSceneName);
    }
}
