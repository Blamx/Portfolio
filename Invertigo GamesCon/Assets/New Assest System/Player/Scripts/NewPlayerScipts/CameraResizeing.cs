using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CameraResizeing : MonoBehaviour {

    public Player_Manager manager;

    static public bool[] stots = new bool[4];

    int playernum = 0;
    static public int StaticPlayerNum = 0;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (StaticPlayerNum != manager.gamePadManager.numberOfPlayers)
        {
            StaticPlayerNum = manager.gamePadManager.numberOfPlayers;
            for (int i = 0; i < 3; i++)
            {
                CameraResizeing.stots[i] = false;
            }

        }

            if (playernum != manager.gamePadManager.numberOfPlayers)
            {
                if (manager.gamePadManager.numberOfPlayers == 1)
                {
                    gameObject.GetComponent<Camera>().rect = new Rect(new Vector2(0, 0), new Vector2(1, 1));
                    manager.uIresizeing.scale = new Vector2(1,1);
                    manager.rulerPosLeft.offset = 0;
                    manager.rulerPosRight.offset = 0;
                    manager.scorePos.offset = 0;
                    manager.health.offset = 10.7f;
                    manager.menuSize.offset = new Vector2(1100,650);
                }
                if (manager.gamePadManager.numberOfPlayers == 2)
                {
                   
                    if (!CameraResizeing.stots[0])
                    {
                        CameraResizeing.stots[0] = true;
                        gameObject.GetComponent<Camera>().rect = new Rect(new Vector2(0, 0.5f), new Vector2(1, 0.5f));
                        manager.uIresizeing.scale = new Vector2(0.5f, 0.5f);
                        manager.rulerPosLeft.offset = -51.5f;
                        manager.rulerPosRight.offset = 51.5f;
                        manager.scorePos.offset = -550f;
                        manager.health.offset = 22f;
                        manager.menuSize.offset = new Vector2(2200, 1000);
                    }
                    else
                    {
                        gameObject.GetComponent<Camera>().rect = new Rect(new Vector2(0, 0), new Vector2(1, 0.5f));
                        manager.uIresizeing.scale = new Vector2(0.5f, 0.5f);
                        manager.rulerPosLeft.offset = -51.5f;
                        manager.rulerPosRight.offset = 51.5f;
                        manager.scorePos.offset = -550f;
                        manager.health.offset = 22f;
                        manager.menuSize.offset = new Vector2(2200, 1000);
                    }

                }
                if (manager.gamePadManager.numberOfPlayers == 3)
                {
                if (!CameraResizeing.stots[0])
                {
                    CameraResizeing.stots[0] = true;
                    gameObject.GetComponent<Camera>().rect = new Rect(new Vector2(0, 0.5f), new Vector2(1, 0.5f));
                    manager.uIresizeing.scale = new Vector2(0.5f, 0.5f);
                    manager.rulerPosLeft.offset = -51.5f;
                    manager.rulerPosRight.offset = 51.5f;
                    manager.scorePos.offset = -550f;
                    manager.health.offset = 22f;
                    manager.menuSize.offset = new Vector2(2200, 1000);
                }
                else if (!CameraResizeing.stots[1])
                {
                    CameraResizeing.stots[1] = true;
                    gameObject.GetComponent<Camera>().rect = new Rect(new Vector2(0, 0), new Vector2(0.5f, 0.5f));
                    manager.uIresizeing.scale = new Vector2(0.5f, 0.5f);
                    manager.rulerPosLeft.offset = 0;
                    manager.rulerPosRight.offset = 0;
                    manager.scorePos.offset = 0;
                    manager.health.offset = 10.7f;
                    manager.menuSize.offset = new Vector2(1100, 650);
                }
                else
                {
                    gameObject.GetComponent<Camera>().rect = new Rect(new Vector2(0.5f, 0), new Vector2(0.5f, 0.5f));
                    manager.uIresizeing.scale = new Vector2(0.5f, 0.5f);
                    manager.rulerPosLeft.offset = 0;
                    manager.rulerPosRight.offset = 0;
                    manager.scorePos.offset = 0;
                    manager.health.offset = 10.7f;
                    manager.menuSize.offset = new Vector2(1100, 650);
                }
            }

                
                if (manager.gamePadManager.numberOfPlayers == 4)
                {
                    if (!CameraResizeing.stots[0])
                    {
                        CameraResizeing.stots[0] = true;
                        gameObject.GetComponent<Camera>().rect = new Rect(new Vector2(0, 0.5f), new Vector2(0.5f, 0.5f));
                        manager.uIresizeing.scale = new Vector2(0.5f, 0.5f);
                        manager.rulerPosLeft.offset = 0;
                        manager.rulerPosRight.offset = 0;
                        manager.scorePos.offset = 0;
                        manager.health.offset = 10.7f;
                        manager.menuSize.offset = new Vector2(1100, 650);
                    }
                    else if (!CameraResizeing.stots[1])
                    {
                        CameraResizeing.stots[1] = true;
                        gameObject.GetComponent<Camera>().rect = new Rect(new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));
                        manager.uIresizeing.scale = new Vector2(0.5f, 0.5f);
                        manager.rulerPosLeft.offset = 0;
                        manager.rulerPosRight.offset = 0;
                        manager.scorePos.offset = 0;
                       manager.health.offset = 10.7f;
                    manager.menuSize.offset = new Vector2(1100, 650);
                }
                    else if (!CameraResizeing.stots[2])
                    {
                        CameraResizeing.stots[2] = true;
                        gameObject.GetComponent<Camera>().rect = new Rect(new Vector2(0, 0), new Vector2(0.5f, 0.5f));
                        manager.uIresizeing.scale = new Vector2(0.5f, 0.5f);
                        manager.rulerPosLeft.offset = 0;
                        manager.rulerPosRight.offset = 0;
                        manager.scorePos.offset = 0;
                        manager.health.offset = 10.7f;
                    manager.menuSize.offset = new Vector2(1100, 650);
                }
                    else
                    {
                        gameObject.GetComponent<Camera>().rect = new Rect(new Vector2(0.5f, 0), new Vector2(0.5f, 0.5f));
                        manager.uIresizeing.scale = new Vector2(0.5f, 0.5f);
                        manager.rulerPosLeft.offset = 0;
                        manager.rulerPosRight.offset = 0;
                        manager.scorePos.offset = 0;
                        manager.health.offset = 10.7f;
                    manager.menuSize.offset = new Vector2(1100, 650);
                }
                }
            }
        
        playernum = manager.gamePadManager.numberOfPlayers;
       
    }

    public static void wipe()
    {
        stots = new bool[4];
    }
}
