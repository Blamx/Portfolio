using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuSlots : MonoBehaviour {

    public GameObject cursor;

    public int slot;

    public GameObject[] menuButtons = new GameObject[1];
    public int menuHeight;

    public GameObject[] settingButtons = new GameObject[1];
    public int settingHeight;

    int currentMax = 0;
    int currentHeight = 0;

    bool locked;

    public Player_Manager manager;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (manager.state.state == 1)
        {
            cursor.transform.position = menuButtons[slot].transform.position;
            currentMax = menuButtons.Length;
            currentHeight = menuHeight;

            cursor.GetComponent<CursorSizeing>().bottomDistance.x = menuButtons[slot].GetComponent<RectTransform>().sizeDelta.x * 0.5f;
            cursor.GetComponent<CursorSizeing>().bottomDistance.y = -menuButtons[slot].GetComponent<RectTransform>().sizeDelta.y * 0.5f;

            cursor.GetComponent<CursorSizeing>().topDistance.x = -menuButtons[slot].GetComponent<RectTransform>().sizeDelta.x * 0.5f;
            cursor.GetComponent<CursorSizeing>().topDistance.y = menuButtons[slot].GetComponent<RectTransform>().sizeDelta.y * 0.5f;
        }
        if (manager.state.state == 2)
        {
            cursor.transform.position = settingButtons[slot].transform.position;
            currentMax = settingButtons.Length;
            currentHeight = settingHeight;

            cursor.GetComponent<CursorSizeing>().bottomDistance.x = settingButtons[slot].GetComponent<RectTransform>().sizeDelta.x * 0.5f;
            cursor.GetComponent<CursorSizeing>().bottomDistance.y = -settingButtons[slot].GetComponent<RectTransform>().sizeDelta.y * 0.5f;

            cursor.GetComponent<CursorSizeing>().topDistance.x = -settingButtons[slot].GetComponent<RectTransform>().sizeDelta.x * 0.5f;
            cursor.GetComponent<CursorSizeing>().topDistance.y = settingButtons[slot].GetComponent<RectTransform>().sizeDelta.y * 0.5f;
        }

        if (manager.playerGamepad.commands.menuDown && !locked)
            {
                Debug.Log("down");
                if (slot < currentMax - 1 && (slot + 1) % currentHeight != 0)
                    slot++;
            }
        if (manager.playerGamepad.commands.menuUp && !locked)
            {
                Debug.Log("up");
                if (slot > 0 && (slot) % currentHeight != 0)
                    slot--;
            }
        if (manager.playerGamepad.commands.menuLeft)
            {
                Debug.Log("left");
                if (slot - (currentHeight - 1) > 0)
                    slot -= currentHeight;
            }
        if (manager.playerGamepad.commands.menuRight)
            {
                Debug.Log("Right");
                if (slot + currentHeight <= currentMax - 1)
                    slot += currentHeight;
            }
        if (manager.playerGamepad.commands.select)
            {

                if (manager.state.state == 1)
                {
                    if (slot == 0)
                    {
                        manager.menuButtons.Return();
                    }
                    if (slot == 1)
                    {
                        manager.state.state = 2;
                        slot = 0;
                    }
                    if (slot == 2)
                    {
                        manager.menuButtons.Exit();
                    }
                }
                else if (manager.state.state == 2)
                {
                    if (slot == 0)
                    {

                        if (manager.state.sensitvity)
                        {
                            locked = false;
                            manager.state.sensitvity = false;
                            cursor.GetComponent<SpriteRenderer>().color = Color.white;
                        }
                        else
                        {
                            locked = true;
                            manager.state.sensitvity = true;
                            cursor.GetComponent<SpriteRenderer>().color = Color.red;
                        }
                    }
                if (slot == 1)
                {

                    if (manager.state.fov)
                    {
                        locked = false;
                        manager.state.fov = false;
                        cursor.GetComponent<SpriteRenderer>().color = Color.white;
                    }
                    else
                    {
                        locked = true;
                        manager.state.fov = true;
                        cursor.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                }

            }

            }
        if (manager.playerGamepad.commands.back || manager.playerGamepad.commands.pause)
        {
            if (manager.state.state == 1)
            {
                manager.state.state = 0;
            }
            if (manager.state.state == 2)
            {
               if (manager.state.sensitvity)
               {
                    locked = false;
                    manager.state.sensitvity = false;
                   cursor.GetComponent<SpriteRenderer>().color = Color.white;
               }
               else if (manager.state.fov)
               {
                    locked = false;
                    manager.state.fov = false;
                   cursor.GetComponent<SpriteRenderer>().color = Color.white;
               }
               else
               {
                   manager.state.state = 1;
               }
            }
        }

    }
}
