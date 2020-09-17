using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class GamePad_Manager : MonoBehaviour {


    public static bool[] gamePads = new bool[4];
    public static bool[] players = new bool[4];

    static bool keyboardTaken = false;

    public bool isKeyboard = false;

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    public GamePadState state;
    GamePadState prevState;

    int playerNumber = -1;
    [Range(0.0f, 5.0f)]
    public float curve = 1;

    public float innerDeadzone = 0.1f;
    public float outerDeazone = 0.8f;

    // Use this for initialization
    void Start()
    {

        if(transform.name == "Player1")
        {
            playerNumber = 0;
        }
        if (transform.name == "Player2")
        {
            playerNumber = 1;
        }
        if (transform.name == "Player3")
        {
            playerNumber = 2;
        }
        if (transform.name == "Player4")
        {
            playerNumber = 3;

        }
        if ((!playerIndexSet || !prevState.IsConnected) && players[playerNumber] == false)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected && gamePads[i] == false && players[playerNumber] == false)
                {

                    Debug.Log(string.Format("GamePad for player: " + transform.name + " found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                    gamePads[i] = true;
                    players[playerNumber] = true;
                }
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
       
        if ((!playerIndexSet || !prevState.IsConnected) && players[playerNumber] == false && isKeyboard == false)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected && gamePads[i] == false && players[playerNumber] == false)
                {
                  
                    Debug.Log(string.Format("GamePad for player: " + transform.name + " found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                    gamePads[i] = true;
                    players[playerNumber] = true;
                }
            }
        }

        if(!state.IsConnected && keyboardTaken == false)
        {
            isKeyboard = true;
            keyboardTaken = true;
        }
        else if(prevState.IsConnected && state.IsConnected && isKeyboard)
        {
            isKeyboard = false;
            keyboardTaken = false;
        }
    

        if (playerIndexSet)
        { 
          prevState = state;
          state = GamePad.GetState(playerIndex);
        }
    }

    public bool getButtonDown(string button)
    {
        if (playerIndexSet)
        {
            if (button == "A" && state.Buttons.A == ButtonState.Pressed)
            {
                return true;
            }
            else if (button == "B" && state.Buttons.B == ButtonState.Pressed)
            {
                return true;
            }
            else if (button == "X" && state.Buttons.X == ButtonState.Pressed)
            {
                return true;
            }
            else if (button == "Y" && state.Buttons.Y == ButtonState.Pressed)
            {
                return true;
            }
            else if (button == "RB" && state.Buttons.RightShoulder == ButtonState.Pressed)
            {
                return true;
            }
            else if (button == "LB" && state.Buttons.LeftShoulder == ButtonState.Pressed)
            {
                return true;
            }
            else if (button == "R3" && state.Buttons.RightStick == ButtonState.Pressed)
            {
                return true;
            }
            else if (button == "L3" && state.Buttons.LeftStick == ButtonState.Pressed)
            {
                return true;
            }
            else if (button == "LT" && state.Triggers.Left > 0.2)
            {
                return true;
            }
            else if (button == "RT" && state.Triggers.Right > 0.2)
            {
                return true;
            }
        }
        return false;
    }
    public bool getButtonPressed(string button)
    {
        if (playerIndexSet)
        {
            if (button == "A" && state.Buttons.A == ButtonState.Pressed && prevState.Buttons.A == ButtonState.Released)
            {
                return true;
            }
            else if (button == "B" && state.Buttons.B == ButtonState.Pressed && prevState.Buttons.B == ButtonState.Released)
            {
                return true;
            }
            else if (button == "X" && state.Buttons.X == ButtonState.Pressed && prevState.Buttons.X == ButtonState.Released)
            {
                return true;
            }
            else if (button == "Y" && state.Buttons.Y == ButtonState.Pressed && prevState.Buttons.Y == ButtonState.Released)
            {
                return true;
            }
            else if (button == "RB" && state.Buttons.RightShoulder == ButtonState.Pressed && prevState.Buttons.RightShoulder == ButtonState.Released)
            {
                return true;
            }
            else if (button == "LB" && state.Buttons.LeftShoulder == ButtonState.Pressed && prevState.Buttons.LeftShoulder == ButtonState.Released)
            {
                return true;
            }
            else if (button == "ST" && state.Buttons.Start == ButtonState.Pressed && prevState.Buttons.Start == ButtonState.Released)
            {
                Debug.Break();
                return true;
            }
            else if (button == "LT" && state.Triggers.Left > 0.2 && prevState.Triggers.Left < 0.2)
            {
                return true;
            }
            else if (button == "RT" && state.Triggers.Right > 0.2 && prevState.Triggers.Right < 0.2)
            {
                return true;
            }
        }
        return false;
    }
    public bool getButtonReleased(string button)
    {
        if (playerIndexSet)
        {
            if (button == "A" && state.Buttons.A == ButtonState.Released)
            {
                return true;
            }
            else if (button == "B" && state.Buttons.B == ButtonState.Released)
            {
                return true;
            }
            else if (button == "X" && state.Buttons.X == ButtonState.Released)
            {
                return true;
            }
            else if (button == "Y" && state.Buttons.Y == ButtonState.Released)
            {
                return true;
            }
            else if (button == "RB" && state.Buttons.RightShoulder == ButtonState.Released)
            {
                return true;
            }
            else if (button == "LB" && state.Buttons.LeftShoulder == ButtonState.Released)
            {
                return true;
            }
            else if (button == "LT" && state.Triggers.Left < 0.2 && prevState.Triggers.Left > 0.2)
            {
                return true;
            }
            else if (button == "RT" && state.Triggers.Right < 0.2 && prevState.Triggers.Right > 0.2)
            {
                return true;
            }
        }
        return false;
    }

    public void rumble(float rumbleAmountX , float rumbleAmountY)
    {
        GamePad.SetVibration(playerIndex,rumbleAmountX,rumbleAmountY);
    }

    public float getAxis(string stick,string axis)
    {
       
        if (playerIndexSet)
        {
            float Mag = Mathf.Clamp01(Vector2.SqrMagnitude(new Vector2(state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y)) + 0.2f);



            if (axis == "X" && stick == "Left")
            {

                if (Mathf.Abs(state.ThumbSticks.Left.X) < innerDeadzone && Mathf.Abs(state.ThumbSticks.Left.Y) < innerDeadzone)
                {
                    return 0.0f;
                }
                else if (Mathf.Abs(state.ThumbSticks.Left.X) > outerDeazone)
                {
                    if (state.ThumbSticks.Left.X > 0)
                        return 1.0f;
                    else
                        return -1.0f;
                }
                else
                {
                    float x = (Mathf.Pow(Mag, curve));

                    return x * state.ThumbSticks.Left.X;

                }
            }
            else if (axis == "Y" && stick == "Left")
            {

                if (Mathf.Abs(state.ThumbSticks.Left.Y) < innerDeadzone && Mathf.Abs(state.ThumbSticks.Left.X) < innerDeadzone)
                {
                    return 0.0f;
                }
                else if (Mathf.Abs(state.ThumbSticks.Left.Y) > outerDeazone)
                {
                    if (state.ThumbSticks.Left.Y > 0)
                        return 1.0f;
                    else
                        return -1.0f;
                }
                else
                {

                    float y = (Mathf.Pow(Mag, curve));

                    return y * state.ThumbSticks.Left.Y;

                }

            }
            else if (axis == "X" && stick == "Right")
            {
                if (Mathf.Abs(state.ThumbSticks.Right.X) < innerDeadzone && Mathf.Abs(state.ThumbSticks.Right.Y) < innerDeadzone)
                {
                    return 0.0f;
                }
                else if (Mathf.Abs(state.ThumbSticks.Right.X) > outerDeazone)
                {
                    if (state.ThumbSticks.Right.X > 0)
                        return 1.0f;
                    else
                        return -1.0f;
                }
                else
                {
                    float x = (Mathf.Pow(Mag, curve));
                   
                    return x  *state.ThumbSticks.Right.X;
                 
                }
            }
            else if (axis == "Y" && stick == "Right")
            {
                if (Mathf.Abs(state.ThumbSticks.Right.Y) < innerDeadzone && Mathf.Abs(state.ThumbSticks.Right.X) < innerDeadzone )
                {
                    return 0.0f;
                }
                else if (Mathf.Abs(state.ThumbSticks.Right.Y) > outerDeazone)
                {
                    if (state.ThumbSticks.Right.Y > 0)
                        return 1.0f;
                    else
                        return -1.0f;
                }
                else
                {
                   
                    float y = (Mathf.Pow(Mag, curve));
             
                    return y * state.ThumbSticks.Right.Y;
                   
                }
            }
        }
        return 0.0f;

    }
    public float getAxisRaw(string stick, string axis)
    {
        if (playerIndexSet)
        {


            if (axis == "X" && stick == "Left")
            {
                return state.ThumbSticks.Left.X;
            }
            else if (axis == "X" && stick == "Right")
            {              
                    return state.ThumbSticks.Right.X;
            }
            else if (axis == "Y" && stick == "Left")
            {
                return state.ThumbSticks.Left.Y;
            }
            else if (axis == "Y" && stick == "Right")
            {
                    return state.ThumbSticks.Right.Y;                  
            }
        }
        return 0.0f;

    }
    public bool getGamePadConnected()
    {
        if(playerIndexSet)
        {
            return true;
        }
        return false;
    }
}
