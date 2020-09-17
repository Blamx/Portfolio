using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitchAimPS : MonoBehaviour {

    private GameObject cam;

	// Use this for initialization
	void Start () {
        cam = GameObject.Find(transform.parent.parent.parent.name + "/Camera");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
