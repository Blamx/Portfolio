using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitIndicator : MonoBehaviour
{
    public Player_Manager manager;

    private GameObject player, cam;

    public Rigidbody arrow;

    Rigidbody clone;

    private Vector3 lookAt;

    string nameCheck;

    // Use this for initialization
    void Start()
    {
        cam = GameObject.Find(transform.parent.parent.parent.name + "/Camera");
        player = GameObject.Find(transform.parent.parent.parent.parent.parent.parent.name);
        lookAt = Vector3.zero;
        nameCheck = "";
    }

    // Update is called once per frame
    void Update()
    {
       // print(player.name + "--------------------");
        if (manager.target.oldHealth != manager.target.newHealth) //If player loses life
        {
            if (manager.playerStats.lastHitBy != null)
            {
                //print(player.name + "   Hit By:        " + manager.playerStats.lastHitBy.name + "     located at:    " + manager.playerStats.lastHitBy.transform.GetChild(0).transform.GetChild(0).name);
                //print(manager.playerStats.lastHitBy.name + "      " + manager.playerStats.lastHitBy.transform.position);
                if (manager.playerStats.lastHitBy.transform.parent.parent.name != nameCheck || manager.playerStats.lastHitBy.name != nameCheck)
                {
                    //Debug.Break();
                    clone = Instantiate(arrow, manager.playerStats.gameObject.transform, false) as Rigidbody; //Instantiate (clone) the arrow prefab, set the player as the parent, and make its position relative to the parent
                    clone.GetComponent<lookAt>().player = manager.playerStats.lastHitBy.transform.position; //Pass the player and its attached cam to the cloned prefab
                    clone.GetComponent<lookAt>().cam = manager.cam.transform.gameObject;

                    manager.target.oldHealth = manager.target.newHealth;
                    //player.GetComponent<Player_Player>().oldHP = player.GetComponent<Player_Player>().newHP; //Set its old and new HP to be the same so its not "hit" anymore

                    clone.name = nameCheck = manager.playerStats.lastHitBy.transform.parent.parent.name;
                    //print(manager.playerStats.lastHitBy.transform.parent.parent.name + "     " + manager.playerStats.name);
                }
                else
                {
                    if (clone != null)
                    {
                        Destroy(clone.gameObject);
                    }
                    clone = Instantiate(arrow, this.gameObject.transform, false) as Rigidbody; //Instantiate (clone) the arrow prefab, set the player as the parent, and make its position relative to the parent
                    clone.GetComponent<lookAt>().player = manager.playerStats.lastHitBy.transform.position; //Pass the player and its attached cam to the cloned prefab
                    clone.GetComponent<lookAt>().cam = manager.cam.transform.gameObject;

                    //player.GetComponent<Player_Player>().oldHP = player.GetComponent<Player_Player>().newHP; //Set its old and new HP to be the same so its not "hit" anymore

                    clone.name = nameCheck = manager.playerStats.lastHitBy.name;
                }

            }

            if (manager.target.oldHealth == manager.target.newHealth) //If player not hit
            {
                if (clone != null) //If a clone is existing in the scene
                {
                    Destroy(clone.gameObject, 5); //Destroy the clone after 5 seconds
                }
            }
        }
    }
}
