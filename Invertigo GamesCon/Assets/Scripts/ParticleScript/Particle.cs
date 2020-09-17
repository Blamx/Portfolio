using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour {

    public bool die;
 
    // Use this for initialization
    void Start () {


    }
	
	// Update is called once per frame
	void Update ()
    {
       
		if (die)
		{
			Destroy(gameObject.GetComponent<TrailRenderer>());
			Destroy(this.gameObject);
		}
		
    }
}
