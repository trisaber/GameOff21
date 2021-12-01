using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RestartGame : MonoBehaviour
{
    public float tapSpeed = 0.25f; //in seconds
    private float lastTapTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        lastTapTime = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if ((Time.time - lastTapTime) <= tapSpeed)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            lastTapTime = Time.time;
        }
    }
}
