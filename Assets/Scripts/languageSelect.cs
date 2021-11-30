using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class languageSelect : MonoBehaviour
{
    public NewLanguage[] languages;
    public GameObject languagelayout;
    public GameObject languageSelectPanel;
    public float gameLoadWait = 3f;
    public AudioSource audioSource;
    public AudioClip swordChang;

    public void Start()
    {
        audioSource.PlayOneShot(swordChang);
        Invoke("activateLanguagePanel", 3f);
    }

    private void activateLanguagePanel()
    {
        languageSelectPanel.SetActive(true);
    }



    public void selectLanguage(NewLanguage language)
    {

        foreach (NewLanguage lang in languages)
        {
            if (lang == language)
            {
                PlayerPrefs.SetString("selectedLanguage", lang.languageName);
            }
        }
        languagelayout.SetActive(false);
        Invoke("loadGame",gameLoadWait);
    }

    void loadGame()
    {
    SceneManager.LoadScene(1);
        }
}
