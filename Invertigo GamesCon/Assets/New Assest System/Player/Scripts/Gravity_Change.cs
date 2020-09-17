using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity_Change : MonoBehaviour
{

    public ConstantForce force_Gravity;

    public Vector3 dir_Gravity;
    public float accel_gravity = 9.81f;

    public int playerNum;

    public string playerName;

    private GameObject player;

    GamePad_Manager gamePad;

    GameObject playerCamera;
    //=====================================================================================================================================================
    void Start()
    {

        playerCamera = GameObject.Find(transform.parent.name + "/Camera");
   
        force_Gravity = gameObject.AddComponent<ConstantForce>();// add a force to object
        gameObject.GetComponent<Rigidbody>().useGravity = false;// make sure object isn't using it's own gravity

        dir_Gravity = new Vector3(0, 1, 0);//set dir and speed of new gravity force

        player = GameObject.Find(transform.parent.name + "/Character");

        gamePad = transform.parent.gameObject.GetComponent<GamePad_Manager>();

       
    }

    //=====================================================================================================================================================
    void Update()
    {

        if(Input.GetKey(KeyCode.LeftControl))

            dir_Gravity = -playerCamera.transform.forward;//playerCamera.GetComponent<Cam_Rotation>().new_UpVec);//set gravity direction to normal of raytrace collision surface


        else
            dir_Gravity = player.transform.up;//playerCamera.GetComponent<Cam_Rotation>().new_UpVec);//set gravity direction to normal of raytrace collision surface
         


        
                 force_Gravity.force = -dir_Gravity * accel_gravity;
        
    }
   
}
