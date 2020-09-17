using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour {

    public Player_Manager manager;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Return()
    {
        manager.state.state = 0;
    }
    public void Exit()
    {
        manager.backToMenu();
    }
}
