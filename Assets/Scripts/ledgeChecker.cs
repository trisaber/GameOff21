using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ledgeChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // transform.parent.GetComponent<Movement>().Hanged();
        Debug.Log("dokundum");
    }
}
