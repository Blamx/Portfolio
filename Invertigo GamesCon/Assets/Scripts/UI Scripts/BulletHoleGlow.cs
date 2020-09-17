using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHoleGlow : MonoBehaviour {

    Color tempCol;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        tempCol = GetComponent<SpriteRenderer>().color;
        if (tempCol.a > 0)
        {
            tempCol.a -= 0.01f;
        }
        GetComponent<SpriteRenderer>().color = tempCol;
    }
}
