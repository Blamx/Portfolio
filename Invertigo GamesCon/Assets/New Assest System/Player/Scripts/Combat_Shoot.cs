using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat_Shoot : MonoBehaviour
{

    public GameObject emitter;

    public bool Shot;
    public bool laser = false;
    public bool Hit;

    public bool iskicking;
    public bool isCentering;

    public Vector3 pointHit;

    public GamePad_Manager gamePad;

    private GameObject player;
    GameObject playerCamera;
    Gun_Behavior gun;
    Cam_Rotation camRotation;


    //=====================================================================================================================================================
    void Start()
    {
        gun = transform.gameObject.GetComponentInChildren<Gun_Behavior>();
        playerCamera = transform.gameObject;
        camRotation = playerCamera.GetComponent<Cam_Rotation>();
        player = GameObject.Find(transform.parent.name + "/Character");
        gamePad = transform.parent.gameObject.GetComponent<GamePad_Manager>();
    }

    //=====================================================================================================================================================
    void Update()
    {

        if (((Input.GetMouseButton(0) && gamePad.isKeyboard && gun.currentGun.fireType == 0) || (Input.GetMouseButtonDown(0) && gamePad.isKeyboard && gun.currentGun.fireType == 1) || (gun.currentGun.fireType == 0 && gamePad.getButtonDown("RT")) || (gun.currentGun.fireType == 1 && gamePad.getButtonPressed("RT"))) && !Shot)//right mouse click
        {
            Shot = true;//passed to gun behaviour
            laser = true;
            if (transform.gameObject.GetComponent<Cam_Raytrace>().rayHit)
            {
                pointHit = transform.gameObject.GetComponent<Cam_Raytrace>().rayOutData.point;
                GameObject objectHit = transform.gameObject.GetComponent<Cam_Raytrace>().rayOutData.collider.gameObject;
                //sparks.GetComponent<Control>().Sparks(transform.gameObject.GetComponent<Cam_Raytrace>().rayOutData.point,transform.rotation);
                Instantiate<GameObject>(emitter, pointHit, transform.rotation);
                //Debug.Log(objectHit);
                if (objectHit.transform.parent != null)
                {

                    if (objectHit.transform.parent.gameObject.GetComponent<Player_Player>())// if you're shooting a player
                    {
                        if (objectHit.transform.parent.GetComponent<Player_Player>().GetTeam() != player.GetComponent<Player_Player>().GetTeam())// if the player is not on your team ========================================================================================================FIX IT
                        {
                            objectHit.transform.parent.GetComponent<Player_Player>().SubFromPlayerHP(gun.currentGun.damage);//if object hit is player, remove hp
                            objectHit.transform.parent.GetComponent<Player_Player>().SetLastHitBy(player);
                            objectHit.transform.parent.GetComponent<Player_Player>().regenDelay = 2.5f;
                            objectHit.transform.parent.GetComponent<Rigidbody>().AddForceAtPosition(camRotation.cam_forwardVec * gun.currentGun.force * 6, transform.gameObject.GetComponent<Cam_Raytrace>().rayOutData.point, ForceMode.Force);//else if object has rigid body and is not a player, knock back
                            //hit marker stuff
                            Hit = true;

                        }
                    }
                }
                if (objectHit.GetComponent<Rigidbody>())// if you're shooting a physics object
                {
                    objectHit.GetComponent<Rigidbody>().AddForceAtPosition(camRotation.cam_forwardVec * gun.currentGun.force, transform.gameObject.GetComponent<Cam_Raytrace>().rayOutData.point, ForceMode.Force);//else if object has rigid body and is not a player, knock back
                }

            }


        }
    }
}
