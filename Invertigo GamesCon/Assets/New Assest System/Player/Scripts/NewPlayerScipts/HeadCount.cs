using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCount : MonoBehaviour {

    bool[] controllersActive = new bool[4];


    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < 3; i++)
        {
            controllersActive[i] = GamePadManager.CheckGamepadConnected(i);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
