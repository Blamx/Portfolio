using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Advanced_Stick : MonoBehaviour {


    public Player_Manager manager;
    Vector2 rawRight;
    Vector2 adjustedRight;
    Vector2 rawLeft;
    Vector2 adjustedLeft;
    [Range(0.1f,1.0f)]
    public float lerpSpeed = 0.9f;

    // Use this for initialization
    void Start()
    {
        
        rawRight = new Vector2();
        adjustedRight = new Vector2();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!manager.playerGamepad.isKeyboard)
        {
            rawRight = new Vector2(GamePadManager.GetControllerAxes(manager.playerGamepad.player).r_ThumbStick_X, GamePadManager.GetControllerAxes(manager.playerGamepad.player).r_ThumbStick_Y);
            adjustedRight = Vector2.Lerp(adjustedRight, rawRight, lerpSpeed * Time.deltaTime * 10);

            rawLeft = new Vector2(GamePadManager.GetControllerAxes(manager.playerGamepad.player).l_ThumbStick_X, GamePadManager.GetControllerAxes(manager.playerGamepad.player).l_ThumbStick_Y);
            adjustedLeft = Vector2.Lerp(adjustedLeft, rawLeft, lerpSpeed * Time.deltaTime * 10);
        }

        if (Vector2.SqrMagnitude(rawRight) < 0.1)
        {
            adjustedRight = Vector2.zero;
        }
        if (Vector2.SqrMagnitude(rawLeft) < 0.1)
        {
            adjustedLeft = Vector2.zero;
        }
    }


    /* rawRight = new Vector2(GamePadManager.GetControllerAxes(manager.playerGamepad.player).r_ThumbStick_X, GamePadManager.GetControllerAxes(manager.playerGamepad.player).r_ThumbStick_Y);
    adjustedRight = Vector2.Lerp(adjustedRight, rawRight, lerpSpeed * Time.deltaTime);

    rawLeft = new Vector2(GamePadManager.GetControllerAxes(manager.playerGamepad.player).l_ThumbStick_X, GamePadManager.GetControllerAxes(manager.playerGamepad.player).l_ThumbStick_Y);
    adjustedLeft = Vector2.Lerp(adjustedLeft, rawLeft, lerpSpeed * Time.deltaTime);*/


public Vector2 getAxis(string stick)
    {
        if (Vector2.SqrMagnitude(rawRight) > 0)
        {
            if (stick == "Right")
            {
                return adjustedRight;
            }
        }
        if (Vector2.SqrMagnitude(rawLeft) > 0)
        {
            if (stick == "Left")
            {
                return adjustedLeft;
            }
        }
        return new Vector2(0, 0);
    }
}
