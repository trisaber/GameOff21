using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraControl : MonoBehaviour
{
    /*
    [SerializeField] Transform player;
    [SerializeField] float smoothSpeed=.6f;
    [SerializeField] Vector3 offset;
    */

    [SerializeField]
    private CinemachineVirtualCamera gameCam;

    [SerializeField]
    private CinemachineVirtualCamera zoomCam;




    /*
     void FixedUpdate()
     {
         Vector3 camPosition = player.position + offset;
         Vector3 smoothedPosition = Vector3.Lerp(transform.position, camPosition, smoothSpeed);
         transform.position = smoothedPosition;
         transform.LookAt(player);


     }
     */
    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ZoomCam();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            GameCam();
        }
    }

    private void GameCam()
    {
        zoomCam.Priority = 5;
        gameCam.Priority = 60;
    }

    private void ZoomCam()
    {
        gameCam.Priority = 5;
        zoomCam.Priority = 60;
    }
}
