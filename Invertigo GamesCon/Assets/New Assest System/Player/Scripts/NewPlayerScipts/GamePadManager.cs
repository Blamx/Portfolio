using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using UnityEngine;

public class GamePadManager : MonoBehaviour {

    [DllImport("ControllerInput")]
    public static extern void InitGamepads(); // Sets gamepad IDs (0 to 3)

    [DllImport("ControllerInput")]
    public static extern bool CheckGamepadConnected(int a_GamepadNum); // Checks gamepad state and updates input

    [DllImport("ControllerInput")]
    public static extern bool GetControllerKeyDown(int a_GamepadNum, int a_ButtonID); // Returns true if the button is pressed

    [DllImport("ControllerInput")]
    public static extern gamepadAxes GetControllerAxes(int a_GamepadNum);

    [DllImport("ControllerInput")]
    public static extern void SetControllerRumble(int a_GamepadNum, float a_LeftMotor, float a_RightMotor);

    // Returns true if button was pressed after last update
    [DllImport("ControllerInput")]
    public static extern bool GetControllerKeyPressed(int a_GamepadNum, int a_ButtonID);

    //Returns true if button was released after the last update
    [DllImport("ControllerInput")]
    public static extern bool GetControllerKeyReleased(int a_GamepadNum, int a_ButtonID);

    // Returns true if button is pressed
    [DllImport("ControllerInput")]
    public static extern bool GetCommandDown(int a_Player, string a_Command);

    // Returns true if command was pressed after last update
    [DllImport("ControllerInput")]
    public static extern bool GetCommandPressed(int a_Player, string a_Command);

    // Returns true if command was released after last update
    [DllImport("ControllerInput")]
    public static extern bool GetCommandReleased(int a_Player, string a_Command);

    [DllImport("ControllerInput")]
    public static extern void SetControllerRumbleTime(int gamepadNum, float leftMotor, float rightMotor, double time);

    public struct gamepadAxes
    {

        public float l_ThumbStick_X;
        public float l_ThumbStick_Y;

        public float r_ThumbStick_X;
        public float r_ThumbStick_Y;

        public float l_Trigger;
        public float r_Trigger;
    };
    public enum button
    {
        A = 0,
        B = 1,
        X = 2,
        Y = 3,

        DPad_UP = 4,
        DPad_DOWN = 5,
        DPad_LEFT = 6,
        DPad_RIGHT = 7,

        Shldr_LEFT = 8,
        Shldr_RIGHT = 9,

        ThumbStick_LEFT = 10,
        ThumbStick_RIGHT = 11,

        Start = 12,
        Back = 13
    };

    public bool[] active = new bool[5];
    bool[] hasPlayer = new bool[5];

    public int numberOfPlayers = 0;
    public GameObject player;

    public Camera p1Cam;
    public GameObject p1Layer;
    public Camera p2Cam;
    public GameObject p2Layer;
    public Camera p3Cam;
    public GameObject p3Layer;
    public Camera p4Cam;
    public GameObject p4Layer;
    public Camera p5Cam;
    public GameObject p5Layer;

    public Team_Manager teamManager;

    public ScoreBoard scoreBoard;

    public bool menu;

    static public int playersActive = 0;

    public static bool end = false;
    // Use this for initialization
    void Start ()
    {
        InitGamepads();
        for(int i = 0; i <= 3; i++)
        {
           
            if(CheckGamepadConnected(i))
            {
                Debug.Log("gamepad" + i + "is connected");
            }
            else
            {
                Debug.Log("gamepad" + i + "is not connected");
            }
        }

        if(!menu)
        {
            active = dataTransfer.active;
        }
        if (menu)
        {
            active = dataTransfer.active;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        end = false;
       for (int i = 0; i <= 3; i++)
        {
            CheckGamepadConnected(i);

            if(GamePadManager.GetControllerKeyDown(i, 0))
            {
                end = true;
            }

            if (GamePadManager.GetControllerKeyDown(i, 12) && menu)
            {
                playersActive++;
                active[i] = true;
            }
                if(!hasPlayer[i] && !menu && active[i])
                {


                    numberOfPlayers++;

                    int number = i + 1;
                    
                    GameObject temp = Instantiate<GameObject>(player, this.gameObject.transform);
                    temp.gameObject.GetComponent<Player_Manager>().playerGamepad.gamepad = i;
                    temp.gameObject.GetComponent<Player_Manager>().playerGamepad.player = i;
                    temp.gameObject.GetComponent<Player_Manager>().gamePadManager = gameObject.GetComponent<GamePadManager>();
                    temp.gameObject.name = "Player" + number;
                    temp.gameObject.GetComponent<Player_Manager>().playerStats.playername = "Player" + number;
                    hasPlayer[i] = true;
                    scoreBoard.players.Add(temp.GetComponent<PlayerStats>());

                    //replace with team selector
                    temp.gameObject.GetComponent<Player_Manager>().playerStats.team = i;
                    Debug.Log(temp.gameObject.GetComponent<Player_Manager>().playerStats.team);


                    if (i == 0)
                    {
                        temp.gameObject.GetComponent<Player_Manager>().cam.GetComponent<Camera>().cullingMask = p1Cam.cullingMask;
                        temp.gameObject.GetComponent<Player_Manager>().movment.transform.GetChild(0).GetChild(1).gameObject.layer = p1Layer.layer;
                        temp.gameObject.GetComponent<Player_Manager>().movment.transform.GetChild(0).GetChild(0).gameObject.layer = p1Layer.layer;
                        temp.gameObject.GetComponent<Player_Manager>().movment.transform.gameObject.layer = p1Layer.layer;
                       
                    }

                    else if (i == 1)
                    {
                        temp.gameObject.GetComponent<Player_Manager>().cam.GetComponent<Camera>().cullingMask = p2Cam.cullingMask;
                        temp.gameObject.GetComponent<Player_Manager>().movment.transform.GetChild(0).GetChild(1).gameObject.layer = p2Layer.layer;
                        temp.gameObject.GetComponent<Player_Manager>().movment.transform.GetChild(0).GetChild(0).gameObject.layer = p2Layer.layer;
                        temp.gameObject.GetComponent<Player_Manager>().movment.transform.gameObject.layer = p2Layer.layer;
                    }

                    else if (i == 2)
                    {
                        temp.gameObject.GetComponent<Player_Manager>().cam.GetComponent<Camera>().cullingMask = p3Cam.cullingMask;
                        temp.gameObject.GetComponent<Player_Manager>().movment.transform.GetChild(0).GetChild(1).gameObject.layer = p3Layer.layer;
                        temp.gameObject.GetComponent<Player_Manager>().movment.transform.GetChild(0).GetChild(0).gameObject.layer = p3Layer.layer;
                        temp.gameObject.GetComponent<Player_Manager>().movment.transform.gameObject.layer = p3Layer.layer;
                    }

                    else if (i == 3)
                    {
                        temp.gameObject.GetComponent<Player_Manager>().cam.GetComponent<Camera>().cullingMask = p4Cam.cullingMask;
                        temp.gameObject.GetComponent<Player_Manager>().movment.transform.GetChild(0).GetChild(1).gameObject.layer = p4Layer.layer;
                        temp.gameObject.GetComponent<Player_Manager>().movment.transform.GetChild(0).GetChild(0).gameObject.layer = p4Layer.layer;
                        temp.gameObject.GetComponent<Player_Manager>().movment.transform.gameObject.layer = p4Layer.layer;
                    }
                 

                }
            
            if (GamePadManager.GetControllerKeyDown(i, 13) && menu)
            {
               
                if (hasPlayer[i])
                {
                    numberOfPlayers--;
                    GameObject temp = GameObject.Find("Player" + i);
                    Destroy(temp);
                    hasPlayer[i] = false;
                }
                active[i] = false;
                playersActive--;
            }
        }

       

        if (Input.GetKeyDown(KeyCode.Return) && menu)
        {
                active[4] = true;
                playersActive++;
        }
            if (!hasPlayer[4] && !menu && active[4])
            {
                numberOfPlayers++;
                GameObject temp = Instantiate<GameObject>(player, this.gameObject.transform);
                temp.gameObject.GetComponent<Player_Manager>().playerGamepad.isKeyboard = true;
                temp.gameObject.GetComponent<Player_Manager>().playerGamepad.gamepad = 4;
                temp.gameObject.GetComponent<Player_Manager>().playerGamepad.player = 4;
                temp.gameObject.GetComponent<Player_Manager>().gamePadManager = gameObject.GetComponent<GamePadManager>();
                temp.gameObject.GetComponent<Player_Manager>().cam.GetComponent<Camera>().cullingMask = p5Cam.cullingMask;
                temp.gameObject.GetComponent<Player_Manager>().movment.transform.GetChild(0).GetChild(1).gameObject.layer = p5Layer.layer;
                temp.gameObject.GetComponent<Player_Manager>().movment.transform.GetChild(0).GetChild(0).gameObject.layer = p5Layer.layer;
                temp.gameObject.GetComponent<Player_Manager>().movment.transform.gameObject.layer = p5Layer.layer;
                temp.gameObject.name = "Player" + 4;
                temp.gameObject.GetComponent<Player_Manager>().playerStats.playername = "Player" + 5;
                scoreBoard.players.Add(temp.GetComponent<PlayerStats>());
                hasPlayer[4] = true;
                temp.gameObject.GetComponent<Player_Manager>().playerStats.team = 3;
                Debug.Log(temp.gameObject.GetComponent<Player_Manager>().playerStats.team);

                //gameObject.GetComponent<NetworkManager>().currentPlayer = temp.GetComponent<GameObject>();
               // gameObject.GetComponent<NetworkManager>().playerSpawn = true;
            }
       
       if (Input.GetKeyDown(KeyCode.Backspace) && menu)
       {
            playersActive--;
            active[4] = false;
            if (hasPlayer[4])
            {
                numberOfPlayers--;
                GameObject temp = GameObject.Find("Player" + 5);
                Destroy(temp);
                hasPlayer[4] = false;
            }
           
       }

        dataTransfer.active = active;
    }


}
