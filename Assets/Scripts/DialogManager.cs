﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text text;
    public float waitTime = 10f;
    bool lavaFound = false;

    public NewLanguage language;
    public float spotDistance = 3f;
    public float lavaRecallDistance = 5f;


    private void Start()
    {
        StartCoroutine(startLava());
    }

    IEnumerator startLava()
    {

        yield return StartCoroutine(changeText(language.firstCall));
       
        yield return StartCoroutine(changeText(language.standUpCall));

        yield return StartCoroutine(changeText(language.briefing1));
        yield return new WaitForSeconds(waitTime);
        yield return StartCoroutine(changeText(language.briefing2));
        yield return new WaitForSeconds(waitTime);
        yield return StartCoroutine(changeText(language.briefing3));

    }

    IEnumerator changeText(string inputText)
    {


        text.text = inputText;
        yield return new WaitUntil(() => Input.anyKey);
        yield return new WaitForSeconds(waitTime);
        StartCoroutine(clearText());
    }

    IEnumerator clearText()
    {
        yield return new WaitForSeconds(waitTime);
        text.text = "  ";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collectable>() != null)
        {
            switch (other.gameObject.GetComponent<Collectable>().collectableType)
            {
                case Collectable.collectable.LAVA:
                    StartCoroutine(changeText(language.lavaReCall));    
                    break;
                case Collectable.collectable.Notebook:
                    StartCoroutine(changeText(language.noteBook));
                    break;
                case Collectable.collectable.holocon:
                    StartCoroutine(changeText(language.holocon));
                    break;
                case Collectable.collectable.sizeler:
                    StartCoroutine(changeText(language.sizeler));
                    break;
                case Collectable.collectable.sensor:
                    StartCoroutine(changeText(language.sensor));
                    break;
                case Collectable.collectable.ladybug:
                    StartCoroutine(changeText(language.ladybug));
                    break;

            }
        }

    }
}
    


