using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuSize : MonoBehaviour {


    public Vector2 offset = new Vector2(1100, 650);
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //gameObject.GetComponent<RectTransform>().localScale = new Vector3(offset, 1, 1);
      
        gameObject.GetComponent<RectTransform>().sizeDelta = offset;
    }
}
