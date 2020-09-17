using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Aim_Assist : MonoBehaviour {

    private GamePad_Manager gamepad;

    private Advanced_Stick gamepadStick;

    private GameObject target;//never used!

    private GameObject player;

    private GameObject tempPlayer;
    public float stickyAim = 3.0f;
    public float followPercent = 10.0f;

    public Vector2 mouseSensitivity = new Vector2(2.0f, 2.0f);
    public Vector2 adsMouseSensitivity = new Vector2(0.5f, 0.5f);
    public Vector2 sensitivity = new Vector2(3.0f, 3.0f);
    public Vector2 adsSensitivity = new Vector2(1.0f, 1.0f);
    public Vector2 ajustedSensitivity;
    public float adsMod = 3;
    Vector2 oldStick;




    private Vector3 targetPos;
    Vector3 oldTargetPos;

    Vector3 oldPos;

    // Use this for initialization
    void Start()
    {
        gamepad = transform.parent.GetComponent<GamePad_Manager>();
        gamepadStick = transform.parent.GetComponent<Advanced_Stick>();
        player = GameObject.Find(transform.parent.name + "/Character");
        tempPlayer = new GameObject("tempPlayer");



    }

    // Update is called once per frame
    void Update()
    {


        tempPlayer.transform.position = transform.position;
        tempPlayer.transform.rotation = transform.rotation;
        if (!gamepad.isKeyboard)
        { ajustedSensitivity = sensitivity; }
        else
        { ajustedSensitivity = mouseSensitivity; }



        //target = Cam_Raytrace.rayOutData[playerNum].collider.gameObject;

        if (transform.gameObject.GetComponent<Cam_Rotation>().isZooming)
        {
            if (!gamepad.isKeyboard)
            { ajustedSensitivity = adsSensitivity; }
            else
            { ajustedSensitivity = adsMouseSensitivity; }
        }
        if (!gamepad.isKeyboard)
        {
            if (transform.gameObject.GetComponent<Cam_Raytrace>().rayHit)
            {
                if (transform.gameObject.GetComponent<Cam_Raytrace>().rayOutData.collider.gameObject.name == "HitBox")
                {
                    if (transform.parent.name != transform.gameObject.GetComponent<Cam_Raytrace>().rayOutData.collider.transform.parent.parent.name)
                    {
                        targetPos = transform.gameObject.GetComponent<Cam_Raytrace>().rayOutData.collider.transform.position;


                        ajustedSensitivity = ajustedSensitivity / stickyAim;
                    }

                }
            }
            if (transform.gameObject.GetComponent<Cam_Raytrace>().rayHit)
            {
                if (transform.gameObject.GetComponent<Cam_Raytrace>().rayOutData.collider.gameObject.name == "HitBox")
                {
                    oldTargetPos = transform.gameObject.GetComponent<Cam_Raytrace>().rayOutData.collider.transform.position;
                }
            }
        }
        oldPos = player.transform.position;
        oldStick = gamepadStick.getAxis("Right");
    }
    public Vector2 newAim(Vector2 playerInput)
    {
        float rayDistance = 0;
        if (transform.gameObject.GetComponent<Cam_Raytrace>().rayHit)
        {
            target = transform.gameObject.GetComponent<Cam_Raytrace>().gameObject;
            if (transform.gameObject.GetComponent<Cam_Raytrace>().rayOutData.collider.gameObject.name == "HitBox")
            {
                Vector3 targetMove = -(target.transform.position - oldTargetPos);
                Vector3 proj = Vector3.Project(target.transform.up, targetMove);

                Vector3 targetDir = targetPos - player.transform.position;
                Vector3 perp = Vector3.Cross(player.transform.forward, targetDir);

                float dir = Vector3.Dot(perp, player.transform.up);

                if (dir > 0f)
                {
                    dir = 1;
                }
                else if (dir < 0)
                {
                    dir = -1;
                }
                else
                {
                    dir = 0;
                }





                rayDistance = Mathf.Clamp(transform.gameObject.GetComponent<Cam_Raytrace>().rayOutData.distance, 0.1f, 10f);

                Vector2 adjust = new Vector2(-Mathf.Clamp(gamepadStick.getAxis("Left").x, -1, 1), 0);

                Debug.Log(dir);
                //return playerInput + (adjust / (rayDistance*2)) * 2;

                if (dir > 0 && adjust.x > 0)
                    return playerInput + (adjust / (rayDistance / (followPercent * 0.1f))) * 2;
                else if (dir < 0 && adjust.x < 0)
                    return playerInput + (adjust / (rayDistance / (followPercent * 0.1f))) * 2;
            }
        }
        return playerInput;
    }


    //refrence
    //https://forum.unity.com/threads/left-right-test-function.31420/

    //returns -1 when to the left, 1 to the right, and 0 for forward/backward
    static public float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0.0f)
        {
            return 1.0f;
        }
        else if (dir < 0.0f)
        {
            return -1.0f;
        }
        else
        {
            return 0.0f;
        }
    }

}
