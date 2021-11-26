using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text text;
    public float waitTime=10f;

    public NewLanguage language;

    private void Start()
    {        
        new WaitUntil(() => Input.anyKey);
        StartCoroutine(changeText(language.firstCall)) ;
        //StartCoroutine(changeText(language.lavaReCall));
    }

    IEnumerator changeText(string inputText)
    {
        text.text = inputText;
        
        yield return new WaitUntil(()=>Input.anyKey);
        text.text = language.lavaReCall;
    }

}
