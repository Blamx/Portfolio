using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeIntensity : MonoBehaviour {

    public Light light;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        light.intensity = 1 - (gameObject.GetComponent<Target>().health / gameObject.GetComponent<Target>().MaxHealth);
        light.range = 100 / (gameObject.GetComponent<Target>().health / gameObject.GetComponent<Target>().MaxHealth);
    }
}
