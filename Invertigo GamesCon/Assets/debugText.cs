using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class debugText : MonoBehaviour {


    public Text debug;
    public GameObject gameManager;

	// Use this for initialization
	void Start ()
    {
        gameManager = GameObject.Find("GameManager");	
	}
	
	// Update is called once per frame
	void Update ()
    {
        debug.text = gameManager.GetComponent<NetworkManager>().output;
	}
}
