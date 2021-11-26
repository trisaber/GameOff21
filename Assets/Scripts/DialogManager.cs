using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text text;
    public float waitTime=10f;
    public Transform lava;
    bool lavaFound = false;

    public NewLanguage language;

    private void Start()
    {
        StartCoroutine(startLava());
    }

    IEnumerator startLava()
    {
        new WaitUntil(() => Input.anyKey);
        yield return StartCoroutine(changeText(language.firstCall));
        yield return new WaitForSeconds(waitTime);
        yield return StartCoroutine(changeText(language.standUpCall));
    }

    IEnumerator changeText(string inputText)
    {
        
        
        yield return new WaitUntil(()=>Input.anyKey);
        text.text = inputText;
        StartCoroutine(clearText());
    }

    IEnumerator clearText()
    {
        yield return new WaitForSeconds(waitTime);
        text.text = "  ";
    }
    private void Update()
    {
        lavaCall();
    }

    private void lavaCall()
    {
        if (Vector3.Distance(transform.position, lava.position) < 5)
        {
            lavaFound = true;
        }
        if (lavaFound && Vector3.Distance(transform.position, lava.position) > 5)
        {
            StartCoroutine(changeText(language.lavaReCall));
        }
    }
}
