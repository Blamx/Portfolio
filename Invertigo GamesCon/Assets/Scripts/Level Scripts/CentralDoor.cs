using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralDoor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Aperture.gatesClosed)
        {
            GameObject.Find("DeathZone").GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            GameObject.Find("DeathZone").GetComponent<BoxCollider>().enabled = true;
        }
	}
}
