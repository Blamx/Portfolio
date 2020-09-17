using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowBreak : MonoBehaviour {
    public Transform brokenGlass;
	GameObject ship;
    bool isBroken = false;

	// Use this for initialization
	void Start () {
		ship = GameObject.Find("Broken_Shuttle");
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.Find("glass") == null && !isBroken)
        {
            Instantiate(brokenGlass, transform.position, transform.rotation);
            isBroken = true;
        }

		if (isBroken)
		{
			ship.transform.position = new Vector3(
				ship.transform.position.x + 5.0f,
				ship.transform.position.y,
				ship.transform.position.z);
		}
	}
}
