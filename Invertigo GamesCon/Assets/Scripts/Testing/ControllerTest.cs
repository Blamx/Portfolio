using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour {


    GamePad_Manager gamePad;
    GameObject raw;

	// Use this for initialization
	void Start ()
    {
        gamePad = transform.parent.transform.gameObject.GetComponent<GamePad_Manager>();
        raw = GameObject.Find("RawInput");
	}
	
	// Update is called once per frame
	void Update ()
    {



        if (transform.name == "AjustedInput")
        { transform.position = Vector3.Slerp(transform.position, raw.transform.position, 0.9999f * Time.deltaTime*10); }
        else
        { transform.position = new Vector3(gamePad.getAxis("Right", "X") * 5, gamePad.getAxis("Right", "Y") * 5, -15); }

    }
}
