using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour {

    public NewMovment movment;
    public NewCamrea cam;
    public Shooting shooting;
    public Gun_loader gunLoader;
    public Gun_Behavior gunBehavior;
    public CameraOrigin cameraOrigin;
    public CamPos camPos;
    public PlayerStats playerStats;
    public GamePadManager gamePadManager;
    public PlayerGamepad playerGamepad;
    public CameraResizeing cameraResizeing;
    public Target target;
    public Advanced_Stick avdStick;
    public GameObject footBox;
    public CameraFov camFov;
    public ArmMoveing armMove;
    public Team_Manager teamManager;
    public UIresizeing uIresizeing;
    public HitMarker hitMarker;
    public PlayerCheck HitCone;
    public RulerPos rulerPosLeft;
    public RulerPos rulerPosRight;
    public ScorePos scorePos;
    public HealthBar health;
    public PlayerState state;
    public MenuSize menuSize;
    public MenuButtons menuButtons;
    public Observer observer;
    public GameObject glowColor;

    int playerNum = 0;
	//public GameObject[] playerSounds = new GameObject[1];

	// Use this for initialization
	void Start ()
    {
        if (GameObject.Find("Suction Zone"))
        {
            Physics.IgnoreCollision(GameObject.Find("Suction Zone").GetComponent<Collider>(), footBox.GetComponent<Collider>(), true);
        }
        transform.position = Spawn_System.findSpawn().transform.position;
        transform.rotation = Spawn_System.findSpawn().transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerGamepad.gamepad == 0)
        {
            glowColor.GetComponent<Renderer>().materials[1].color = Color.blue;
        }
        if (playerGamepad.gamepad == 1)
        {
            glowColor.GetComponent<Renderer>().materials[1].color = Color.red;
        }
        if (playerGamepad.gamepad == 2)
        {
            glowColor.GetComponent<Renderer>().materials[1].color = Color.yellow;
        }
        if (playerGamepad.gamepad == 3)
        {
            glowColor.GetComponent<Renderer>().materials[1].color = Color.cyan;
        }
        if (playerGamepad.gamepad == 4)
        {
            glowColor.GetComponent<Renderer>().materials[1].color = new Vector4(1f, 0.4f, 0f, 1);
        }
        //Debug.Log(playerNum + ": " + playerStats.kills + "/" + playerStats.deaths);

        if (playerStats.dead)
        {
            movment.gameObject.SetActive(false);
            armMove.gameObject.SetActive(false);
            uIresizeing.gameObject.SetActive(false);
        }
        else
        {
            movment.gameObject.SetActive(true);
            armMove.gameObject.SetActive(true);
            uIresizeing.gameObject.SetActive(true);
        }
        if(playerNum != CameraResizeing.StaticPlayerNum)
        {
            foreach (GameObject temp in GameObject.FindObjectsOfType(typeof(GameObject)))
            {
                if (temp.name == "FeetCube")
                {
                    Physics.IgnoreCollision(temp.GetComponent<Collider>(), HitCone.GetComponent<Collider>(), true);
                }
            }
        }
        playerNum = CameraResizeing.StaticPlayerNum;

        if(state.state == 0 && playerGamepad.commands.pause)
        {
            state.state = 1;
        }
        else if (state.state == 1 && playerGamepad.commands.pause)
        {
            state.state = 0;
        }
        if(target.health < target.prevhp && !playerGamepad.isKeyboard)
        {
           GamePadManager.SetControllerRumbleTime(playerGamepad.gamepad, 1.0f, 1.0f, 0.2);
        }
    }

    public void backToMenu()
    {
        Gametype_Manager.backToMenu();
    }
}
