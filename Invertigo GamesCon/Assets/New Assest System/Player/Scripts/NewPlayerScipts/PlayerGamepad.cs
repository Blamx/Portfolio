using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGamepad : MonoBehaviour {

    public Player_Manager manager;

    public int gamepad = -1;
    public int player = -1;

    public bool isKeyboard;

    public struct Commands
    {
        public bool jump;
        public bool flip;
        public bool aim;
        public bool shoot;
        public bool sprint;
        public bool slam;
        public bool dash;
        public bool swap;
        public bool crouch;
        public bool grenade;

        public bool pause;

        public bool menuDown;
        public bool menuUp;
        public bool menuRight;
        public bool menuLeft;

        public bool back;
        public bool select;
    };

    public Commands commands;

	// Use this for initialization
	void Start () {
		
	}
       /*
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
        */
	// Update is called once per frame
	void Update ()
    {

        if (!isKeyboard)
        {
            if (manager.state.state == 0)
            {
                commands.jump = GamePadManager.GetControllerKeyPressed(manager.playerGamepad.gamepad, 0);
                commands.flip = GamePadManager.GetControllerKeyDown(manager.playerGamepad.gamepad, 8);
                commands.aim = GamePadManager.GetControllerAxes(manager.playerGamepad.gamepad).l_Trigger > 0.4f;
                commands.shoot = GamePadManager.GetControllerAxes(manager.playerGamepad.gamepad).r_Trigger > 0.4f;
                commands.sprint = GamePadManager.GetControllerKeyDown(manager.playerGamepad.gamepad, 10);
                commands.slam = GamePadManager.GetControllerKeyPressed(manager.playerGamepad.gamepad, 2);
                commands.dash = GamePadManager.GetControllerKeyPressed(manager.playerGamepad.gamepad, 11);
                commands.swap = GamePadManager.GetControllerKeyPressed(manager.playerGamepad.gamepad, 3);
                commands.crouch = GamePadManager.GetControllerKeyPressed(manager.playerGamepad.gamepad, 1);
                //commands.grenade = GamePadManager.GetControllerKeyPressed(manager.playerGamepad.gamepad, 9);
            }
            commands.pause = GamePadManager.GetControllerKeyPressed(manager.playerGamepad.gamepad, 12);

            commands.menuUp = GamePadManager.GetControllerKeyPressed(manager.playerGamepad.gamepad, 4);
            commands.menuDown = GamePadManager.GetControllerKeyPressed(manager.playerGamepad.gamepad, 5);
            commands.menuLeft = GamePadManager.GetControllerKeyPressed(manager.playerGamepad.gamepad, 6);
            commands.menuRight = GamePadManager.GetControllerKeyPressed(manager.playerGamepad.gamepad, 7);

            commands.back = GamePadManager.GetControllerKeyPressed(manager.playerGamepad.player, 1);
            commands.select = GamePadManager.GetControllerKeyPressed(manager.playerGamepad.player, 0);
        }
        else
        {
            if (manager.state.state == 0)
            {
                commands.jump = Input.GetKeyDown(KeyCode.Space);
                commands.flip = Input.GetKeyDown("q");
                commands.aim = Input.GetMouseButton(1);
                commands.shoot = Input.GetMouseButton(0);
                commands.sprint = Input.GetKeyDown(KeyCode.LeftShift);
                commands.slam = Input.GetKeyDown(KeyCode.LeftControl);
                commands.dash = Input.GetKeyDown(KeyCode.E);
            }
                commands.pause = Input.GetKeyDown(KeyCode.Escape);

            commands.menuUp = Input.GetKeyDown(KeyCode.W);
            commands.menuDown = Input.GetKeyDown(KeyCode.S);
            commands.menuLeft = Input.GetKeyDown(KeyCode.A);
            commands.menuRight = Input.GetKeyDown(KeyCode.D);

            commands.back = Input.GetKeyDown(KeyCode.Escape);
            commands.select = Input.GetKeyDown(KeyCode.Return);
        }

    }
}
