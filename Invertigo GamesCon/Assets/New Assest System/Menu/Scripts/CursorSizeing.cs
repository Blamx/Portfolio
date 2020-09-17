using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CursorSizeing : MonoBehaviour {

    public GameObject bottom;
    public GameObject top;

    public Image topIm, bottomIm;

    public Vector2 bottomDistance = new Vector2(0,0);
    public Vector2 topDistance = new Vector2(0, 0);

    public float scale = 1;

    public Vector2 offset;
    public Vector2 mult;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        top.transform.localPosition = topDistance;
        bottom.transform.localPosition = bottomDistance;

        topIm.color = gameObject.GetComponent<SpriteRenderer>().color;
        bottomIm.color = gameObject.GetComponent<SpriteRenderer>().color;
        
    }
}
