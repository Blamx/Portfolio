using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player_Move : MonoBehaviour
{
    //speed of player
    float speed = 50; //current speed of player
    float strafeSpeed = 50;
    public float BaseSpeed = 50; //speed of player when not adsing 
    public float ADSSpeed = 25; //speed of player when adsing
    public float sprintSpeed = 125;

    //how much control is lost in the air
    public float airRestriction = 1;

    public float jumpStrength = 25;

    //rigid body for jumping
    public Rigidbody rb;
    public RaycastHit rayOutData;
    public bool onFloor;

    private Player_Manager playerManager;

    public GamePad_Manager gamePad;

    public GameObject playerCamera;
    Cam_Rotation camRotation;

    bool sprint;

    public bool run, walkBack, falling, jumping, strafeL, strafeR;


    // Use this for initialization
    void Start()
    {

        playerCamera = GameObject.Find(transform.parent.name + "/Camera");
        camRotation = playerCamera.GetComponent<Cam_Rotation>();
        gamePad = transform.parent.gameObject.GetComponent<GamePad_Manager>();
        playerManager = transform.parent.GetComponent<Player_Manager>();
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        run = falling = walkBack = strafeR = strafeL = false;

        gamePad.getButtonPressed("ST");

        if (camRotation.isZooming)
        {
            speed = ADSSpeed;
            strafeSpeed = ADSSpeed;
        }
        else if (sprint)
        {
            speed = sprintSpeed;
            strafeSpeed = BaseSpeed;
        }
        else
        {
            speed = BaseSpeed;
            strafeSpeed = BaseSpeed;
        }

        onFloor = Physics.Raycast(transform.position, -camRotation.cam_upVec, out rayOutData, 1.6f);

        //changes Drag if in air
        if (onFloor == false)
        {
            rb.drag = 1;
        }
        else
        {
            rb.drag = 5;
        }

        if (gamePad.isKeyboard == true)
        {
            if (onFloor)
            {    //WASD movement
                if (Input.GetKey("w"))//forward
                {

                    rb.AddForce(transform.forward * Time.deltaTime * speed, ForceMode.VelocityChange);
                    run = true;
                }
                if (Input.GetKey("s"))//backward
                {
                    speed = 25;
                    rb.AddForce(-transform.forward * Time.deltaTime * speed, ForceMode.VelocityChange);
                    walkBack = true;
                }
                //-----------------------------------------------------------------
                if (Input.GetKey("d"))//right
                {

                    rb.AddForce(camRotation.cam_rightVec * Time.deltaTime * strafeSpeed, ForceMode.VelocityChange);
                    strafeR = true;
                }
                if (Input.GetKey("a"))//left
                {

                    rb.AddForce(-camRotation.cam_rightVec * Time.deltaTime * strafeSpeed, ForceMode.VelocityChange);
                    strafeL = true;
                }
            }
            else
            {
                if (Input.GetKey("w"))//forward
                {
                    rb.AddForce(transform.forward * Time.deltaTime * speed / airRestriction, ForceMode.VelocityChange);
                }
                if (Input.GetKey("s"))//backward
                {
                    rb.AddForce(-transform.forward * Time.deltaTime * speed / airRestriction, ForceMode.VelocityChange);
                }
                //-----------------------------------------------------------------
                if (Input.GetKey("d"))//right
                {
                    rb.AddForce(camRotation.cam_rightVec * Time.deltaTime * strafeSpeed / airRestriction, ForceMode.VelocityChange);
                }
                if (Input.GetKey("a"))//left
                {
                    rb.AddForce(-camRotation.cam_rightVec * Time.deltaTime * strafeSpeed / airRestriction, ForceMode.VelocityChange);
                }
                //falling = true;
            }
        }

        //analog stick movement(Xbox)
        if (gamePad.getGamePadConnected())
        {
            if (onFloor)
            {
                if (gamePad.getAxisRaw("Left", "Y") > 0.2f)
                {
                    run = true;
                }
                if (gamePad.getAxisRaw("Left", "Y") < -0.2f)
                {
                    speed = 25;
                    walkBack = true;
                }
                if (gamePad.getAxisRaw("Left", "X") > 0.2f)
                {
                    speed = speed / 2;
                    strafeR = true;
                }
                if (gamePad.getAxisRaw("Left", "X") < -0.2f)
                {
                    speed = speed / 2;
                    strafeL = true;
                }
                rb.AddForce(transform.forward * Time.deltaTime * speed * gamePad.getAxisRaw("Left", "Y"), ForceMode.VelocityChange);
                rb.AddForce(camRotation.cam_rightVec * Time.deltaTime * strafeSpeed * gamePad.getAxisRaw("Left", "X"), ForceMode.VelocityChange);
            }
            else
            {
                rb.AddForce(transform.forward * Time.deltaTime * speed / airRestriction * gamePad.getAxisRaw("Left", "Y"), ForceMode.VelocityChange);
                rb.AddForce(camRotation.cam_rightVec * Time.deltaTime * strafeSpeed / airRestriction * gamePad.getAxisRaw("Left", "X"), ForceMode.VelocityChange);
                //falling = true;
            }
        }

        // jump
        if (onFloor)
        {
            if ((Input.GetKeyDown("space") && gamePad.isKeyboard) || gamePad.getButtonPressed("A"))
            {
                rb.AddForce(camRotation.cam_upVec * jumpStrength * 2, ForceMode.Force);

                jumping = true;
            }
        }

        if (!onFloor && !jumping)
        {
            falling = true;
        }
        else if (onFloor)
        {
            falling = false;
        }

        if (!gamePad.isKeyboard)
        {
            if (gamePad.getButtonDown("L3"))
            {
                sprint = true;
            }
            else if (Vector2.SqrMagnitude(new Vector2(gamePad.getAxisRaw("Left", "Y"), gamePad.getAxisRaw("Left", "X"))) < 0.2 || playerCamera.GetComponent<Combat_Shoot>().Shot)
            {
                sprint = false;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                sprint = true;

            }
            else if (!Input.GetKey("w") || playerCamera.GetComponent<Combat_Shoot>().Shot)
            {
                sprint = false;
            }
        }


        //rotates the player to look where camera is looking.
        transform.rotation = camRotation.transform.rotation;
        Vector3 newFwd = Vector3.Cross(camRotation.cam_upVec, camRotation.cam_rightVec);
        transform.rotation = Quaternion.LookRotation(-newFwd, transform.up);


    }
}
