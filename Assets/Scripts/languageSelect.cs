using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class languageSelect : MonoBehaviour
{
    public NewLanguage[] languages;
    public GameObject languagelayout;

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
        Invoke("loadGame",3f);
    }

    void loadGame()
    {
    SceneManager.LoadScene(1);
        }
}
