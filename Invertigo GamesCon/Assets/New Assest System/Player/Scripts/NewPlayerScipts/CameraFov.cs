using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CameraFov : MonoBehaviour {

    public Player_Manager manager;
    float currentFov = 60;

    public float fovMod = 0;

    public Slider Fov;

    bool aim = false;
    float adsTime = 0;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        Fov.value = 60 + fovMod;

        if(manager.state.fov)
        {
            if(manager.playerGamepad.commands.menuRight)
            {
         
                fovMod += 5;

                if (fovMod > 50)
                {
                    fovMod = 50;
                }
            }
            if (manager.playerGamepad.commands.menuLeft)
            {
                fovMod -= 5;
                if (fovMod < 0)
                {
                    fovMod = 0;
                }

            }
        }

        adsTime -= Time.deltaTime;
        //replace with Zoom stat on guns later
		if(manager.playerGamepad.commands.aim && aim == false)
        {
            currentFov = 35;
            aim = true;
            adsTime = manager.gunBehavior.currentGun.AdsDuration;
        }
        else if(!manager.playerGamepad.commands.aim && aim)
        {
            currentFov = 60;
            aim = false;
            adsTime = manager.gunBehavior.currentGun.AdsDuration;

        }

        if (adsTime > 0)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, currentFov + fovMod, 1 - adsTime / manager.gunBehavior.currentGun.AdsDuration);
        }
        else
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, currentFov + fovMod, 1);
        }
    }
}
