using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentAudio : MonoBehaviour
{
    public AudioClip[] clips;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        randomPlay();
    }

    void  randomPlay()
    {

            StartCoroutine(playAudio());
    }
    IEnumerator playAudio()

    {
        audioSource.clip = clips[Random.Range(0, clips.Length)];
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        randomPlay();
    }
}
