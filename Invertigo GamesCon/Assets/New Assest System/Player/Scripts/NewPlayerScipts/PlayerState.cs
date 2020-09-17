using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

    public int state = 0;

    public GameObject Menu, Settings, main;

    public Player_Manager manager;

    //the setting's settings
    public bool sensitvity;
    public bool fov;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(state == 0)
        {
            Menu.SetActive(false);
            Settings.SetActive(false);
            main.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;//lock cursor to center of screen
        }
        else if(state == 1)
        {
            Menu.SetActive(true);
            Settings.SetActive(false);
            main.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;//lock cursor to center of screen
        }
        else if (state == 2)
        {
            Menu.SetActive(true);
            Settings.SetActive(true);
            main.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;//lock cursor to center of screen
        }
    }
}
