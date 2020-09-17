using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePos : MonoBehaviour {

    public bool resize;

    public float offset = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.GetComponent<RectTransform>().localPosition = new Vector3(offset, 0, 0);
    }
}
