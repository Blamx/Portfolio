using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NewCamrea : MonoBehaviour {

    public GameObject camTarget;
    public GameObject recoilTarget;

    public Vector2 cameraChange;
    float camSens = 3.0f;
    [Range(1.0f,10.0f)]
    public float stickSens = 7.0f;
    [Range(0.0f, 1.0f)]
    public float adsMod = 1.0f;
	float maxLookAngle = 85.0f;

    public Player_Manager manager;

	const float degToRad = 360.0f / (2.0f * 3.14159f);

    Vector2 stickVelocity;
    public float stickmod = 1.0f;

    public Slider senseslider;

    public float centerTime = 0;
    void Start ()
    {
        camTarget = new GameObject();
        recoilTarget = new GameObject();
        camTarget.transform.rotation = transform.rotation;
        recoilTarget.transform.rotation = transform.rotation;
        gameObject.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
    }


    void Update()
    {
        if (manager.state.state == 2 && manager.state.sensitvity)
        {
            senseslider.value = stickSens;
            if (manager.playerGamepad.commands.menuRight)
            {
                stickSens++;
                if (stickSens > 10)
                {
                    stickSens = 10;
                }
            }
            else if (manager.playerGamepad.commands.menuLeft)
            {
                stickSens--;
                if (stickSens < 1)
                {
                    stickSens = 1;
                }
            }
        }
        centerTime -= Time.deltaTime;

       
            if (!manager.playerStats.dead)
            {
                if (manager.movment.rayHit)
                {
                    if (manager.movment.rayOutData.transform.name == "hitBox")
                    {
                        stickmod = 0.4f;
                    }
                    else
                    {
                        stickmod = 1;
                    }
                }
                else
                {
                    stickmod = 1;
                }

                if (!manager.playerGamepad.isKeyboard)
                {
                    stickVelocity = manager.avdStick.getAxis("Right");
                    if (!manager.playerGamepad.commands.aim)
                    {
                        cameraChange.x = (stickVelocity.x * stickSens * stickmod) + manager.movment.aimAssist.x;
                        cameraChange.y = stickVelocity.y * stickSens / 2 * stickmod;
                    }
                    else
                    {
                        cameraChange.x = ((stickVelocity.x * stickSens * adsMod * stickmod) + manager.movment.aimAssist.x);
                        cameraChange.y = (stickVelocity.y * stickSens * adsMod / 2 * stickmod);
                    }
                }
                else
                {
                    //get mouse input
                    cameraChange.x = Input.GetAxis("Mouse X") * camSens;
                    cameraChange.y = Input.GetAxis("Mouse Y") * camSens;
                }

                //test for recoil
                if (Input.GetKeyDown("r"))
                {
                    recoilTarget.transform.Rotate(-transform.right * 10, Space.World);//rotate on local y axis
                }

                if (cameraChange.y < 0 && Vector3.Angle(transform.forward, manager.movment.transform.up) < Vector3.Angle(camTarget.transform.forward, manager.movment.transform.up))
                {
                    camTarget.transform.Rotate(transform.right * cameraChange.y, Space.World);//rotate on local y axis
                }
                if (Vector3.Angle(transform.forward, camTarget.transform.forward) < 0.1f)
                {
                    camTarget.transform.rotation = transform.rotation;
                }

                if(centerTime < 0)
                {
                    camTarget.transform.rotation = transform.rotation;
                    recoilTarget.transform.rotation = transform.rotation;
                }

                //Effected by recoil
                transform.Rotate(-transform.right * cameraChange.y, Space.World);//rotate on local y axis
                transform.Rotate(manager.movment.transform.up * cameraChange.x, Space.World);//rotate on local x axis

                //Not effected from recoil
                camTarget.transform.Rotate(-transform.right * cameraChange.y, Space.World);//rotate on local y axis
                camTarget.transform.Rotate(manager.movment.transform.up * cameraChange.x, Space.World);//rotate on local x axis

                //recoil effect
                recoilTarget.transform.Rotate(-transform.right * cameraChange.y, Space.World);//rotate on local y axis
                recoilTarget.transform.Rotate(manager.movment.transform.up * cameraChange.x, Space.World);//rotate on local x axis

                //Lerping tward recoil
                recoilTarget.transform.rotation = Quaternion.Lerp(camTarget.transform.rotation, recoilTarget.transform.rotation, 1 - manager.gunBehavior.currentGun.recoilSpeed);

                //recovering from recoil
                transform.rotation = Quaternion.Lerp(recoilTarget.transform.rotation, transform.rotation, manager.gunBehavior.currentGun.centerSpeed);

                //transform.rotation = recoilTarget.transform.rotation;

                float angleBetween = Vector3.Angle(transform.forward, manager.movment.transform.forward);

                if (angleBetween > maxLookAngle)
                {
                    if (Vector3.Angle(transform.forward, manager.movment.transform.up) > 90.0f)
                    {
                        transform.Rotate(-transform.right * -(maxLookAngle - angleBetween), Space.World);//rotate on local y axis
                        camTarget.transform.Rotate(-transform.right * -(maxLookAngle - angleBetween), Space.World);//rotate on local y axis
                        recoilTarget.transform.Rotate(-transform.right * -(maxLookAngle - angleBetween), Space.World);//rotate on local y axis
                    }
                    else
                    {
                        transform.Rotate(-transform.right * (maxLookAngle - angleBetween), Space.World);//rotate on local y axis
                        camTarget.transform.Rotate(-transform.right * (maxLookAngle - angleBetween), Space.World);//rotate on local y axis
                        recoilTarget.transform.Rotate(-transform.right * (maxLookAngle - angleBetween), Space.World);//rotate on local y axis
                    }
                }


                manager.cameraOrigin.transform.position = manager.camPos.transform.position;

                //  Debug.DrawLine(transform.position, transform.position + manager.cam.transform.forward * 1000, Color.green);
                //Debug.DrawLine(manager.movment.rayOutData.point,  manager.movment.rayOutData.point + manager.movment.rayOutData.normal * 10, Color.blue);




            }
            else
            {
                GameObject temp = new GameObject();
                temp.transform.position = transform.position;
                temp.transform.rotation = transform.rotation;
                temp.transform.LookAt(manager.playerStats.lastHitBy.transform.position, manager.movment.transform.up);

                transform.rotation = Quaternion.Lerp(transform.rotation, temp.transform.rotation, 0.25f);
            }
        
    }

}
