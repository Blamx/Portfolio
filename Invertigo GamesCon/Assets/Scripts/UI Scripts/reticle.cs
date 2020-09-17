using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class reticle : MonoBehaviour
{
    public RawImage rtcle;

    public Player_Manager manager;
    
    float angle;

    // Update is called once per frame
    void Update()
    {
        if(manager.playerGamepad.commands.aim)
        {
            rtcle.enabled = false;
        }
        else
            rtcle.enabled = true;
        rtcle.transform.LookAt(Vector3.up + rtcle.transform.position, manager.cam.transform.up);

        rtcle.transform.Rotate(90.0f, 0, 0);

        rtcle.transform.right = manager.cam.transform.right;
        rtcle.transform.forward = manager.cam.transform.forward;
    }
}
