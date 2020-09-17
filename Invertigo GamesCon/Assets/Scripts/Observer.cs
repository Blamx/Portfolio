using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour {

    public static List<GameObject> logList = new List<GameObject>();
    public static int numOfLogs = 0;
    public GameObject check;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (logList.Count > 0)
        {
            print("no problems here");
        }
        float value;
        dataTransfer.mixer.GetFloat("Master_Volume", out value);
	}
}
