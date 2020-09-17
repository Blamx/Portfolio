using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSpin : MonoBehaviour {

    float timer = 0;
    bool timerDir = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(timer < -1)
        {
            timerDir = true;
        }
        else if(timer > 0.5)
        {
            timerDir = false;
        }

        if (timerDir)
            timer += Time.deltaTime;
        else
            timer -= Time.deltaTime;
        transform.Rotate(transform.up, 500 * Time.deltaTime);
        transform.GetComponent<Light>().intensity = timer * 2;
;	}
}
