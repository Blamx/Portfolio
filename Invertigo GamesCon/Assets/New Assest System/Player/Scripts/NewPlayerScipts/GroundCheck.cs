using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    public Player_Manager manager;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay()
    {
        manager.movment.onGround = true;
    }
    void OnTriggerExit()
    {
        manager.movment.onGround = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        /*if(other.GetComponent<Target>())
        {
            other.GetComponent<Target>().oldHealth = other.GetComponent<Target>().health;
            other.GetComponent<Target>().health -= 50;
            other.GetComponent<Target>().newHealth = other.GetComponent<Target>().health;
            //Debug.Log(manager.movment.rb.velocity.magnitude);
        }*/
    }
}
