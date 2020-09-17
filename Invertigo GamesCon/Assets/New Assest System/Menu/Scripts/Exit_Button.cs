using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit_Button : MonoBehaviour {

	// Update is called once per frame
	public void quit () {
        print("Close");
        Application.Quit();
	}
}
