using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIresizeing : MonoBehaviour {

    public Vector2 scale = new Vector2(1,1);

    Vector2 baseSize = new Vector2(100, 100);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.localScale = new Vector3(scale.x * 1.8f,scale.y * 1.8f, 1);
        
	}
}
