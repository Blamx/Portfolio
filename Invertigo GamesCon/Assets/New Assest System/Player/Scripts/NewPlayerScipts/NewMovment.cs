using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovment : MonoBehaviour
{

    public bool rayHit;//if a collision happens
    public RaycastHit rayOutData;//Place to store data of ray collision

    public bool onGround;//if a collision happens
    public RaycastHit downRayOutData;//Place to store data of ray collision

    public Player_Manager manager;

    Vector3 cameraOldUp; //up vec when camera starts rotating
    Vector3 cameraTargetUp = Vector3.up; //up vec camera is trying to rotate to
    Vector3 cameraLerpUp; //up vec inbetween the two, based on time
    float cameraFlipTimer = 0.0f;
    float cameraFlipSpeed = 0.6f;//time (seconds) camera takes to complete a rotation


    CharacterController characterController;
    public Rigidbody rb;

    // Gravity switching Varibles
    Vector3 up_Vec;
    public bool gravity = true;
    float airmod = 4;

    public Vector2 aimAssist = Vector2.zero;

    public LayerMask shotMask;

    public Vector3 stickVelocity;

    //movement mods
    [Range(0, 5)]
    public float sprintMod = 1;
    [Range(0, 5)]
    public float strafeMod = 1;
    [Range(0, 5)]
    public float backupMod = 1;
    [Range(1, 10)]
    public float baseSpeed = 10;
    [Range(0, 5)]
    public float adsMod = 0.5f;

    bool sprint;
    float speed;

    //abilites // experimental
    bool slam;
    bool dash;
    float dashMod = 30;
    float dashcoolDown = 3;
    float dashtimer = 0;

    //for animations
    public bool runningAnimation;
    public bool jumping;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        //Cursor.lockState = CursorLockMode.Locked;//lock cursor to center of screen
        //shotMask = manager.cam.GetComponent<Camera>().cullingMask;
    }

    void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            manager.target.health = 0;
        }
        dashtimer -= Time.deltaTime;
        dash = false;
        speed = baseSpeed * 1000;
        if (manager.playerGamepad.commands.aim)
        {
            speed *= adsMod;
        }
        if (manager.playerGamepad.commands.sprint)
        {
            sprint = true;
        }
        if (onGround)
        {
            slam = false;
        }
        else if (manager.playerGamepad.commands.slam)
        {
            slam = true;
        }
        if (manager.playerGamepad.commands.dash && dashtimer < 0)
        {
            if (!manager.playerGamepad.isKeyboard)
            {
                rb.AddForce((transform.forward * stickVelocity.y) * speed / 50 * dashMod);
                rb.AddForce((transform.right * stickVelocity.x) * speed / 50 * dashMod);
            }
            else
            {
                if (Input.GetKey("w"))
                {
                    rb.AddForce(transform.forward * speed / 50 * dashMod);
                }
                if (Input.GetKey("s"))
                {
                    rb.AddForce(-transform.forward * speed / 50 * dashMod);
                }
                if (Input.GetKey("a"))
                {
                    rb.AddForce(-transform.right * speed / 50 * dashMod);
                }
                if (Input.GetKey("d"))
                {
                    rb.AddForce(transform.right * speed / 50 * dashMod);
                }
            }
            dashtimer = dashcoolDown;
        }


        else if (manager.playerGamepad.isKeyboard && !Input.GetKey("w"))
        {
            sprint = false;
        }
        else if (!manager.playerGamepad.isKeyboard && GamePadManager.GetControllerAxes(manager.playerGamepad.player).r_ThumbStick_Y < 0.4)
        {
            sprint = false;
        }

        rayHit = Physics.Raycast(manager.cam.transform.position, manager.cam.transform.forward, out rayOutData, 1000.0f, manager.cam.GetComponent<Camera>().cullingMask);

        if (onGround)
        {
            rb.drag = 5;
        }
        else
        {
            rb.drag = 0.5f;
        }
        aimAssist.x = 0;

        if (!manager.playerGamepad.isKeyboard)
        {
            stickVelocity = new Vector2(GamePadManager.GetControllerAxes(manager.playerGamepad.gamepad).l_ThumbStick_X, GamePadManager.GetControllerAxes(manager.playerGamepad.gamepad).l_ThumbStick_Y);
            if (stickVelocity.sqrMagnitude < 0.1)
            {
                stickVelocity = Vector2.zero;
            }
            if (manager.movment.rayHit)
            {

                if (manager.movment.rayOutData.transform.name == "hitBox")
                {
                    if ((stickVelocity.x * 1 / rayOutData.distance) * 10 > 0 && Aim_Assist.AngleDir(transform.forward, rayOutData.transform.position - transform.position, transform.up) < 0)
                        aimAssist.x = -((stickVelocity.x * 1 / rayOutData.distance) * 10);// * manager.gunBehavior.currentGun.aimAssist;

                    if ((stickVelocity.x * 1 / rayOutData.distance) * 10 < 0 && Aim_Assist.AngleDir(transform.forward, rayOutData.transform.position - transform.position, transform.up) > 0)
                        aimAssist.x = -((stickVelocity.x * 1 / rayOutData.distance) * 10);// * manager.gunBehavior.currentGun.aimAssist;


                }
            }
        }

        //zero_g glitch would need to be fixed possibly replaced with Low gravity mode
        if (Input.GetKeyDown("e"))
        {
            //gravity = false;
        }

        if (gravity)
        {
            airmod = 4;
            if (!onGround && !slam)
            {
                rb.AddForce(-cameraTargetUp * 75);
            }
            else if (!onGround && slam)
            {
                rb.AddForce(-cameraTargetUp * 75 * 5);
            }
        }
        else
        {
            airmod = 50;
        }

        //Walking around
        if (onGround)
        {

            if (!manager.playerGamepad.isKeyboard)
            {
                if (stickVelocity.y > 0)
                {
                    if (sprint)
                        rb.AddForce(transform.forward * speed * sprintMod * Time.deltaTime * stickVelocity.y);
                    else
                    {
                        rb.AddForce(transform.forward * speed * Time.deltaTime * stickVelocity.y);
                    }

                    runningAnimation = true;
                }
                else
                {
                    rb.AddForce(transform.forward * speed * backupMod * Time.deltaTime * stickVelocity.y);
                    runningAnimation = false;
                }

                rb.AddForce(transform.right * speed * strafeMod * Time.deltaTime * stickVelocity.x);

            }
            else
            {
                if (Input.GetKey("w"))
                {
                    if (sprint)
                        rb.AddForce(transform.forward * speed * sprintMod * Time.deltaTime);
                    else
                        rb.AddForce(transform.forward * speed * Time.deltaTime);

                    runningAnimation = true;
                }
                if (Input.GetKey("s"))
                {
                    rb.AddForce(-transform.forward * speed * backupMod * Time.deltaTime);

                    runningAnimation = true;
                }
                if (Input.GetKey("a"))
                {
                    rb.AddForce(-transform.right * speed * strafeMod * Time.deltaTime);

                    runningAnimation = true;
                }
                if (Input.GetKey("d"))
                {
                    rb.AddForce(transform.right * speed * strafeMod * Time.deltaTime);

                    runningAnimation = true;
                }

                if (Input.GetKeyUp("w") && Input.GetKeyUp("s") && Input.GetKeyUp("a") && Input.GetKeyUp("d"))
                {
                    runningAnimation = false;
                }
            }


        }
        if (!onGround && !slam)
        {
            if (!manager.playerGamepad.isKeyboard)
            {
                rb.AddForce(transform.forward / airmod * speed * Time.deltaTime * stickVelocity.y);
                rb.AddForce(transform.right / airmod * speed * Time.deltaTime * stickVelocity.x);
            }
            else
            {
                if (Input.GetKey("w"))
                {
                    rb.AddForce(transform.forward / airmod * speed * Time.deltaTime);
                }
                if (Input.GetKey("s"))
                {
                    rb.AddForce(-transform.forward / airmod * speed * Time.deltaTime);
                }
                if (Input.GetKey("a"))
                {
                    rb.AddForce(-transform.right / airmod * speed * Time.deltaTime);
                }
                if (Input.GetKey("d"))
                {
                    rb.AddForce(transform.right / airmod * speed * Time.deltaTime);
                }
            }
        }
        //jumping
        if ((manager.playerGamepad.commands.jump && onGround))
        {
            jumping = true;
            rb.AddForce(transform.up * 2000);
        }
        else
        {
            jumping = false;
        }
        cameraFlip();

        //print(rb.velocity.magnitude);

        /*FOR NETWORKING*/
        //animation
        //if (onGround && rb.velocity.magnitude >= 2.0f)
        //{
        //    runningAnimation = true;
        //}
        //else if (!onGround || rb.velocity.magnitude < 2.0f)
        //{
        //    runningAnimation = false;
        //}

        if (!gravity)
        {
            transform.forward = manager.cam.transform.forward;
        }
        onGround = false;

        //Debug.DrawLine(transform.position, transform.position + cameraLerpUp * 10, Color.red);

    }
    void cameraFlip()
    {
        if (manager.playerGamepad.commands.flip)
        {
            //rayHit = Physics.Raycast(transform.position,manager.cam.transform.forward,out rayOutData);

            cameraTargetUp = rayOutData.normal;
            cameraOldUp = manager.cam.transform.up;

            cameraFlipTimer = 0.0f;
            gravity = true;
        }
        else if (manager.playerStats.respawn == true)
        {
            manager.playerStats.respawn = false;
            cameraTargetUp = transform.up;
            cameraOldUp = manager.cam.transform.up;

            cameraFlipTimer = 1.0f;
            gravity = true;
        }
        //rotate camera
        cameraFlipTimer += Time.deltaTime / cameraFlipSpeed;
        if (cameraFlipTimer >= 1.0f)
        {
            cameraFlipTimer = 1.0f;
        }
        else
        {
            manager.cam.recoilTarget.transform.rotation = manager.cam.transform.rotation;
        }
        //float temp = (Mathf.Sin(3.14159f * (cameraFlipTimer - 0.5f)) * 0.5f) + 0.5f;

        Vector3 oldForward = manager.cam.transform.forward;
        cameraLerpUp = Vector3.Slerp(cameraOldUp, cameraTargetUp, (Mathf.Sin(3.14159f * (cameraFlipTimer - 0.5f)) * 0.5f) + 0.5f);

        manager.cam.transform.up = cameraLerpUp;
        manager.cam.transform.LookAt(manager.cam.transform.position + oldForward, manager.cam.transform.up);


        Vector3 oldRecForward = manager.cam.recoilTarget.transform.forward;
        cameraLerpUp = Vector3.Slerp(cameraOldUp, cameraTargetUp, (Mathf.Sin(3.14159f * (cameraFlipTimer - 0.5f)) * 0.5f) + 0.5f);

        manager.cam.recoilTarget.transform.up = cameraLerpUp;
        manager.cam.recoilTarget.transform.LookAt(manager.cam.recoilTarget.transform.position + oldRecForward, manager.cam.recoilTarget.transform.up);

        //rotate player
        Vector3 playerForward = Vector3.Cross(cameraLerpUp, transform.right);
        transform.up = cameraLerpUp;
        transform.LookAt(transform.position + playerForward, transform.up);
        up_Vec = transform.up;
        transform.rotation = manager.cam.transform.rotation;
        Vector3 newFwd = Vector3.Cross(up_Vec, manager.cam.transform.right);
        transform.rotation = Quaternion.LookRotation(-newFwd, transform.up);


    }
}

