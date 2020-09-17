using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour {

    GameObject temp;
    public GameObject emitter;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update() {

        
    }

    public void Sparks(Vector3 position,Quaternion bull)
        {
         Instantiate<GameObject>(emitter,position,bull);    
        }
}
