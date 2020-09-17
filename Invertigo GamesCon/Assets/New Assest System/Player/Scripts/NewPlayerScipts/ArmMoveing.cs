using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMoveing : MonoBehaviour {

    public Player_Manager manager;

    public GameObject basePosition;

    GameObject rotationObj;
    GameObject targetpos;

    public GameObject adsObj;

    float moveMod = 0.02f;
    float rotateMod = 0.5f;

       // arm modifiers
    [Range(0.01f, 2f)]
    public float adsRotateMod = 0.25f;
    [Range(0.01f, 2f)]
    public float hipRotateMod = 1.0f;
    [Range(0.00001f, 0.0005f)]
    public float adsMoveMod = 0.00001f;
    [Range(0.00001f, 0.0005f)]
    public float hipMoveMod = 0.0002f;
    [Range(0.01f,1.0f)]
    public float moveReturnMod = 0.1f;
    [Range(0.01f, 1.0f)]
    public float rotateReturnMod = 0.1f;


    float adsTimer = 0;
    bool ads = false;
    bool adsing = false;

    // Use this for initialization
    void Start ()
    {
        rotationObj = new GameObject();
        targetpos = new GameObject();
    }
	
	// Update is called once per frame
	void Update ()
    {
        adsTimer -= Time.deltaTime;
       // Debug.Log(adsTimer);
        if (manager.playerGamepad.commands.aim)
        {
            targetpos.transform.position = adsObj.transform.position;
            targetpos.transform.rotation = adsObj.transform.rotation;
          
             if (!ads)
             {
                 ads = true;
                 adsing = true;
                 adsTimer = manager.gunBehavior.currentGun.AdsSpeed;
             }
             if (adsTimer <= 0)
             {
                adsing = false;
             }
            else
            {
                moveMod = adsMoveMod * 0.0001f;
                rotateMod = adsRotateMod * 0.0001f;
            }
        }
        else if(!manager.playerGamepad.commands.aim)
        {
            ads = false;
            targetpos.transform.position = basePosition.transform.position;
            targetpos.transform.rotation = basePosition.transform.rotation;
            moveMod = hipMoveMod;
            rotateMod = hipRotateMod;
        }



        transform.Rotate(Vector3.ClampMagnitude(new Vector3(-manager.cam.cameraChange.y, manager.cam.cameraChange.x, 0) * rotateMod , 1), Space.Self);
        transform.position += -manager.movment.rb.velocity * moveMod;

        if (adsing)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetpos.transform.rotation, rotateReturnMod * manager.gunBehavior.currentGun.AdsDuration);
            transform.position = Vector3.Lerp(transform.position, targetpos.transform.position, moveReturnMod * manager.gunBehavior.currentGun.AdsDuration);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetpos.transform.rotation, rotateReturnMod);
            transform.position = Vector3.Lerp(transform.position, targetpos.transform.position, moveReturnMod);
        }

	}
}
