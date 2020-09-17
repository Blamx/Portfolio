using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Rotation : MonoBehaviour
{

    public Vector2 mouseCamSensitivity = new Vector2(1.0f, 1.0f);
    public Vector2 stickCamSensitivity = new Vector2(3.0f, 3.0f);

    public Vector3 cam_upVec;
    public Vector3 cam_rightVec;
    public Vector3 cam_forwardVec;

    //Needs to Be Fixed//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //RotationDuration doesn't do anything ////////////////////////////////////////////////////////////////////////////////////////////////////////////// 
    private float rotationTimer = 0.0f;//timer variable
    public float rotationDuration = 0.9f;//time it takes to complete rotation (seconds)
    private bool isRotating;
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private float adsTime; //variable to intrement time
    public float adsDuration = 0.2f; //how long to ads

    public bool isZooming;//is asing
    public bool AdsActive;//if ads or hip

    Vector3 old_UpVec;//vec considered up at the start of rotation process
    public Vector3 new_UpVec = new Vector3(0,1,0);//target vector for rotation process

    [Range(40.0f, 120.0f)]
    public float hipFov = 60; //varibles to lerp between FOV's when ADSing
    [Range(0.0f, 100.0f)]
    public float adsFov = 30;

    public float restrictAngle; //used for restricting camera rotation   

    public GameObject player;

    GamePad_Manager gamePad;
    public Advanced_Stick gamePadStick;//advanced anglog stick controls

    public float recoilEffect = 0;//sideways recoil value
    public float recoilEffectY = 0;//sideways recoil value

    GameObject NoRecoil;
    GameObject camPos;

    //=====================================================================================================================================================
    void Start()
    {
        camPos = GameObject.Find(transform.parent.name + "/Character/CamPos");
        cam_upVec = new Vector3(0, 1, 0);//init camera up vector
        new_UpVec = new Vector3(0, 1, 0);
        Cursor.lockState = CursorLockMode.Locked;//lock cursor to center of screen
        transform.forward += new Vector3(0, 0.0001f, 0);

        //finds player
        player = GameObject.Find(transform.parent.name + "/Character");
        gamePad = transform.parent.gameObject.GetComponent<GamePad_Manager>();
        gamePadStick = transform.parent.gameObject.GetComponent<Advanced_Stick>();
        NoRecoil = GameObject.Find(transform.parent.name + "/NoRecoil");
        NoRecoil.transform.rotation = transform.rotation;
    }

    //=====================================================================================================================================================
    void Update()
    {
        NoRecoil.transform.position = transform.position;
        CameraTurning();

        CamreaZoom();

        Vector2 cameraChange = new Vector2(0,0);
        Vector2 cameraChangeNR = new Vector2(0, 0);

        if (gamePad.isKeyboard)
        {

            cameraChange.x = transform.GetComponent<Aim_Assist>().ajustedSensitivity.x * Input.GetAxis("Mouse X") + recoilEffect;//get mouse input
            cameraChange.y = transform.GetComponent<Aim_Assist>().ajustedSensitivity.y * Input.GetAxis("Mouse Y") + recoilEffectY;
            cameraChangeNR.y = transform.GetComponent<Aim_Assist>().ajustedSensitivity.y * Input.GetAxis("Mouse Y");
        }
        if (!gamePad.isKeyboard)
        {
            cameraChange.x = transform.GetComponent<Aim_Assist>().ajustedSensitivity.x * gamePadStick.getAxis("Right").x + recoilEffect;//get stick input
            cameraChange.y = transform.GetComponent<Aim_Assist>().ajustedSensitivity.y * gamePadStick.getAxis("Right").y + recoilEffect;
            cameraChangeNR.y = transform.GetComponent<Aim_Assist>().ajustedSensitivity.y * gamePadStick.getAxis("Right").y;

            cameraChange = transform.GetComponent<Aim_Assist>().newAim(cameraChange);
           
        }
        recoilEffect = 0;
        recoilEffectY = 0;

        transform.Rotate(cam_upVec * cameraChange.x, Space.World);//rotate on local x axis
        NoRecoil.transform.Rotate(cam_upVec * cameraChange.x, Space.World);//rotate on local x axis
        //Debug.DrawRay(transform.position, cam_upVec, Color.green);

        cam_rightVec = Vector3.Cross(cam_upVec, transform.forward);//calculate new right vector
        cam_rightVec = Vector3.Normalize(cam_rightVec);

        //Checks the angle between camera's forward vector and the normal of the surface the player is on
        if ((Vector3.Angle(transform.forward, cam_upVec) <= restrictAngle) && (cameraChange.y > 0)) //If the angle between the two vectors is less than a certain degree and the player is trying to look up
        {
            cameraChange.y = -1.0f; // rotate down instead
        }
        else if ((Vector3.Angle(transform.forward, cam_upVec) >= (180 - restrictAngle)) && (cameraChange.y < 0)) //If the angle between the two vectors is bigger than a certain degree and the player is trying to look down
        {
            cameraChange.y = 1.0f; // rotate up instead
        }
        else //Just rotates normally
        {
            //Debug.Log(Vector3.Angle(NoRecoil.transform.forward, player.transform.forward) +" "+ Vector3.Angle(transform.forward, player.transform.forward));
            if (Vector3.Angle(NoRecoil.transform.forward, player.transform.forward) <= Vector3.Angle(transform.forward, player.transform.forward) || cameraChange.y > 0)
            NoRecoil.transform.Rotate(cam_rightVec * -cameraChangeNR.y, Space.World);
            transform.Rotate(cam_rightVec * -cameraChange.y, Space.World);//rotate on local y axis (netagive to function like standard FPS controls)
            //Debug.DrawRay(transform.position, cam_rightVec, Color.red);

            if (Vector3.Angle(NoRecoil.transform.forward, player.transform.up) < Vector3.Angle(transform.forward, player.transform.up))
            {
                NoRecoil.transform.rotation = transform.rotation;
            }
        }

        cam_forwardVec = transform.TransformDirection(Vector3.forward);//seting forward

        //sets camrea to players position
        transform.position = camPos.transform.position;
        //transform.rotation = NoRecoil.transform.rotation;
        Debug.DrawRay(NoRecoil.transform.position, NoRecoil.transform.forward);

    }

    void CameraTurning()
    {
        /*if (!isRotating && player.gameObject.GetComponent<Player_Move>().onFloor)
        {
            Debug.Log(Vector3.Angle(player.gameObject.GetComponent<Player_Move>().rayOutData.normal, transform.up));

            if (Vector3.Angle(player.gameObject.GetComponent<Player_Move>().rayOutData.normal,transform.up) > 5 && Vector3.Angle(player.gameObject.GetComponent<Player_Move>().rayOutData.normal, transform.up) < 20 && transform.gameObject.GetComponent<Cam_Raytrace>().rayHit)
            {
                
                rotationTimer = rotationDuration;
                isRotating = true;

                old_UpVec = cam_upVec;
                // Debug.DrawRay(transform.position, old_UpVec, Color.magenta, 3.0f);
                new_UpVec = player.gameObject.GetComponent<Player_Move>().rayOutData.normal;
                //Debug.DrawRay(transform.position, new_UpVec, Color.cyan, 3.0f);
            }
        }
        */
        if (isRotating)
        {
            rotationTimer -= Time.deltaTime;//decrement timer
            if (old_UpVec == -new_UpVec)//to handle when flipping 180 degrees
            {
                old_UpVec += cam_rightVec * 0.001f;
            }
            cam_upVec = Vector3.Slerp(old_UpVec, new_UpVec, (1.0f - (rotationTimer / rotationDuration)));//lerp between new and old up vectors
            transform.up = cam_upVec;

            cam_rightVec = Vector3.Cross(cam_upVec, transform.forward);//calculate new right vector
            cam_rightVec = Vector3.Normalize(cam_rightVec);
            if (transform.gameObject.GetComponent<Cam_Raytrace>().rayHit)
            {
                transform.LookAt(transform.gameObject.GetComponent<Cam_Raytrace>().rayOutData.point, cam_upVec);//keep camera looking at same angle it had before rotation //===ISSUE HERE: because it's being told to look at a point the raytrace defines, your camera will not change if the raytrace doesn't collide with anything
                NoRecoil.transform.LookAt(transform.gameObject.GetComponent<Cam_Raytrace>().rayOutData.point, cam_upVec);//keep camera looking at same angle it had before rotation //===ISSUE HERE: because it's being told to look at a point the raytrace defines, your camera will not change if the raytrace doesn't collide with anything
            }
            if (rotationTimer < 0)//end rotation once timer ends
            {
                isRotating = false;
            }
        }
        if ((Input.GetKeyDown("q") && gamePad.isKeyboard) || gamePad.getButtonPressed("RB"))//right mouse click
        {

            if (transform.gameObject.GetComponent<Cam_Raytrace>().rayHit)
            {
                rotationTimer = rotationDuration;
                isRotating = true;

                old_UpVec = cam_upVec;
               // Debug.DrawRay(transform.position, old_UpVec, Color.magenta, 3.0f);
                new_UpVec = transform.gameObject.GetComponent<Cam_Raytrace>().rayOutData.normal;
                //Debug.DrawRay(transform.position, new_UpVec, Color.cyan, 3.0f);
            }
        }
            else if (player.GetComponent<Player_Player>().reSpawn)
            {
                rotationTimer = rotationDuration;
                isRotating = true;

                player.GetComponent<Player_Move>().rb.velocity = Vector3.zero;

                old_UpVec = cam_upVec;
                new_UpVec = player.GetComponent<Player_Player>().newGrav;
                player.GetComponent<Player_Player>().reSpawn = false;
            }



    }

    void CamreaZoom()
    {

         

        //Aim down sights
        if (((Input.GetMouseButton(1) && gamePad.isKeyboard)) || gamePad.getButtonDown("LT"))
        {
            AdsActive = true;
            Camera TempCam = GetComponent<Camera>();
            if (isZooming == false)
            {
                adsTime = adsDuration;
            }
            isZooming = true;

           


            TempCam.fieldOfView = Mathf.Lerp(hipFov, adsFov, 1.0f - (adsTime / adsDuration));
        }
        else
        {
            AdsActive = false;
            if (isZooming == true)
            {
                adsTime = adsDuration;
            }
            Camera TempCam = GetComponent<Camera>();
            TempCam.fieldOfView = Mathf.Lerp(adsFov, hipFov, 1.0f - (adsTime / adsDuration));
            isZooming = false;
        }
        adsTime -= Time.deltaTime;
    }
}
