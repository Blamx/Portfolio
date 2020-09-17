using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAt : MonoBehaviour
{

    public GameObject cam;

    public Vector3 player;

    Vector3 lookat;

    // Use this for initialization
    void Start()
    {
        
        lookat = player; //Stores the position of the player who hit
//        print(lookat);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(lookat);//Looks at the position constantly
        Debug.DrawLine(transform.position, lookat);
        Debug.DrawLine(transform.position, Vector3.zero, Color.red);

        transform.position = new Vector3(cam.transform.position.x + cam.transform.forward.x, cam.transform.position.y + cam.transform.forward.y + 0.2f, cam.transform.position.z + cam.transform.forward.z); //Transforms the positoin of the arrow to always be in front of the player

        //this.name = "Arrow" + player.transform.parent.name;

        //print(this.name);
    }
}
