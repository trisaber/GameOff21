using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour
{

    [SerializeField] Transform player;
    [SerializeField] float smoothSpeed=.6f;
    [SerializeField] Vector3 offset;
    




    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 camPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, camPosition, smoothSpeed);
        transform.position = smoothedPosition;
        transform.LookAt(player);

    
    }
}
